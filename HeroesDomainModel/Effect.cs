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
    
    public partial class Effect
    {
        public Effect()
        {
            this.Ability_Effect = new HashSet<Ability_Effect>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string helper { get; set; }
    
        public virtual ICollection<Ability_Effect> Ability_Effect { get; set; }
    }
}
