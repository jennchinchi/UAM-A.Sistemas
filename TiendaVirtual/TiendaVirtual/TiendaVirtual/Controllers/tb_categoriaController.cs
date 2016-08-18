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
    {   // Instancia para llamar metodos de la base de datos
        private bd_tienda_virtual_Entities db = new bd_tienda_virtual_Entities();
        // muestra la lista de categorias
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.tb_categoria.ToList());
        }
        // crea la vista y la enseña
        // GET: tb_categoria/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: tb_categoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //Se guarda la lista creada y la retorna para mostrarla
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
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
        // se edita la categoría
        [Authorize(Roles = "admin")]
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
        // Se guarda lo que se editó y se muestra
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
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
