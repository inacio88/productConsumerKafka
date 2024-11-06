using Ecommerce.Common;
using Ecommerce.ProductService.Data;
using Ecommerce.ProductService.Kafka;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();
builder.Services.AddHostedService<ProductConsumer>();

string conexao = "Server=localhost;Database=EcommerceProduct;Trusted_Connection=True;TrustServerCertificate=True;User ID=sa;Password='Senha123!';integrated security=false;";
builder.Services.AddDbContext<ProductDbContext>(option => 
option.UseSqlServer(conexao));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

