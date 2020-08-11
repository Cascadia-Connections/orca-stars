using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OrcaStarsWebApplication.Areas.Identity.Data
{
    public class OrcaStarsWebApplicationRole : IdentityRole
    {
        OrcaStarsWebApplicationRole Admin = new OrcaStarsWebApplicationRole()
        {
            Id = "1",
            Name = "Admin",
            NormalizedName = "ADMIN"
        };
    }
}
