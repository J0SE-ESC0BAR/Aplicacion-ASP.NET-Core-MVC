using AdventureWorksPhotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace AdventureWorksPhotos.Controllers
{
    public class ComentariosController : Controller
    {
        private AWPEntities db = new AWPEntities();
        // GET: Comentarios
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comentarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comentarios/Create
        public ActionResult AddComment()
        {
            return View();
        }

        // POST: Comentarios/Create
        [HttpPost]
        public ActionResult AddComment(int idFoto, string textoComentario)
        {
                // Crear el nuevo comentario
                var comentario = new Comentarios
                {
                    IdFoto = idFoto,
                    TextoComentario = textoComentario,
                    FechaComentario = DateTime.Now,

                };
                if (Session["IdUsuario"] is int userId)
                {
                    comentario.IdUsuario = userId;
                }

                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Details", "Fotos", new { id = idFoto });
        }


        // GET: Comentarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comentarios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // POST: Comentarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var comentario = db.Comentarios.Find(id);
                if (comentario != null)
                {
                    db.Comentarios.Remove(comentario);
                    db.SaveChanges();
                }

                return RedirectToAction("Details", "Fotos", new { id = comentario?.IdFoto });
            }
            catch
            {
                // Manejo de errores
                return RedirectToAction("Index", "Error"); // Redirige a una página de error genérica, ajusta según sea necesario
            }
        }
    }
}
