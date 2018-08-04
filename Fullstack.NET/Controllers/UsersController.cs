using System;
using System.Threading.Tasks;
using Fullstack.NET.Authentication;
using Fullstack.NET.Services.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Fullstack.NET.Controllers
{
    [Authenticate]
    [Route("api/v1/store/users/{userID:guid}")]
    public class UsersController : Controller
    {
        private readonly IOrdersQueryService ordersQueryService;

        public UsersController(
            IOrdersQueryService ordersQueryService)
        {
            this.ordersQueryService = ordersQueryService;
        }

        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult> GetOrders(Guid userID)
        {
            var orders = await this.ordersQueryService.GetOrders(userID);

            return this.Ok(orders);
        }
    }
}
