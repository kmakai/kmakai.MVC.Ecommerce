using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kmakai.MVC.Ecommerce.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public bool CheckoutComplete { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = null!;

    public string AppUserId { get; set; } = null!;

    [ForeignKey("AppUserId")]
    public AppUser AppUser { get; set; } = null!;
}
