using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fullstack.NET.Controllers
{
    public interface IStoreQueryService
    {
        Task<IReadOnlyList<StoreItem>> GetPurchases(Guid userID);
    }
}