using TiendaVirtual.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TiendaVirtual.ViewModel
{
    public class ShoppingCartViewModel
    {
        [Key]
        public List<tb_carrito> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}