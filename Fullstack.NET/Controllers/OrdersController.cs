using System;
using System.Net;
using System.Threading.Tasks;
using Fullstack.NET.Authentication;
using Fullstack.NET.Models;
using Fullstack.NET.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fullstack.NET.Controllers
{
    [Authenticate]
    [Route("api/v1/store/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrdersQueryService ordersQueryService;
        private readonly IOrdersCommandService ordersCommandService;

        public OrdersController(
            IOrdersQueryService ordersQueryService,
            IOrdersCommandService ordersCommandService)
        {
            this.ordersQueryService = ordersQueryService;
            this.ordersCommandService = ordersCommandService;
        }

        [HttpGet]
        [Route("{userID:guid}")]
        public async Task<IActionResult> GetOrders(Guid userID)
        {
            var orders = await this.ordersQueryService.GetOrders(userID);

            return this.Ok(orders);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> CreateOrder([FromBody] NewOrderModel newOrder)
        {
            var createOrderCommand = new NewOrderCommand(
                newOrder.SelectedProductIds,
                Guid.NewGuid());

            await this.ordersCommandService.CreateOrder(createOrderCommand);

            return this.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
