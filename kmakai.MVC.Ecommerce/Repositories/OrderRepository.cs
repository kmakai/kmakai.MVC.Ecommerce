using Dapper;
using kmakai.MVC.Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace kmakai.MVC.Ecommerce.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;
        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var sql = @"INSERT INTO Orders (OrderDate, CheckoutComplete, AppUserId) VALUES (@OrderDate, @CheckoutComplete, @AppUserId); SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = connection.QuerySingle<int>(sql, order);

            var newOrder = await connection.QueryFirstOrDefaultAsync<Order>("SELECT * FROM Orders WHERE OrderId = @Id", new { Id = id });

            return newOrder;
        }

        public async void AddOrderItem(OrderItem item)
        {
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));


            var sql = @"Select * FROM Orders where OrderId = @Id";

            var order = await connection.QueryFirstOrDefaultAsync<Order>(sql, new { Id = item.OrderId });


            Console.WriteLine(order);



        }
    }
}
