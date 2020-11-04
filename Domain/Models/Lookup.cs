using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Lookup : Entity
    {

        public string MajorCode { get; set; }
        public string Value { get; set; }
        public long CmsmoduleId { get; set; }
        public string Description { get; set; }
        public long Order { get; set; }
        public string MinorCode { get; set; }

        public virtual Cmsmodule Cmsmodule { get; set; }

    }
}
