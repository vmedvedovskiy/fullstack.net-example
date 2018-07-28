using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fullstack.NET.Database;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.NET.Services.Products
{
    public class ProductsQueryService : IProductsQueryService
    {
        private readonly StoreDbContext ctx;

        public ProductsQueryService(StoreDbContext ctx) => this.ctx = ctx;

        public async Task<IReadOnlyList<ProductModel>> GetProducts()
        {
            var data = await this.ctx
                .Set<Product>()
                .ToListAsync();

            return data
                .Select(_ => new ProductModel(_.Id, _.Name))
                .ToList();
        }
    }
}
