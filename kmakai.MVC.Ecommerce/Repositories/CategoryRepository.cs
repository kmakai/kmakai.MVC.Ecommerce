using Dapper;
using kmakai.MVC.Ecommerce.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace kmakai.MVC.Ecommerce.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IConfiguration _configuration;

    public CategoryRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var categories = await connection.QueryAsync<Category>("SELECT * FROM Categories");

        return categories.ToList();

    }

    public Task<Category> GetCategoryByIdAsync(int id)
    {
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var category = connection.QueryFirstOrDefaultAsync<Category>("SELECT * FROM Categories WHERE CategoryId = @Id", new { Id = id });

        return category;

    }
}
