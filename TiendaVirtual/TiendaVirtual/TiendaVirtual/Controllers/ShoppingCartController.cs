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
        private bd_tienda_virtual db = new bd_tienda_virtual();
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        [HttpPost]
        public ActionResult AddToCart(int id)
        {

            var cantidad_inventario = from cust in db.tb_producto
                           where cust.id_producto == id
                            select cust.cantidad;

            // Valida si la cantidad del item es mayor a uno
            if ( cantidad_inventario.FirstOrDefault() >= 1 ) {

                // Retrieve the item from the database
                var addedItem = db.tb_producto
                    .Single(item => item.id_producto == id);

                // Add it to the shopping cart
                var cart = ShoppingCart.GetCart(this.HttpContext);

                int count = cart.AddToCart(addedItem);

                // Display the confirmation message
                var results = new ShoppingCartRemoveViewModel
                {
                    Message = Server.HtmlEncode(addedItem.nombre_prod) +
                        " ha sido eliminado de su carrito.",
                    CartTotal = cart.GetTotal(),
                    CartCount = cart.GetCount(),
                    ItemCount = count,
                    DeleteId = id
                };
                return Json(results);
            }
            else
                return Redirect("Error_Prod_Fuera_Inventario");
        }

        // Go back to the main store page for more shopping
        // return RedirectToAction("Index");

        //     AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            //Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            //Get the name of the item to display confirmation

            //Get the name of the album to display confirmation
            string itemName = db.tb_producto
                .Single(item => item.id_producto == id).nombre_prod;

            //Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            //Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = "One (1) " + Server.HtmlEncode(itemName) +
                    " ha sido eliminado de su carrito.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        //
        // GET: /ShoppingCart/Carrito
        [ChildActionOnly]
        public ActionResult Carrito()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("Carrito");
        }
    }
}