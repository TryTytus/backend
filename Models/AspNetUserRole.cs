using Microsoft.AspNetCore.Identity;

namespace backend.Models;

public class AspNetUserRole : IdentityUserRole<int>
{
    
    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}