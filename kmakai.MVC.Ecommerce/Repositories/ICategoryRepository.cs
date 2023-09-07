using kmakai.MVC.Ecommerce.Models;

namespace kmakai.MVC.Ecommerce.Repositories;

public interface ICategoryRepository
{
    public Task<IEnumerable<Category>> GetCategoriesAsync();
    public Task<Category> GetCategoryByIdAsync(int id);
}
