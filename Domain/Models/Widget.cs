using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Widget : Entity
    { 
        public Widget()
        {
            PageSection = new HashSet<PageSection>();
        }


        public string Name { get; set; }
        public string Html { get; set; }
        public string CustomCss { get; set; }
        public string CustomJs { get; set; }

        public bool IsPublished { get; set; }
        public long? ComponentId { get; set; }

        public virtual Component Component { get; set; }
        public virtual ICollection<PageSection> PageSection { get; set; }
    }
}
