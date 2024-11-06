using Confluent.Kafka;
using Ecommerce.Common;
using Ecommerce.Model;
using Ecommerce.ProductService.Data;
using Newtonsoft.Json;

namespace Ecommerce.ProductService.Kafka
{
    public class ProductConsumer(IServiceProvider serviceProvider, IKafkaProducer kafkaProducer) : KafkaConsumer(topics)
    {
        private static readonly string[] topics = ["order-created"];
        private ProductDbContext GetDbContext()
        {
            var scope = serviceProvider.CreateAsyncScope();
            return scope.ServiceProvider.GetRequiredService<ProductDbContext>();
        }

        protected override async Task ConsumeAsync(ConsumeResult<string, string> consumeResult)
        {
            await base.ConsumeAsync(consumeResult);

            switch (consumeResult.Topic)
            {
                
                case "order-created":
                    await HandleOrderCreated(consumeResult.Message.Value);
                    break;
            }
        }

        private async Task HandleOrderCreated(string message)
        {
            var orderMessage = JsonConvert.DeserializeObject<OrderMessage>(message);
            var isReserved = await ReserveProducts(orderMessage);
            if (isReserved)
            {
                await kafkaProducer.ProduceAsync("products-reserved", orderMessage);
            }
            else
            {
                await kafkaProducer.ProduceAsync("products-reservation-failed", orderMessage);
            }
        }

        private async Task<bool> ReserveProducts(OrderMessage? orderMessage)
        {
            using var dbContext = GetDbContext();
            var product = await dbContext.Products.FindAsync(orderMessage.ProductId);
            if (product is not null && product.Quantity >= orderMessage.Quantity)
            {
                product.Quantity -= orderMessage.Quantity;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}