//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeroesDomainModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Build
    {
        public Build()
        {
            this.Talent = new HashSet<Talent>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int hero_id { get; set; }
    
        public virtual ICollection<Talent> Talent { get; set; }
        public virtual Hero Hero { get; set; }
    }
}
