using DataBase.Entities;
using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class PageSection : Entity
    {
        public PageSection()
        {
            PageAction = new HashSet<PageAction>();
        }

   
        public long? WidgetId { get; set; }
        public long? PageId { get; set; }
        public string Name { get; set; }
        public long? ComponentId { get; set; }
        public string Html { get; set; }
        public string CustomCss { get; set; }
        public string CustomJs { get; set; }
        public long? ModuleId { get; set; }
        public bool IsPublished { get; set; }

        public virtual Module Module { get; set; }
        public virtual Page Page { get; set; }
        public virtual Widget Widget { get; set; }
        public virtual ICollection<PageAction> PageAction { get; set; }
    }
}
