//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class AfiliadoConductor
    {
        public int RowID { get; set; }
        public int AfiliadoID { get; set; }
        public int ConductorID { get; set; }
        public Nullable<bool> Activo { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
    
        public virtual Tercero Tercero { get; set; }
        public virtual Tercero Tercero1 { get; set; }
    }
}
