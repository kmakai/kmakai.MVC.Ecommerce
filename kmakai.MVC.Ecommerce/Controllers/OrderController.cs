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

    [Route("order/checkout")]
    [HttpGet]
    public IActionResult CheckoutOrder()
    {
        return View();
    }



    [Route("order/checkout")]
    [HttpPost]
    public async Task<IActionResult> CheckoutOrder([FromBody] List<CartItem> items)
    {
        foreach (var item in items) Console.WriteLine(item.name);
        var subTotal = items.Sum(x => x.price * x.quantity);
        var tax = subTotal * (decimal)0.08;

        List<OrderItem> orderItems = new List<OrderItem>();
        var order = new Order
        {
            OrderDate = DateTime.Now,
            CheckoutComplete = true,
            Total = subTotal + tax,
            AppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
        };

        var createdOrder = await _orderRepository.CreateOrder(order);

        foreach(var item in items)
        {
            var orderItem = new OrderItem
            {
                ProductId = item.productId,
                Quantity = item.quantity,
                Price = item.price,
                OrderId = createdOrder.OrderId,
           
            };

            orderItems.Add(orderItem);
            _orderRepository.AddOrderItem(orderItem);
        }


        return new JsonResult(new
        {
            products = orderItems,
            total = subTotal + tax,
            user = User.FindFirstValue(ClaimTypes.NameIdentifier)
        });

    }

}


