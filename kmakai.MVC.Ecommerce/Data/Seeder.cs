using System.Text.Json.Serialization;
using Newtonsoft.Json;
using kmakai.MVC.Ecommerce.Models;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace kmakai.MVC.Ecommerce.Data;

public class Seeder
{

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new EcommerceContext(
                       serviceProvider.GetRequiredService<DbContextOptions<EcommerceContext>>());

        context.Database.EnsureCreated();

        if (!context.Categories.Any())
        {
            Console.WriteLine("No categories found, seeding database...");
            GetCategories(context).Wait();
        }

        if (!context.Products.Any())
        {
            GetProducts(context).Wait();
            Console.WriteLine("No products found, seeding database...");
        }
    }




    public async static Task GetProducts(EcommerceContext context)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.GetAsync("https://fakestoreapi.com/products");
        var responseString = await response.Content.ReadAsStringAsync();

        var products = JsonConvert.DeserializeObject<List<JsonProductObject>>(responseString);

        var categories = context.Categories;

        foreach (var product in products!)
        {
            context.Products.Add(new Product
            {
                Name = product.title,
                Description = product.description,
                Price = (decimal)product.price,
                ImageUrl = product.image,
                Rating = product.rating.rate,
                CategoryId = categories!.FirstOrDefault(c => c.Name == product.category)!.CategoryId
            });
        }

        await context.SaveChangesAsync();

    }

    public async static Task GetCategories(EcommerceContext context)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.GetAsync("https://fakestoreapi.com/products/categories");
        var responseString = await response.Content.ReadAsStringAsync();

        var categories = JsonConvert.DeserializeObject<List<string>>(responseString);

        foreach (var category in categories!)
        {
            context.Categories.Add(new Category { Name = category });
        }

        await context.SaveChangesAsync();

    }

}



public class JsonProductObject
{
    [JsonPropertyName("id")]
    public int id { get; set; }

    [JsonPropertyName("title")]
    public string title { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public double price { get; set; }

    [JsonPropertyName("description")]
    public string description { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string category { get; set; } = string.Empty;

    [JsonPropertyName("image")]
    public string image { get; set; } = string.Empty;

    [JsonPropertyName("rating")]
    public JsonRatingObject rating { get; set; } = new JsonRatingObject();
}

public class JsonRatingObject
{
    [JsonPropertyName("rate")]
    public double rate { get; set; }

    [JsonPropertyName("count")]
    public int count { get; set; }
}