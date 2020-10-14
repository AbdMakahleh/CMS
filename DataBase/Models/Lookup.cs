using DataBase.Entities;
using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Lookup : Entity
    {
        public Lookup()
        {
            InverseCmsmodule = new HashSet<Lookup>();
        }
        public string MajorCode { get; set; }
        public string Value { get; set; }
        public long CmsmoduleId { get; set; }
        public string Descreption { get; set; }

        public long Order { get; set; }


        public virtual Lookup Cmsmodule { get; set; }
        public virtual ICollection<Lookup> InverseCmsmodule { get; set; }
    }
}
