using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventureWorksPhotos.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            // Aquí añade la lógica de validación de tus credenciales
            // Si las credenciales son válidas, inicia la sesión del usuario

            return RedirectToAction("Index", "Home"); // Redirige según sea necesario
        }
    }
}