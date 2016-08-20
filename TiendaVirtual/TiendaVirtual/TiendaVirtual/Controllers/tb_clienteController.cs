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
    public class tb_clienteController : Controller
    {
        private bd_tienda_virtual db = new bd_tienda_virtual();

        // GET: tb_cliente
        public ActionResult Index()
        {
            var tb_cliente = db.tb_cliente.Include(t => t.tb_estado).Include(t => t.tb_persona);
            return View(tb_cliente.ToList());
        }

        // GET: tb_cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_cliente tb_cliente = db.tb_cliente.Find(id);
            if (tb_cliente == null)
            {
                return HttpNotFound();
            }
            return View(tb_cliente);
        }

        // GET: tb_cliente/Create
        public ActionResult Create()
        {
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion");
            ViewBag.id_persona = new SelectList(db.tb_persona, "cedula", "nombre");
            return View();
        }

        // POST: tb_cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_asociado,id_persona,monto_aprobado,numero_tarjeta,correo_electronico,id_estado")] tb_cliente tb_cliente)
        {
            if (ModelState.IsValid)
            {
                db.tb_cliente.Add(tb_cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion", tb_cliente.id_estado);
            ViewBag.id_persona = new SelectList(db.tb_persona, "cedula", "nombre", tb_cliente.id_persona);
            return View(tb_cliente);
        }

        // GET: tb_cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_cliente tb_cliente = db.tb_cliente.Find(id);
            if (tb_cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion", tb_cliente.id_estado);
            ViewBag.id_persona = new SelectList(db.tb_persona, "cedula", "nombre", tb_cliente.id_persona);
            return View(tb_cliente);
        }

        // POST: tb_cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_asociado,id_persona,monto_aprobado,numero_tarjeta,correo_electronico,id_estado")] tb_cliente tb_cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion", tb_cliente.id_estado);
            ViewBag.id_persona = new SelectList(db.tb_persona, "cedula", "nombre", tb_cliente.id_persona);
            return View(tb_cliente);
        }

        // GET: tb_cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_cliente tb_cliente = db.tb_cliente.Find(id);
            if (tb_cliente == null)
            {
                return HttpNotFound();
            }
            return View(tb_cliente);
        }

        // POST: tb_cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_cliente tb_cliente = db.tb_cliente.Find(id);
            db.tb_cliente.Remove(tb_cliente);
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
