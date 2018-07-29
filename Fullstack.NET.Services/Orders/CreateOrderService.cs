using System.Threading.Tasks;
using Fullstack.NET.Services.Authentication;

namespace Fullstack.NET.Services.Orders
{
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IUsersQueryService usersQueryService;
        private readonly IOrdersCommandService ordersCommandService;

        public CreateOrderService(
            IUsersQueryService usersQueryService, 
            IOrdersCommandService ordersCommandService)
        {
            this.usersQueryService = usersQueryService;
            this.ordersCommandService = ordersCommandService;
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
    }
}
