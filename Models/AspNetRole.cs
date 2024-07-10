using System;
using System.Collections.Generic;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend;

public partial class AspNetRole : IdentityRole<int>
{
    
    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();

    public virtual ICollection<AspNetUserRole> UserRoles { get; set; } = new List<AspNetUserRole>();
}
