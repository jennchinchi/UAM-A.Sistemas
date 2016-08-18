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
        private bd_tienda_virtual db = new bd_tienda_virtual();
        // Muestra una vista con parametros específicos
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var tb_asociado = db.tb_cliente.Include(t => t.tb_estado).Include(t => t.tb_persona);
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
        public ActionResult Create([Bind(Include = "id_asociado,id_persona,monto_aprobado,numero_tarjeta,id_estado,correo_electronico")] tb_cliente tb_asociado)
        {
            if (ModelState.IsValid)
            {
                db.tb_cliente.Add(tb_asociado);
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
            tb_cliente tb_asociado = db.tb_cliente.Find(id);
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
        public ActionResult Edit([Bind(Include = "id_asociado,id_persona,monto_aprobado,numero_tarjeta,id_estado,correo_electronico")] tb_cliente tb_asociado)
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
