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
    
    public partial class tb_acceso_aplicacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_acceso_aplicacion()
        {
            this.tb_asociado = new HashSet<tb_asociado>();
            this.tb_direccion = new HashSet<tb_direccion>();
        }
    
        public string correo_usuario { get; set; }
        public string contrasena { get; set; }
        public int id_tipo_usuario { get; set; }
        public int id_estado { get; set; }
        public Nullable<int> numero_intentos { get; set; }
    
        public virtual tb_estado tb_estado { get; set; }
        public virtual tb_tipo_usuario tb_tipo_usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_asociado> tb_asociado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_direccion> tb_direccion { get; set; }
    }
}
