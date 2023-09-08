using kmakai.MVC.Ecommerce.Models;

namespace kmakai.MVC.Ecommerce.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateOrder(Order order);
    void AddOrderItem(OrderItem item);
    Task<AppUser> GetAppUserByName(string? name);
}
