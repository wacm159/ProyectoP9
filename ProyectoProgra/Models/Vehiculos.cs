//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoProgra.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vehiculos
    {
        public int id_vehiculo { get; set; }
        public string placa { get; set; }
        public string marca { get; set; }
        public int modelo { get; set; }
        public int cilindros { get; set; }
        public int llantas { get; set; }
        public int precio { get; set; }
        public Nullable<int> total { get; set; }
        public int id_cliente { get; set; }
    
        public virtual Cliente Cliente { get; set; }
    }
}