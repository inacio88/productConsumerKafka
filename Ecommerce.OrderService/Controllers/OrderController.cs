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
    }
}