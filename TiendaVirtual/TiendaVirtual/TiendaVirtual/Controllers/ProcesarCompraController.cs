using Microsoft.AspNet.Identity;
using TiendaVirtual.Models;
using TiendaVirtual.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace TiendaVirtual.Controllers
{
    [Authorize]
    public class ProcesarCompraController : Controller
    {
        private bd_tienda_virtual db = new bd_tienda_virtual();
        //
        // GET: /ProcesarCompra/SaldoDisponible
        [Authorize(Roles = "admin,cliente")]
        public ActionResult SaldoDisponible()
        {
            var asociado = db.tb_cliente.FirstOrDefault(a => a.correo_electronico == User.Identity.Name);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            //var previousOrder = db.tb_factura.FirstOrDefault(x => x.usuario == User.Identity.Name);
            var compra = new ProcesarCompra();
            compra.asociado = asociado;
            compra.montoCompra = cart.GetTotal();
            compra.saldo = (decimal) asociado.monto_aprobado - cart.GetTotal();

            if (asociado != null)
                return View(compra);
            else
                return View();
        }

        //
        // POST: /ProcesarCompra/SaldoDisponible
        [HttpPost]
        public async Task<ActionResult> SaldoDisponible(FormCollection values)
        {
            var order = new tb_factura();
            var asociado = db.tb_cliente.FirstOrDefault(a => a.correo_electronico == User.Identity.Name);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            //var previousOrder = db.tb_factura.FirstOrDefault(x => x.usuario == User.Identity.Name);
            var compra = new ProcesarCompra();
            compra.asociado = asociado;
            compra.montoCompra = cart.GetTotal();
            compra.saldo = (decimal)asociado.monto_aprobado - cart.GetTotal();
            if (compra.montoCompra > 0)
            {
                Random rnd = new Random();
                int valor1 = ((int)compra.montoCompra) / 2;
                int valor2 = (int) (compra.montoCompra * 2);
                int monto = rnd.Next(valor1, valor2);
                if (compra.montoCompra <= monto)
                {
                    try
                    {
                        order.cliente_asociado = asociado.id_asociado;
                        order.id_direccion = 1;
                        order.id_estado = 1;
                        order.costo_total = cart.GetTotal();
                        order.fecha = DateTime.Now;
                        order.direccion = values["direccion"];
                        order.usuario = asociado.correo_electronico;

                        //Guardar Orden
                        db.tb_factura.Add(order);

                        asociado.monto_aprobado = (decimal)(asociado.monto_aprobado - order.costo_total);
                        await db.SaveChangesAsync();
                        //Procesar la orden
                        order = cart.CreateOrder(order);


                        return RedirectToAction("Completar",
                        new { id = order.id_factura });
                    }
                    catch (Exception e)
                    {
                        //Invalid - redisplay with errors
                        compra.Message = "Hubo un error en la compra: " + e.Message;
                        return View(compra);
                    }
                }
                else
                {
                    compra.Message = "Fondos insuficientes";
                    return View(compra);
                }
            }
            else
            {
                compra.Message = "No existe nada disponible en el carrito para realizar compra";
                return View(compra);
            }
        }

        //
        // GET: /ProcesarCompra/Completar
        public ActionResult Completar(int id)
        {
            // Validate customer owns this order
            bool isValid = db.tb_factura.Any(
                o => o.id_factura == id);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}