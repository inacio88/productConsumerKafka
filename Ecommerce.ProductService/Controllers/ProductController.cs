using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model;
using Ecommerce.ProductService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(ProductDbContext productDbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<List<ProductModel>> GetProducts()
        {
            return await productDbContext.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ProductModel> GetProductModelAsync(int id)
        {
            return await productDbContext.Products.FindAsync(id);
        }
    }
}