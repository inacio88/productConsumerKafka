using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductService.Data
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext(DbContextOptions options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel {Id = 1, Name = "Shirt", Quantity = 2, Price = 12.34m});
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel {Id = 2, Name = "Pant", Quantity = 45, Price = 152.34m});
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel {Id = 3, Name = "Polo", Quantity = 34, Price = 523.34m});
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProductModel> Products {get;set;}
    }
}