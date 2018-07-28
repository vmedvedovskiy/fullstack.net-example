using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fullstack.NET.Controllers;

namespace Fullstack.NET.Services
{
    public class StoreQueryService : IStoreQueryService
    {
        public Task<IReadOnlyList<StoreItem>> GetPurchases(Guid userID)
        {
            IReadOnlyList<StoreItem> goods = new List<StoreItem>
            {
                new StoreItem("some goody")
            };

            return Task.FromResult(goods);
        }
    }
}
