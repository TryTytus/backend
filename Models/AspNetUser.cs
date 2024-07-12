using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace backend;

public partial class AspNetUser : IdentityUser<int>
{

    public string Name { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public DateTime CreatedAt { get; set; }



    public virtual ICollection<Post> Posts { get; set; } = default!;


    public ICollection<AspNetUserClaim>? AspNetUserClaims { get; set; }


    public ICollection<AspNetUserLogin>? AspNetUserLogins { get; set; }


    public ICollection<AspNetUserToken>? AspNetUserTokens { get; set; }
    


    public ICollection<AspNetUserRole>? UserRoles { get; set; }
}
