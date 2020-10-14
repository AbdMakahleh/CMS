using DataBase.Entities;
using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class PageSetup : Entity
    {
        public PageSetup()
        {
            Page = new HashSet<Page>();
            TemplatePage = new HashSet<TemplatePage>();
        }


        public string Name { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Seo { get; set; }
        public string Slug { get; set; }
        public bool IsPublished { get; set; }
        public long? MenuId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual ICollection<Page> Page { get; set; }
        public virtual ICollection<TemplatePage> TemplatePage { get; set; }
    }
}
