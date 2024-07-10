using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace backend;

public partial class AspNetUserToken : IdentityUserToken<int>
{
    public virtual AspNetUser User { get; set; } = null!;
}
