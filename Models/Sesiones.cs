//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdventureWorksPhotos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sesiones
    {
        public int IdSesion { get; set; }
        public int IdUsuario { get; set; }
        public string TokenSesion { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public string TipoSesion { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}