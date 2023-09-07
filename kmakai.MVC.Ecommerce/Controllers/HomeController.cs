using kmakai.MVC.Ecommerce.Data;
using kmakai.MVC.Ecommerce.Models;
using kmakai.MVC.Ecommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace kmakai.MVC.Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;
        public HomeController(IProductRepository productRepo)
        {
            productRepository = productRepo;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productRepository.GetProductsAsync();
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