using Ecommerce.OrderService.Data;
using Ecommerce.OrderService.Kafka;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();

string conexao = "Server=localhost;Database=EcommerceOrder;Trusted_Connection=True;TrustServerCertificate=True;User ID=sa;Password='Senha123!';integrated security=false;";
builder.Services.AddDbContext<OrderDbContext>(options => 
options.UseSqlServer(conexao));
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

