using kmakai.MVC.Ecommerce.Models;

namespace kmakai.MVC.Ecommerce.Repositories;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetProductsAsync();
}
