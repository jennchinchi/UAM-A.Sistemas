﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class bd_tienda_virtual_dellEntities : DbContext
    {
        public bd_tienda_virtual_dellEntities()
            : base("name=bd_tienda_virtual_dellEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tb_asociado> tb_asociado { get; set; }
        public virtual DbSet<tb_carrito> tb_carrito { get; set; }
        public virtual DbSet<tb_categoria> tb_categoria { get; set; }
        public virtual DbSet<tb_detalle> tb_detalle { get; set; }
        public virtual DbSet<tb_direccion> tb_direccion { get; set; }
        public virtual DbSet<tb_estado> tb_estado { get; set; }
        public virtual DbSet<tb_factura> tb_factura { get; set; }
        public virtual DbSet<tb_persona> tb_persona { get; set; }
        public virtual DbSet<tb_producto> tb_producto { get; set; }
        public virtual DbSet<tb_provincia> tb_provincia { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
    }
}
