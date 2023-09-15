using kmakai.MVC.Ecommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace kmakai.MVC.Ecommerce.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }


    [Route("product/{id}")]
    public async Task<IActionResult> Product(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }


        return View(product);
    }

    [Route("products/search")]
    public async Task<IActionResult> Search([FromQuery]string searchValue, [FromQuery] int searchCategory)
    {
        Console.WriteLine(searchCategory);
        if (string.IsNullOrEmpty(searchValue))
        {
            return BadRequest();
        }

        var products = await _productRepository.GetProductsAsync();
        products = products.Where(p => p.Name.ToLower().Contains(searchValue.ToLower()));
        Console.WriteLine(products.Count());

        if(searchCategory != 0)
        {
            products = products.Where(p => p.Category.CategoryId == searchCategory);
        }

       return Json(products);
    }
}
