//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfTicketera
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ticket
    {
        public int Id_Ticket { get; set; }
        public string Nro_Ticket { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public Nullable<int> Id_Cliente { get; set; }
        public Nullable<int> Id_Caja { get; set; }
    
        public virtual Caja Caja { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
