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
    public class tb_asociadoController : Controller
    {   // Instancia para llamar metodos de la base de datos
        private bd_tienda_virtual_dellEntities db = new bd_tienda_virtual_dellEntities();
        // Muestra una vista con parametros específicos
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var tb_asociado = db.tb_asociado.Include(t => t.tb_estado).Include(t => t.tb_persona);
            return View(tb_asociado.ToList());
        }
        // Se crea un asociado
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion");
            ViewBag.id_persona = new SelectList(db.tb_persona, "cedula", "nombre");
            return View();
        }

        // POST: tb_asociado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        // Se agraga el asociado a la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_asociado,id_persona,monto_ahorro,id_estado,correo_electronico")] tb_asociado tb_asociado)
        {
            if (ModelState.IsValid)
            {
                db.tb_asociado.Add(tb_asociado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion", tb_asociado.id_estado);
            ViewBag.id_persona = new SelectList(db.tb_persona, "cedula", "nombre", tb_asociado.id_persona);
            return View(tb_asociado);
        }
        // Se edita el asociado agregado con anterioridad
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_asociado tb_asociado = db.tb_asociado.Find(id);
            if (tb_asociado == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion", tb_asociado.id_estado);
            ViewBag.id_persona = new SelectList(db.tb_persona, "cedula", "nombre", tb_asociado.id_persona);
            return View(tb_asociado);
        }

        // POST: tb_asociado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //  Se encarga de guardar lo editado
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "id_asociado,id_persona,monto_ahorro,id_estado,correo_electronico")] tb_asociado tb_asociado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_asociado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_estado = new SelectList(db.tb_estado, "id_estado", "descripcion", tb_asociado.id_estado);
            ViewBag.id_persona = new SelectList(db.tb_persona, "cedula", "nombre", tb_asociado.id_persona);
            return View(tb_asociado);
        }

        // Metodo para poner Inactivo un asociado
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_asociado tb_asociado = db.tb_asociado.Find(id);
            if (tb_asociado == null)
            {
                return HttpNotFound();
            }
            return View(tb_asociado);
        }

        // POST: tb_asociado/Delete/5 Metodo para borrar un asociado y de una vez se guarda.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_asociado tb_asociado = db.tb_asociado.Find(id);
            db.tb_asociado.Remove(tb_asociado);
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
