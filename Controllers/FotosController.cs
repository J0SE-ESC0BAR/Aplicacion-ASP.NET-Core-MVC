using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdventureWorksPhotos.Models;

namespace AdventureWorksPhotos.Controllers
{
    public class FotosController : Controller
    {
        private AdventureWorksPhotosEntitiesPhotos db = new AdventureWorksPhotosEntitiesPhotos();

        // GET: Fotos
        public ActionResult Index()
        {
            return View(db.Fotos.ToList());
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
            return View();
        }

        // POST: Fotos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFoto,IdUsuario,Titulo,Descripcion,RutaArchivo,FechaCreacion,Visibilidad")] Fotos fotos)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult Edit([Bind(Include = "IdFoto,IdUsuario,Titulo,Descripcion,RutaArchivo,FechaCreacion,Visibilidad")] Fotos fotos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fotos).State = EntityState.Modified;
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
