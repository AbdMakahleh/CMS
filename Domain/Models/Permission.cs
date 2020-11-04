using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Permission : Entity
    {
        public long PageActionId { get; set; }
        public long RoleId { get; set; }

        public virtual PageAction PageAction { get; set; }
        public virtual Role Role { get; set; }
    }
}
