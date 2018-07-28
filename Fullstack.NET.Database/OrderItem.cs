using System;

namespace Fullstack.NET.Database
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid OrderId { get; set; }
    }
}
