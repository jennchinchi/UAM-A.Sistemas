using Microsoft.AspNet.Identity;
using TiendaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtual.Controllers
{
    [Authorize]
    public class ProcesarCompraController : Controller
    {
        private bd_tienda_virtual_dellEntities db = new bd_tienda_virtual_dellEntities();
        //
        // GET: /ProcesarCompra/SaldoDisponible
        [Authorize(Roles = "admin,cliente")]
        public ActionResult SaldoDisponible()
        {
            var asociado = db.tb_asociado.FirstOrDefault(a => a.correo_electronico == User.Identity.Name);
            //var previousOrder = db.tb_factura.FirstOrDefault(x => x.usuario == User.Identity.Name);

            if (asociado != null)
                return View(asociado);
            else
                return View();
        }

        //
        // POST: /ProcesarCompra/SaldoDisponible
        [HttpPost]
        public async Task<ActionResult> SaldoDisponible(FormCollection values)
        {
            string result = values[9];

            var order = new tb_factura();
            TryUpdateModel(order);
            //order.CreditCard = result;

            try
            {
               // order.usuario = User.Identity.Name;
                order.fecha = DateTime.Now;
                var currentUserId = User.Identity.GetUserId();

                //if (order.SaveInfo && !order.Username.Equals("guest@guest.com"))
                //{

                //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                //    var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
                //    var ctx = store.Context;
                //    var currentUser = manager.FindById(User.Identity.GetUserId());

                //    currentUser.Address = order.Address;
                //    currentUser.City = order.City;
                //    currentUser.Country = order.Country;
                //    currentUser.State = order.State;
                //    currentUser.Phone = order.Phone;
                //    currentUser.PostalCode = order.PostalCode;
                //    currentUser.FirstName = order.FirstName;

                //    //Save this back
                //    //http://stackoverflow.com/questions/20444022/updating-user-data-asp-net-identity
                //    //var result = await UserManager.UpdateAsync(currentUser);
                //    await ctx.SaveChangesAsync();

                //    await db.SaveChangesAsync();
                //}


                //Save Order
                db.tb_factura.Add(order);
                await db.SaveChangesAsync();
                //Process the order
                var cart = ShoppingCart.GetCart(this.HttpContext);
                order = cart.CreateOrder(order);



                //CheckoutController.SendOrderMessage(order.usuario, "Nueva Factura: " + order.id_factura);

                return RedirectToAction("Completada",
                    new { id = order.id_factura });

            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /ProcesarCompra/Completar
        public ActionResult Completar(int id)
        {
            // Validate customer owns this order
            //bool isValid = db.tb_factura.Any(
            //    o => o.id_factura == id &&
            //   o.usuario == User.Identity.Name);

            //if (isValid)
            //{
            //    return View(id);
            //}
            //else
            //{
               return View("Error");
            //}

        }
        
    }
}