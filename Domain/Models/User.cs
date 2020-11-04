using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class User : Entity
    {

        public string Name { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public long? PolicyId { get; set; }

        public virtual Policy Policy { get; set; }
    }
}
