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
    public class tb_estadoController : Controller
    {
        private bd_tienda_virtual_dellEntities db = new bd_tienda_virtual_dellEntities();

        // GET: tb_estado
        public ActionResult Index()
        {
            return View(db.tb_estado.ToList());
        }

        // GET: tb_estado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_estado tb_estado = db.tb_estado.Find(id);
            if (tb_estado == null)
            {
                return HttpNotFound();
            }
            return View(tb_estado);
        }

        // GET: tb_estado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tb_estado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_estado,descripcion")] tb_estado tb_estado)
        {
            if (ModelState.IsValid)
            {
                db.tb_estado.Add(tb_estado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_estado);
        }

        // GET: tb_estado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_estado tb_estado = db.tb_estado.Find(id);
            if (tb_estado == null)
            {
                return HttpNotFound();
            }
            return View(tb_estado);
        }

        // POST: tb_estado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_estado,descripcion")] tb_estado tb_estado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_estado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_estado);
        }

        // GET: tb_estado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_estado tb_estado = db.tb_estado.Find(id);
            if (tb_estado == null)
            {
                return HttpNotFound();
            }
            return View(tb_estado);
        }

        // POST: tb_estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_estado tb_estado = db.tb_estado.Find(id);
            db.tb_estado.Remove(tb_estado);
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
