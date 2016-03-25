using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    public class tb_categoriaController : Controller
    {
        private bd_tienda_virtual_dellEntities db = new bd_tienda_virtual_dellEntities();

        // GET: tb_categoria
        public ActionResult Index()
        {
            return View(db.tb_categoria.ToList());
        }

        // GET: tb_categoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_categoria tb_categoria = db.tb_categoria.Find(id);
            if (tb_categoria == null)
            {
                return HttpNotFound();
            }
            return View(tb_categoria);
        }

        // GET: tb_categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tb_categoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_categoria,descripcion")] tb_categoria tb_categoria)
        {
            if (ModelState.IsValid)
            {
                db.tb_categoria.Add(tb_categoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_categoria);
        }

        // GET: tb_categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_categoria tb_categoria = db.tb_categoria.Find(id);
            if (tb_categoria == null)
            {
                return HttpNotFound();
            }
            return View(tb_categoria);
        }

        // POST: tb_categoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_categoria,descripcion")] tb_categoria tb_categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_categoria);
        }

        // GET: tb_categoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_categoria tb_categoria = db.tb_categoria.Find(id);
            if (tb_categoria == null)
            {
                return HttpNotFound();
            }
            return View(tb_categoria);
        }

        // POST: tb_categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_categoria tb_categoria = db.tb_categoria.Find(id);
            db.tb_categoria.Remove(tb_categoria);
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
