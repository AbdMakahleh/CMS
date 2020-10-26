using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entity
{
   public interface IEntity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
