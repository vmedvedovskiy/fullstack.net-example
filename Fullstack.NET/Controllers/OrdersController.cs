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
        private readonly IOrderOpsService orderOpsService;

        public OrdersController(
            IOrdersQueryService ordersQueryService,
            IOrderOpsService createOrderService)
        {
            this.ordersQueryService = ordersQueryService;
            this.orderOpsService = createOrderService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> CreateOrder(
            [FromBody] NewOrderModel newOrder)
        {
            var createOrderCommand = new NewOrderCommand(
                newOrder.SelectedProductIds,
                Guid.NewGuid());

            await this.orderOpsService.CreateOrder(
                new NewAnonymousOrderCommand(
                    newOrder.PhoneNumber,
                    newOrder.SelectedProductIds));

            return this.StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("{orderId:guid}/address")]
        public async Task<IActionResult> UpdateOrderAddress(
            Guid orderId,
            [FromBody] NewAddressModel newAddress)
        {
            var updateAddressCommand = new UpdateOrderAddressCommand(
                orderId,
                newAddress.DeptNumber,
                newAddress.City,
                newAddress.Street,
                newAddress.StreetNumber);

            await this.orderOpsService.UpdateOrderAddress(updateAddressCommand);

            return this.StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}
