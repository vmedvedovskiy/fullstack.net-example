using System.Threading.Tasks;

namespace Fullstack.NET.Services.Orders
{
    public interface ICreateOrderService
    {
        Task CreateOrder(NewAnonymousOrderCommand newOrder);
    }
}
