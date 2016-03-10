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
    public class tb_productoController : Controller
    {
        private bd_tienda_virtual_dellEntities db = new bd_tienda_virtual_dellEntities();

        // GET: tb_producto
        public ActionResult Index()
        {
            var tb_producto = db.tb_producto.Include(t => t.tb_categoria).Include(t => t.tb_estado);
            return View(tb_producto.ToList());
        }

        // GET: tb_producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_producto tb_producto = db.tb_producto.Find(id);
            if (tb_producto == null)
            {
                return HttpNotFound();
            }
            return View(tb_producto);
        }

        // GET: tb_producto/Create
        public ActionResult Create()
        {
            ViewBag.id_categoria_prod = new SelectList(db.tb_categoria, "id_categoria", "descripcion");
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion");
            return View();
        }

        // POST: tb_producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_producto,nombre_prod,costo,cantidad,descripcion_prod,id_categoria_prod,id_estado,imagen")] tb_producto tb_producto)
        {
            if (ModelState.IsValid)
            {
                db.tb_producto.Add(tb_producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_categoria_prod = new SelectList(db.tb_categoria, "id_categoria", "descripcion", tb_producto.id_categoria_prod);
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion", tb_producto.id_estado);
            return View(tb_producto);
        }

        // GET: tb_producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_producto tb_producto = db.tb_producto.Find(id);
            if (tb_producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_categoria_prod = new SelectList(db.tb_categoria, "id_categoria", "descripcion", tb_producto.id_categoria_prod);
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion", tb_producto.id_estado);
            return View(tb_producto);
        }

        // POST: tb_producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_producto,nombre_prod,costo,cantidad,descripcion_prod,id_categoria_prod,id_estado,imagen")] tb_producto tb_producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_categoria_prod = new SelectList(db.tb_categoria, "id_categoria", "descripcion", tb_producto.id_categoria_prod);
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion", tb_producto.id_estado);
            return View(tb_producto);
        }

        // GET: tb_producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_producto tb_producto = db.tb_producto.Find(id);
            if (tb_producto == null)
            {
                return HttpNotFound();
            }
            return View(tb_producto);
        }

        // POST: tb_producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_producto tb_producto = db.tb_producto.Find(id);
            db.tb_producto.Remove(tb_producto);
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
