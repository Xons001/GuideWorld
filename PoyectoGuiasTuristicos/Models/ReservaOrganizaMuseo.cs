//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PoyectoGuiasTuristicos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReservaOrganizaMuseo
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public int museo_id { get; set; }
    
        public virtual Museo Museo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}