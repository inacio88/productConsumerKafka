using System.Text.Json;
using Confluent.Kafka;
using Ecommerce.Model;
using Ecommerce.OrderService.Data;
using Ecommerce.OrderService.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(OrderDbContext orderDbContext, IKafkaProducer kafkaProducer) : ControllerBase
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

            await kafkaProducer.ProduceAsync("order-topic", new Message<string,string>{
                Key = orderModel.Id.ToString(),
                Value = JsonSerializer.Serialize(orderModel)
            });

            return orderModel;
        }
    }
}