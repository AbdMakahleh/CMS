using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Role : Entity
    {
        public Role()
        {
            Permission = new HashSet<Permission>();
            PolicyRole = new HashSet<PolicyRole>();
        }


        public string Name { get; set; }
    
        public virtual ICollection<Permission> Permission { get; set; }
        public virtual ICollection<PolicyRole> PolicyRole { get; set; }
    }
}
