﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Translanza.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TranslanzaEntities : DbContext
    {
        public TranslanzaEntities()
            : base("name=TranslanzaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agrupacion> Agrupacion { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }
        public virtual DbSet<AfiliadoConductor> AfiliadoConductor { get; set; }
        public virtual DbSet<EspecialidadConductor> EspecialidadConductor { get; set; }
        public virtual DbSet<ObservacionConductor> ObservacionConductor { get; set; }
        public virtual DbSet<ObservacionVehiculo> ObservacionVehiculo { get; set; }
        public virtual DbSet<Tercero> Tercero { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolMenu> RolMenu { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
