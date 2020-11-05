using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Policy : Entity
    {
        public Policy()
        {
            PolicyRole = new HashSet<PolicyRole>();
            User = new HashSet<User>();
        }

        public string Name { get; set; }
  

        public virtual ICollection<PolicyRole> PolicyRole { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
