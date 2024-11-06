using System.Text.Json;
using Confluent.Kafka;
using Ecommerce.Common;
using Ecommerce.Model;
using Ecommerce.OrderService.Data;
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
            orderModel.Status = "Pending";
            orderDbContext.Orders.Add(orderModel);
            await orderDbContext.SaveChangesAsync();

            var orderMessage = new OrderMessage
            {
                OrderId = orderModel.Id,
                ProductId = orderModel.ProductId,
                Quantity = orderModel.Quantity
            };

            await kafkaProducer.ProduceAsync("order-created", orderMessage);

            return orderModel;
        }
    }
}