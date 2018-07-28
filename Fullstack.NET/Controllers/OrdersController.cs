using System;
using System.Threading.Tasks;
using Fullstack.NET.Authentication;
using Fullstack.NET.Services.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Fullstack.NET.Controllers
{
    [AuthenticateAttribute]
    [Route("api/v1/store/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrdersQueryService ordersQueryService;

        public OrdersController(IOrdersQueryService ordersQueryService)
            => this.ordersQueryService = ordersQueryService;

        [HttpGet]
        [Route("{userID:guid}")]
        public async Task<IActionResult> GetProducts(Guid userID)
        {
            var orders = await this.ordersQueryService.GetOrders(userID);

            return this.Ok(orders);
        }
    }
}
