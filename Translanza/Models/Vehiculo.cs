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
    
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            this.ObservacionVehiculo = new HashSet<ObservacionVehiculo>();
        }
    
        public int RowID { get; set; }
        public string Placa { get; set; }
        public Nullable<int> MarcaID { get; set; }
        public string Modelo { get; set; }
        public string Linea { get; set; }
        public Nullable<int> TipoID { get; set; }
        public Nullable<int> NoPasajeros { get; set; }
        public Nullable<int> CombustibleID { get; set; }
        public Nullable<int> TerceroID { get; set; }
        public string Img_TarjetaOperacion { get; set; }
        public Nullable<System.DateTime> Vencimiento_Tecnomecanica { get; set; }
        public string Img_Tecnomecanica { get; set; }
        public Nullable<System.DateTime> Vencimiento_Soat { get; set; }
        public string Img_Soat { get; set; }
        public Nullable<System.DateTime> Vencimiento_SeguroCivil { get; set; }
        public string Img_SeguroCivil { get; set; }
        public string Img_TarjetaPropiedad { get; set; }
        public string Img_Vehiculo { get; set; }
        public Nullable<bool> Activo { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
    
        public virtual Tipo Tipo { get; set; }
        public virtual Tipo Tipo1 { get; set; }
        public virtual Tipo Tipo2 { get; set; }
        public virtual Tipo Tipo3 { get; set; }
        public virtual Tipo Tipo4 { get; set; }
        public virtual ICollection<ObservacionVehiculo> ObservacionVehiculo { get; set; }
        public virtual Tercero Tercero { get; set; }
    }
}
