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
        public tb_cliente asociado { get; set; }
        public decimal montoCompra { get; set; }
        public string numTarjeta { get; set; }
        public string tipoTarjeta { get; set; }
        public int codigoSeguridad { get; set; }
        public int mesVence { get; set; }
        public int annoVence { get; set; }
        public string direccion { get; set; }
    }
}