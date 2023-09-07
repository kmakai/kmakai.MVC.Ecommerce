using Dapper;
using kmakai.MVC.Ecommerce.Data;
using kmakai.MVC.Ecommerce.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace kmakai.MVC.Ecommerce.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IConfiguration Configuration;
    public ProductRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }
  
    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        using IDbConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
        var products = await connection.QueryAsync<Product>("SELECT * FROM Products");

        return products.ToList();
        
    }
}
