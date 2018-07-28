using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fullstack.NET.Services.Products
{
    public interface IProductsQueryService
    {
        Task<IReadOnlyList<ProductModel>> GetProducts();
    }
}