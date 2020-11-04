using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Cmsmodule : Entity
    {
        public Cmsmodule()
        {
            Configuration = new HashSet<Configuration>();
            Lookup = new HashSet<Lookup>();
        }

 
        public string Name { get; set; }

        public string Description { get; set; }
        public virtual ICollection<Configuration> Configuration { get; set; }
        public virtual ICollection<Lookup> Lookup { get; set; }
    }
}
