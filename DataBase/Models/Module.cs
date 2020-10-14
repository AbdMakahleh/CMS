using DataBase.Entities;
using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Module : Entity
    {
        public Module()
        {
            Component = new HashSet<Component>();
            PageSection = new HashSet<PageSection>();
        }

        public string Name { get; set; }
        public bool IsPublished { get; set; }

        public virtual ICollection<Component> Component { get; set; }
        public virtual ICollection<PageSection> PageSection { get; set; }
    }
}
