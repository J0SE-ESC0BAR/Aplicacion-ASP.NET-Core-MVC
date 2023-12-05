using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        private AWPEntities db = new AWPEntities();

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
            if (oUsuario.Contrasena != oUsuario.confirmarContrasena)
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
            if(ValidarUsuario(oUsuario))
            {
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
            try
            {
                oUsuario.FechaRegistro = DateTime.Now;
                db.Usuarios.Add(oUsuario);
                db.SaveChanges();
                return (true, "Usuario registrado con éxito");
            }
            catch (Exception ex)
            {
                return (false, "Error al registrar: " + ex.Message);
            }
        }

        private bool ValidarUsuario(Usuarios oUsuario)
        {
            var usuario = db.Usuarios.FirstOrDefault(u => u.Email == oUsuario.Email && u.Contrasena == oUsuario.Contrasena);
            if (usuario != null)
            {
                oUsuario.IdUsuario = usuario.IdUsuario;
                oUsuario.NombreUsuario = usuario.NombreUsuario;

                // Aquí es donde debes establecer la cookie con el nombre de usuario en lugar del ID.
                FormsAuthentication.SetAuthCookie(oUsuario.NombreUsuario, true);
                Session["IdUsuario"] = usuario.IdUsuario;
                Session["RolUsuario"] = usuario.Rol;
                return true;
            }
            return false;
        }
    }
}