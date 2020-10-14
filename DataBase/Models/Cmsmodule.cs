using DataBase.Entities;
using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Cmsmodule : Entity
    {
        public Cmsmodule()
        {
            Configuration = new HashSet<Configuration>();
        }

 
        public string Name { get; set; }

        public string Description { get; set; }
        public virtual ICollection<Configuration> Configuration { get; set; }
    }
}
