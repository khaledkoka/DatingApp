using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUserRole : IdentityUserRole<int>
    {
        // Specify the JOIN entities needed for this
        public AppUser User { get; set; }
        public AppRole Role { get; set; }
    }
}