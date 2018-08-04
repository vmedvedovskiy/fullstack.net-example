using System;

namespace Fullstack.NET.Services.Orders
{
    public class OrderModel
    {
        public readonly Guid Id;
        public readonly DateTimeOffset CreatedDate;
        public readonly Guid UserId;

        public OrderModel(Guid id, DateTimeOffset createdDate, Guid userId)
        {
            this.Id = id;
            this.CreatedDate = createdDate;
            this.UserId = userId;
        }
    }
}