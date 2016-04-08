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
    public class tb_personaController : Controller
    {   // Instancia para llamar metodos de la base de datos
        private bd_tienda_virtual_dellEntities db = new bd_tienda_virtual_dellEntities();
        // se enlista  las personas y se muestran
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.tb_persona.ToList());
        }

        [Authorize(Roles = "admin")]
        // Se crea la vista y se retorna
        public ActionResult Create()
        {
            return View();
        }

        // POST: tb_persona/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        // se guarda el modelo de la vista y se muestra 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "cedula,fecha_nac,nombre,apellido,apellido2,telefono")] tb_persona tb_persona)
        {
            if (ModelState.IsValid)
            {
                db.tb_persona.Add(tb_persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_persona);
        }
        // Se edita la persona
        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_persona tb_persona = db.tb_persona.Find(id);
            if (tb_persona == null)
            {
                return HttpNotFound();
            }
            return View(tb_persona);
        }

        // POST: tb_persona/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        // Se guarda lo que se editó y se retorna a la vista
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "cedula,fecha_nac,nombre,apellido,apellido2,telefono")] tb_persona tb_persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_persona);
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
