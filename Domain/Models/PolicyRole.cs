using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class PolicyRole : Entity
    {

        public long PolicyId { get; set; }
        public long RoleId { get; set; }

        public virtual Policy Policy { get; set; }
        public virtual Role Role { get; set; }
    }
}
