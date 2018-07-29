using System;
using System.Collections.Generic;

namespace Fullstack.NET.Services.Orders
{
    public class NewOrderCommand
    {
        public readonly IReadOnlyList<Guid> ProductIds;
        public readonly Guid UserId;

        public NewOrderCommand(
            IReadOnlyList<Guid> productIds, 
            Guid userId)
        {
            this.ProductIds = productIds;
            this.UserId = userId;
        }
    }
}
