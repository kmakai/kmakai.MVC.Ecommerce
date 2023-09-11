using kmakai.MVC.Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using kmakai.MVC.Ecommerce.Repositories;
using System.Security.Claims;

namespace kmakai.MVC.Ecommerce.Controllers;

public class OrderController : Controller
{
    private readonly IOrderRepository _orderRepository;
    private readonly UserManager<AppUser> _userManager;

    public OrderController(IOrderRepository orderRepository, UserManager<AppUser> userManager)
    {
        _orderRepository = orderRepository;
        _userManager = userManager;
    }


    [Route("order/create")]
    public async Task<IActionResult> CreateOrder()
    {
        //var user = await _userManager.GetUserAsync(User);
        var order = new Order
        {
            OrderDate = DateTime.Now,
            CheckoutComplete = false,
            AppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
        };
        //Console.WriteLine(user.Id);

        var createdOrder = await _orderRepository.CreateOrder(order);
        return Json(createdOrder);
    }

}


