using TiendaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.ViewModel;

namespace TiendaVirtual.Controllers
{
    public class ShoppingCartController : Controller
    {
        private bd_tienda_virtual_dellEntities db = new bd_tienda_virtual_dellEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Store/AddToCart/5
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            // Retrieve the item from the database
            var addedItem = db.tb_producto
                .Single(item => item.id_producto == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var idUser = System.Web.Security.Membership.GetUser().ProviderUserKey;
            int count = cart.AddToCart(addedItem, int.Parse(idUser.ToString()));

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(addedItem.nombre_prod) +
                    " has been added to your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = count,
                DeleteId = id
            };
            return Json(results);

            // Go back to the main store page for more shopping
            // return RedirectToAction("Index");
        }
    }
}