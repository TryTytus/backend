using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace backend;

public partial class AspNetRoleClaim : IdentityRoleClaim<int>
{
    public virtual AspNetRole Role { get; set; } = null!;
}
