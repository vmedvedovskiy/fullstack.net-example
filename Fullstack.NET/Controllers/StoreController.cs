using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Fullstack.NET.Controllers
{
    [Route("api/store")]
    public class StoreController : Controller
    {
        private readonly IStoreQueryService storeQueryService;

        public StoreController(IStoreQueryService storeQueryService) 
            => this.storeQueryService = storeQueryService;

        [HttpGet]
        [Route("{userID:guid}/purchases")]
        public async Task<JsonResult> GetPurchasedItems(Guid userID)
        {
            var goods = await this.storeQueryService.GetPurchases(userID);

            return this.Json(goods);
        }
    }
}
