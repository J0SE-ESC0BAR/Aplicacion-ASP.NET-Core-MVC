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
            return View();
        }

        public ActionResult AgregarFoto()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Galeria()
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