using Dapper;
using kmakai.MVC.Ecommerce.Data;
using kmakai.MVC.Ecommerce.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace kmakai.MVC.Ecommerce.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IConfiguration _configuration;
    public ProductRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        var products = await connection.QueryAsync<Product>("SELECT * FROM Products");
        foreach (var product in products)
        {
            product.Category = await connection.QueryFirstOrDefaultAsync<Category>("SELECT * FROM Categories WHERE CategoryId = @Id", new { Id = product.CategoryId });
        }

        return products.ToList();

    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        var sql = @"SELECT * FROM Products p
                    WHERE p.ProductId = @Id";

        var product = await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });

        product.Category = await connection.QueryFirstOrDefaultAsync<Category>("SELECT * FROM Categories WHERE CategoryId = @Id", new { Id = product.CategoryId });

        return product;

    }

}

