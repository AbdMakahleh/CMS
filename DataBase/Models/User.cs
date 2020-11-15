using DataBase.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class User : Entity
    {

        public string Name { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public long? PolicyId { get; set; }

        public virtual Policy Policy { get; set; }
    }
}
