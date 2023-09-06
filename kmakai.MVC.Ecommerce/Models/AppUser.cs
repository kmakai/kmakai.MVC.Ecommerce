using Microsoft.AspNetCore.Identity;

namespace kmakai.MVC.Ecommerce.Models;

public class AppUser: IdentityUser
{
    public ICollection<Order> Orders { get; set; } = null!;

}
