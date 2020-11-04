using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Configuration : Entity
    {

        public string Key { get; set; }
        public string Value { get; set; }
        public long CmsmoduleId { get; set; }
        public string Description { get; set; }
        public bool IsCached { get; set; }


        public virtual Cmsmodule Cmsmodule { get; set; }
    }
}
