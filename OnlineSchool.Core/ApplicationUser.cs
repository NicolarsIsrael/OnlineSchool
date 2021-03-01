using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineSchool.Core
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }
        public bool IsAccountSetUpComplete { get; set; }
        //public bool IsBasicAccountSetupComplete { get; set; }
    }
}
