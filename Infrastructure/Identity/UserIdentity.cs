using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity
{
  public class UserIdentity : ClaimsIdentity
    {
        public long Id { get; set; }
    }
}
