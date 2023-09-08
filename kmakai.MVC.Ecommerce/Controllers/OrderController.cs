using kmakai.MVC.Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using kmakai.MVC.Ecommerce.Repositories;

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
        var order = new Order
        {
            OrderDate = DateTime.Now,
            CheckoutComplete = false,
            AppUserId = "d17f50d9-5813-4805-8a83-93d72e483db5"
        };
       var createdOrder = await _orderRepository.CreateOrder(order);
        return Json(createdOrder);
    }

}

    
