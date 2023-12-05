using AdventureWorksPhotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventureWorksPhotos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            // Verificar si las cookies existen
            if (Request.Cookies["UserId"] != null && Request.Cookies["UserRole"] != null)
            {
                // Recuperar los valores de las cookies
                var userId = Request.Cookies["UserId"].Value;
                var userRole = Request.Cookies["UserRole"].Value;

                // Asignar los valores de las cookies a las variables de sesión
                Session["IdUsuario"] = userId;
                Session["RolUsuario"] = userRole;
            }

            return View();
        }

        public ActionResult AgregarFoto()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Presentacion()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

    }
}