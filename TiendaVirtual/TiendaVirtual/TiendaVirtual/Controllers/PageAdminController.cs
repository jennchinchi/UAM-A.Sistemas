using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtual.Controllers
{
    public class PageAdminController : Controller
    {
        //[Authorize(Roles = "Admins")]
        public ActionResult Index()
        {
            return View();
        }
        
    }
}