namespace kmakai.MVC.Ecommerce.Models;

public class CartItem
{
    public string name { get; set; } = null!;
    public int quantity { get; set; }
    public decimal price { get; set; }
    public int productId { get; set; }

}
