using System.Threading.Tasks;
using Fullstack.NET.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Fullstack.NET.Controllers
{
    [Route("api/v1/store")]
    public class ProductsController : Controller
    {
        private readonly IProductsQueryService storeQueryService;

        public ProductsController(IProductsQueryService storeQueryService) 
            => this.storeQueryService = storeQueryService;

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetProducts()
        {
            var goods = await this.storeQueryService.GetProducts();

            return this.Ok(goods);
        }
    }
}
