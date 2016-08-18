using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    public class PageAdminController : Controller
    {
        private bd_tienda_virtual_Entities db = new bd_tienda_virtual_Entities();

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }
        
        public void cargar_montos()
        {
            db.sp_update_monto_asociados();
            db.SaveChanges();
        }
        
    }

}
