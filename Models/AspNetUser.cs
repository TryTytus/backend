using System;
using System.Collections.Generic;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend;

public partial class AspNetUser : IdentityUser<int>
{

    public string Name { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<AspNetUserRole> UserRoles { get; set; } = new List<AspNetUserRole>();
}
