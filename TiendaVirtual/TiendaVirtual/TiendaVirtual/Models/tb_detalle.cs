//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
