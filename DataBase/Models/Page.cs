using DataBase.Entities;
using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Page : Entity
    {
        public Page()
        {
            PageAction = new HashSet<PageAction>();
            PageSection = new HashSet<PageSection>();
        }

        public string Html { get; set; }
        public long? PageSetupId { get; set; }
        public string CustomCss { get; set; }
        public string CustomerJs { get; set; }
        public long? MenuId { get; set; }

        public virtual PageSetup PageSetup { get; set; }
        public virtual ICollection<PageAction> PageAction { get; set; }
        public virtual ICollection<PageSection> PageSection { get; set; }
    }
}
