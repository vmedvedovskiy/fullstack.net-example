using System.Threading.Tasks;
using Fullstack.NET.Services.Address;
using Fullstack.NET.Services.Authentication;

namespace Fullstack.NET.Services.Orders
{
    public class OrderOpsService : IOrderOpsService
    {
        private readonly IUsersQueryService usersQueryService;
        private readonly IOrdersCommandService ordersCommandService;
        private readonly IAddressCommandService addressCommandService;
        private readonly IOrdersQueryService ordersQueryService;

        public OrderOpsService(
            IUsersQueryService usersQueryService, 
            IOrdersCommandService ordersCommandService,
            IAddressCommandService addressCommandService,
            IOrdersQueryService ordersQueryService)
        {
            this.usersQueryService = usersQueryService;
            this.ordersCommandService = ordersCommandService;
            this.addressCommandService = addressCommandService;
            this.ordersQueryService = ordersQueryService;
        }

        public async Task CreateOrder(NewAnonymousOrderCommand newOrder)
        {
            var user = await this.usersQueryService
                .Find(newOrder.PhoneNumber);

            if(user == null)
            {
                // TODO: Create user
            }

            await this.ordersCommandService.CreateOrder(
                new NewOrderCommand(newOrder.SelectedProductIds, user.Id));
        }

        public async Task UpdateOrderAddress(UpdateOrderAddressCommand updateAdressCommand)
        {
            var order = await this.ordersQueryService
                .GetOrder(updateAdressCommand.OrderId);

            var user = await this.usersQueryService
                .Find(order.UserId);

            await this.addressCommandService.CreateAddress(
                new NewAddressCommand(
                    user.Id,
                    updateAdressCommand.DeptNumber,
                    updateAdressCommand.City,
                    updateAdressCommand.Street,
                    updateAdressCommand.StreetNumber));
        }
    }
}
