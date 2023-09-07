using kmakai.MVC.Ecommerce.Data;
using kmakai.MVC.Ecommerce.Models;
using kmakai.MVC.Ecommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace kmakai.MVC.Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        public HomeController(IProductRepository productRepo)
        {
            _productRepository = productRepo;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetProductsAsync();
            products = products.Where(p => p.Rating > 4.5);
           
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}