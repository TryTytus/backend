using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace backend;

public partial class AspNetUserLogin : IdentityUserLogin<int>
{
    public virtual AspNetUser User { get; set; } = null!;
}
