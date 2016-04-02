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
    public class tb_carritoController : Controller
    {
        private bd_tienda_virtual_dellEntities db = new bd_tienda_virtual_dellEntities();

        // GET: tb_carrito

        public ActionResult Index(string usuario)
        {
            if (String.IsNullOrEmpty(usuario))
            {
                var tb_carrito = db.tb_carrito.Include(t => t.tb_asociado).Include(t => t.tb_estado).Include(t => t.tb_producto);
                return View(tb_carrito.ToList());
            }
            else
            {
                var tb_carrito = db.tb_carrito.Include(t => t.tb_asociado).Include(t => t.tb_estado).Include(t => t.tb_producto.costo).Where(t => t.id_carrito_user == usuario);
                return View(tb_carrito.ToList());
            }
        }

        // GET: tb_carrito/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_carrito tb_carrito = db.tb_carrito.Find(id);
            if (tb_carrito == null)
            {
                return HttpNotFound();
            }
            return View(tb_carrito);
        }

        // GET: tb_carrito/Create
        public ActionResult Create()
        {
            ViewBag.id_asociado = new SelectList(db.tb_asociado, "id_asociado", "id_persona");
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion");
            ViewBag.id_producto = new SelectList(db.tb_producto, "id_producto", "nombre_prod");
            return View();
        }

        // POST: tb_carrito/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_carrito,id_asociado,id_producto,cantidad,id_estado,id_carrito_user")] tb_carrito tb_carrito)
        {
            if (ModelState.IsValid)
            {
                db.tb_carrito.Add(tb_carrito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_asociado = new SelectList(db.tb_asociado, "id_asociado", "id_persona", tb_carrito.id_asociado);
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion", tb_carrito.id_estado);
            ViewBag.id_producto = new SelectList(db.tb_producto, "id_producto", "nombre_prod", tb_carrito.id_producto);
            return View(tb_carrito);
        }

        // GET: tb_carrito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_carrito tb_carrito = db.tb_carrito.Find(id);
            if (tb_carrito == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_asociado = new SelectList(db.tb_asociado, "id_asociado", "id_persona", tb_carrito.id_asociado);
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion", tb_carrito.id_estado);
            ViewBag.id_producto = new SelectList(db.tb_producto, "id_producto", "nombre_prod", tb_carrito.id_producto);
            return View(tb_carrito);
        }

        // POST: tb_carrito/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_carrito,id_asociado,id_producto,cantidad,id_estado,id_carrito_user")] tb_carrito tb_carrito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_carrito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_asociado = new SelectList(db.tb_asociado, "id_asociado", "id_persona", tb_carrito.id_asociado);
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion", tb_carrito.id_estado);
            ViewBag.id_producto = new SelectList(db.tb_producto, "id_producto", "nombre_prod", tb_carrito.id_producto);
            return View(tb_carrito);
        }

        // GET: tb_carrito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_carrito tb_carrito = db.tb_carrito.Find(id);
            if (tb_carrito == null)
            {
                return HttpNotFound();
            }
            return View(tb_carrito);
        }

        // POST: tb_carrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_carrito tb_carrito = db.tb_carrito.Find(id);
            db.tb_carrito.Remove(tb_carrito);
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
