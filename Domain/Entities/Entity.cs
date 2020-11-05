using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Entity : IEntity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
