using System;

namespace Fullstack.NET.StoreIntegration.Contract
{
    public class Order
    {
        public readonly Guid Id;
        public readonly DateTimeOffset CreatedDate;
        public readonly Guid UserId;

        public Order(Guid id, DateTimeOffset createdDate, Guid userId)
        {
            this.Id = id;
            this.CreatedDate = createdDate;
            this.UserId = userId;
        }
    }
}
