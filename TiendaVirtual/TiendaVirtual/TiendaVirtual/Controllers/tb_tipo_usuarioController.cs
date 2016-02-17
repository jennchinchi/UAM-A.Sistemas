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
    public class tb_tipo_usuarioController : Controller
    {
        private bd_tienda_virtual_dellEntities db = new bd_tienda_virtual_dellEntities();

        // GET: tb_tipo_usuario
        public ActionResult Index()
        {
            return View(db.tb_tipo_usuario.ToList());
        }

        // GET: tb_tipo_usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_tipo_usuario tb_tipo_usuario = db.tb_tipo_usuario.Find(id);
            if (tb_tipo_usuario == null)
            {
                return HttpNotFound();
            }
            return View(tb_tipo_usuario);
        }

        // GET: tb_tipo_usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tb_tipo_usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo_usuario,descripcion")] tb_tipo_usuario tb_tipo_usuario)
        {
            if (ModelState.IsValid)
            {
                db.tb_tipo_usuario.Add(tb_tipo_usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_tipo_usuario);
        }

        // GET: tb_tipo_usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_tipo_usuario tb_tipo_usuario = db.tb_tipo_usuario.Find(id);
            if (tb_tipo_usuario == null)
            {
                return HttpNotFound();
            }
            return View(tb_tipo_usuario);
        }

        // POST: tb_tipo_usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo_usuario,descripcion")] tb_tipo_usuario tb_tipo_usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_tipo_usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_tipo_usuario);
        }

        // GET: tb_tipo_usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_tipo_usuario tb_tipo_usuario = db.tb_tipo_usuario.Find(id);
            if (tb_tipo_usuario == null)
            {
                return HttpNotFound();
            }
            return View(tb_tipo_usuario);
        }

        // POST: tb_tipo_usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_tipo_usuario tb_tipo_usuario = db.tb_tipo_usuario.Find(id);
            db.tb_tipo_usuario.Remove(tb_tipo_usuario);
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
