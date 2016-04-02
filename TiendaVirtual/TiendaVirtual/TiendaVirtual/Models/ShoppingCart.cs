using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Models;

namespace TiendaVirtual.Models
{
    public partial class ShoppingCart
    {
                /*Variable para llamar la base de datos */
                private bd_tienda_virtual_dellEntities db = new bd_tienda_virtual_dellEntities();

                /*Variable para el ID del Carrito */
                string ShoppingCartId { get; set; }

                /* Constante de Actividad de session */
                public const string CartSessionKey = "CartId";

                /* Para mantener el carrito en uso */
                public static ShoppingCart GetCart(HttpContextBase context)
                {
                    var cart = new ShoppingCart();
                    cart.ShoppingCartId = cart.GetCartId(context);
                    return cart;
                }

                // Llamado a los metodos del Shopping Cart
                public static ShoppingCart GetCart(Controller controller)
                {
                    return GetCart(controller.HttpContext);
                }

                // Añadir item al carrito de la persona
                public int AddToCart(tb_producto item)
                {
                    // Get the matching cart and item instances
                    var cartItem = db.tb_carrito.SingleOrDefault(
                        c => c.id_carrito_user == ShoppingCartId
                        && c.id_producto == item.id_producto);

                    if (cartItem == null)
                    {
                        // Si no existe un item se crea
                        cartItem = new tb_carrito
                        {
                            id_producto = item.id_producto,
                            id_carrito_user = ShoppingCartId,
                            cantidad = 1,
                            id_estado = 1,
                            id_asociado= 1,
                        };
                        db.tb_carrito.Add(cartItem);
                    }
                    else
                    {
                        // Si el item existe en el carrito aumentar la cantidad
                        cartItem.cantidad++;
                    }
                    // Guardar Cambios
                    db.SaveChanges();

                    return cartItem.cantidad;
                }

                // Remover del carro
                public int RemoveFromCart(int id_producto_remover)
                {
                    // Conseguir id del caarrito

                    var cartItem = db.tb_carrito.Single(
                        cart => cart.id_carrito == Convert.ToInt32(ShoppingCartId)
                        && cart.id_producto == id_producto_remover);

                    int itemCount = 0;

                    if (cartItem != null)
                    {
                        if (cartItem.cantidad > 1)
                        {
                            cartItem.cantidad--;
                            itemCount = cartItem.cantidad;
                        }
                        else
                        {
                            db.tb_carrito.Remove(cartItem);
                        }
                        // Guardar Cambios
                        db.SaveChanges();
                    }
                    return itemCount;
                }

                public void EmptyCart()
                {
                    var cartItems = db.tb_carrito.Where(
                        cart => cart.id_carrito == Convert.ToInt32(ShoppingCartId));

                    foreach (var cartItem in cartItems)
                    {
                        db.tb_carrito.Remove(cartItem);
                    }
                    // Guardar Cambios
                    db.SaveChanges();
                }

                public List<tb_carrito> GetCartItems()
                {
                    return db.tb_carrito.Where(
                        cart => cart.id_carrito == Convert.ToInt32(ShoppingCartId)).ToList();
                }

                public int GetCount()
                {
                    // Get the count of each item in the cart and sum them up
                    int? count = (from cartItems in db.tb_carrito
                                  where cartItems.id_carrito == Convert.ToInt32(ShoppingCartId)
                                  select (int?)cartItems.cantidad).Sum();
                    // Return 0 if all entries are null
                    return count ?? 0;
                }


        public decimal GetTotal()
        {
            // Multiply item price by count of that item to get 
            // the current price for each of those items in the cart
            // sum all item price totals to get the cart total
            //decimal? total = (from cartItems in db.tb_carrito
            //                  where cartItems.id_carrito == Convert.ToInt32(ShoppingCartId)
            //                  select (int?)cartItems.cantidad *
            //                  cartItems.tb_producto.costo).Sum();

            return decimal.Zero;
        }

        //        public Order CreateOrder(Order order)
        //        {
        //            decimal orderTotal = 0;
        //            order.OrderDetails = new List<OrderDetail>();

        //            var cartItems = GetCartItems();
        //            // Iterate over the items in the cart, 
        //            // adding the order details for each
        //            foreach (var item in cartItems)
        //            {
        //                var orderDetail = new OrderDetail
        //                {
        //                    ItemId = item.ItemId,
        //                    OrderId = order.OrderId,
        //                    UnitPrice = item.Item.Price,
        //                    Quantity = item.Count
        //                };
        //                // Set the order total of the shopping cart
        //                orderTotal += (item.Count * item.Item.Price);
        //                order.OrderDetails.Add(orderDetail);
        //                storeDB.OrderDetails.Add(orderDetail);

        //            }

        //           // Set the order's total to the orderTotal count
        //            order.Total = orderTotal;

        //            // Save the order
        //            storeDB.SaveChanges();
        //            // Empty the shopping cart
        //            EmptyCart();
        //            // Return the OrderId as the confirmation number
        //            return order;
        //        }

        //        // Metodo que indica quien esta activo por el ID en el carrito de compras.
        public string GetCartId(HttpContextBase context)
                {
                    if (context.Session[CartSessionKey] == null)
                    {
                        if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                        {
                            context.Session[CartSessionKey] =
                                context.User.Identity.Name;
                        }
                        else
                        {
                            // Generate a new random GUID using System.Guid class
                            Guid tempCartId = Guid.NewGuid();
                            // Send tempCartId back to client as a cookie
                            context.Session[CartSessionKey] = tempCartId.ToString();
                        }
                    }
                    return context.Session[CartSessionKey].ToString();
                }

        //        // When a user has logged in, migrate their shopping cart to
        //        // be associated with their username
        //        public void MigrateCart(string userName)
        //        {
        //            var shoppingCart = storeDB.Carts.Where(
        //                c => c.CartId == ShoppingCartId);

        //            foreach (Cart item in shoppingCart)
        //            {
        //                item.CartId = userName;
        //            }
        //            // Guardar Cambios
        //            storeDB.SaveChanges();
        //        }
        //    }
    }
}