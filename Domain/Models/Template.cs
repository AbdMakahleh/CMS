using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Template : Entity
    {
        public Template()
        {
            TemplatePage = new HashSet<TemplatePage>();
        }

        public string Name { get; set; }
        public string TemplateCssPath { get; set; }
        public bool IsPublished { get; set; }

        public virtual ICollection<TemplatePage> TemplatePage { get; set; }
    }
}
