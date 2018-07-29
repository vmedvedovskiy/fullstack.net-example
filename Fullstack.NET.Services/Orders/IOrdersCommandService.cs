using System.Threading.Tasks;

namespace Fullstack.NET.Services.Orders
{
    public interface IOrdersCommandService
    {
        Task CreateOrder(NewOrderCommand createCommand);
    }
}
