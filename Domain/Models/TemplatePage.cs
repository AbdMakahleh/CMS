using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class TemplatePage : Entity
    {

        public long? PageSetupId { get; set; }
        public long? TemplateId { get; set; }


        public virtual PageSetup PageSetup { get; set; }
        public virtual Template Template { get; set; }
    }
}
