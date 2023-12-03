using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using AdventureWorksPhotos.Models;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace AdventureWorksPhotos.Controllers
{

    public class AccountController : Controller
    {
        private readonly string _cadena = ConfigurationManager.ConnectionStrings["AdventureWorksPhotosDirect"].ConnectionString;


        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Usuarios oUsuario)
        {
            if (oUsuario.Contrasena != oUsuario.ConfirmarContrasena)
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden";
                return View();
            }

            oUsuario.Contrasena = EncryptPassword(oUsuario.Contrasena);
            var (registrado, mensaje) = RegistrarUsuario(oUsuario);
            ViewBag.Mensaje = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(Usuarios oUsuario)
        {
            oUsuario.Contrasena = EncryptPassword(oUsuario.Contrasena);
            if (ValidarUsuario(oUsuario))
            {

                // Establecer la cookie de autenticación
                FormsAuthentication.SetAuthCookie(oUsuario.NombreUsuario, true);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Mensaje = "Usuario no encontrado";
            return View();
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        private string EncryptPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(password));
            }

            var Sb = new StringBuilder(64);
            using (var hash = SHA256.Create())
            {
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                foreach (byte b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }
            }
            return Sb.ToString();
        }

        private (bool registrado, string mensaje) RegistrarUsuario(Usuarios oUsuario)
        {
            using (var cn = new SqlConnection(_cadena))
            {
                var cmd = new SqlCommand("sp_RegistrarUsuario", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@NombreUsuario", oUsuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@Apellido", oUsuario.Apellido);
                cmd.Parameters.AddWithValue("@Email", oUsuario.Email);
                cmd.Parameters.AddWithValue("@Contrasena", oUsuario.Contrasena);
                cmd.Parameters.AddWithValue("@Rol", oUsuario.Rol);
                cmd.Parameters.AddWithValue("@Estado", oUsuario.Estado);

                cmd.Parameters.Add("@Registrar", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                cn.Open();
                cmd.ExecuteNonQuery();
                return (Convert.ToBoolean(cmd.Parameters["@Registrar"].Value),
                        cmd.Parameters["@Mensaje"].Value.ToString());
            }
        }

        private bool ValidarUsuario(Usuarios oUsuario)
        {
            using (var cn = new SqlConnection(_cadena))
            {
                var cmd = new SqlCommand("sp_ValidarUsuario", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", oUsuario.Email);
                cmd.Parameters.AddWithValue("@Contrasena", oUsuario.Contrasena);

                cn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        oUsuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}