using kmakai.MVC.Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using kmakai.MVC.Ecommerce.Repositories;

namespace kmakai.MVC.Ecommerce.Controllers;

public class OrderController : Controller
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }


    [Route("order/create")]
    public async Task<IActionResult> CreateOrder()
    {
        var order = new Order() { 
            OrderDate = DateTime.Now,
            CheckoutComplete = false,
            AppUser = await _orderRepository.GetAppUserByName(User.Identity.Name)

        };
        order = await _orderRepository.CreateOrder(order);

        return Json(order);


    }
}
