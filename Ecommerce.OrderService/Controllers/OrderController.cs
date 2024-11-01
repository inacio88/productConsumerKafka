using Ecommerce.Model;
using Ecommerce.OrderService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(OrderDbContext orderDbContext) : ControllerBase
    {
        [HttpGet]
        public Task<List<OrderModel>> GetOrderModels()
        {
            return orderDbContext.Orders.ToListAsync();
        }

        [HttpPost]
        public async Task<OrderModel> CreateOrder(OrderModel orderModel)
        {
            orderModel.OrderDate = DateTime.Now.ToUniversalTime();
            orderDbContext.Orders.Add(orderModel);
            await orderDbContext.SaveChangesAsync();

            return orderModel;
        }
    }
}