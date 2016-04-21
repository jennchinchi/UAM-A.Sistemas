using TiendaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaVirtual.ViewModels
{
    public class ProcesarCompra
    {
        public string Message { get; set; }
        public decimal saldo { get; set; }
        public tb_asociado asociado { get; set; }
        public decimal montoCompra { get; set; }
    }
}