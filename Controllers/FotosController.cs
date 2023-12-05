using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AdventureWorksPhotos.Models;

namespace AdventureWorksPhotos.Controllers
{
    public class FotosController : Controller
    {
        private AWPEntities db = new AWPEntities();
        // GET: Fotos
        public ActionResult Index()
        {
            try
            {
                return View(db.Fotos.ToList());
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                return (null);
            }
        }
        // GET: Fotos
        public ActionResult Presentacion()
        {
            return View(db.Fotos.ToList());
        }

        public ActionResult GetImage(int id)
        {
            var foto = db.Fotos.Find(id);
            if (foto == null || foto.RutaArchivo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return File(foto.RutaArchivo, "image/png");
        }


        // GET: Fotos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos fotos = db.Fotos.Find(id);
            if (fotos == null)
            {
                return HttpNotFound();
            }
            return View(fotos);
        }

        // GET: Fotos/Create
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Fotos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Titulo,Descripcion,FechaCreacion,Visibilidad")] Fotos fotos, HttpPostedFileBase file)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                if (Session["IdUsuario"] is int userId)
                {
                    fotos.IdUsuario = userId;
                }

                if (file != null && file.ContentLength > 0)
                {
                    using (var reader = new BinaryReader(file.InputStream))
                    {
                        fotos.RutaArchivo = reader.ReadBytes(file.ContentLength);
                    }
                }

                db.Fotos.Add(fotos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fotos);
        }

        // GET: Fotos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos fotos = db.Fotos.Find(id);
            if (fotos == null)
            {
                return HttpNotFound();
            }
            return View(fotos);
        }

        // POST: Fotos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFoto,Titulo,Descripcion,FechaCreacion,Visibilidad")] Fotos fotos, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var fotoExistente = db.Fotos.Find(fotos.IdFoto);
                if (fotoExistente == null)
                {
                    return HttpNotFound();
                }

                fotoExistente.Titulo = fotos.Titulo;
                fotoExistente.Descripcion = fotos.Descripcion;
                fotoExistente.FechaCreacion = fotos.FechaCreacion;
                fotoExistente.Visibilidad = fotos.Visibilidad;

                if (file != null && file.ContentLength > 0)
                {
                    using (var reader = new BinaryReader(file.InputStream))
                    {
                        fotoExistente.RutaArchivo = reader.ReadBytes(file.ContentLength);
                    }
                }

                db.Entry(fotoExistente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fotos);
        }

        // GET: Fotos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos fotos = db.Fotos.Find(id);
            if (fotos == null)
            {
                return HttpNotFound();
            }
            return View(fotos);
        }


        // POST: Fotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fotos fotos = db.Fotos.Find(id);
            db.Fotos.Remove(fotos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}