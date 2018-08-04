using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fullstack.NET.Services.Orders
{
    public interface IOrdersQueryService
    {
        Task<IReadOnlyList<OrderModel>> GetOrders(Guid userID);

        Task<OrderModel> GetOrder(Guid orderId);
    }
}
