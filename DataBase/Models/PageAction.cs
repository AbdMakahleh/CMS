using DataBase.Entities;
using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class PageAction : Entity
    {
        public PageAction()
        {
            Permission = new HashSet<Permission>();
        }

        public string Name { get; set; }
        public long ActionType { get; set; }
        public bool IsPublished { get; set; }
        public long? PageId { get; set; }
        public long? PageSectionId { get; set; }

        public virtual Page Page { get; set; }
        public virtual PageSection PageSection { get; set; }
        public virtual ICollection<Permission> Permission { get; set; }
    }
}
