using System.Collections.Generic;
using TiendaVirtual.Models;
using System.ComponentModel.DataAnnotations;


namespace TiendaVirtual.ViewModels
{
    public class ShoppingCartViewModel
    {
        [Key]
        public List<tb_carrito> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}