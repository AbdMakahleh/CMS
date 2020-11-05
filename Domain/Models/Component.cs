using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Component : Entity
    {
        public Component()
        {
            Widget = new HashSet<Widget>();
        }

   
        public string Html { get; set; }
        public string CustomCss { get; set; }
        public long? ModuleId { get; set; }
        public bool IsPublished { get; set; }

        public virtual Module Module { get; set; }
        public virtual ICollection<Widget> Widget { get; set; }
    }
}
