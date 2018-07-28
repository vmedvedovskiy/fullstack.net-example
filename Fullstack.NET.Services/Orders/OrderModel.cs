using System;

namespace Fullstack.NET.Services.Orders
{
    public class OrderModel
    {
        public readonly Guid Id;
        public readonly DateTimeOffset CreatedDate;

        public OrderModel(Guid id, DateTimeOffset createdDate)
        {
            this.Id = id;
            this.CreatedDate = createdDate;
        }
    }
}