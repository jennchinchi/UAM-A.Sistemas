//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TiendaVirtual.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_detalle
    {
        public int id_detalle { get; set; }
        public int id_producto { get; set; }
        public int id_factura { get; set; }
        public int cantidad { get; set; }
        public decimal costo_total { get; set; }
    
        public virtual tb_factura tb_factura { get; set; }
        public virtual tb_producto tb_producto { get; set; }
    }
}