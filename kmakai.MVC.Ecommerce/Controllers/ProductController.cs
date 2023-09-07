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
}
