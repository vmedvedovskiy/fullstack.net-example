using System.Threading.Tasks;

namespace Fullstack.NET.Services.Orders
{
    public interface IOrderOpsService
    {
        Task CreateOrder(NewAnonymousOrderCommand newOrder);

        Task UpdateOrderAddress(UpdateOrderAddressCommand address);
    }
}
