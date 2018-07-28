using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fullstack.NET.Database
{
    public class Order
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        [NotMapped]
        public ICollection<OrderItem> Items { get; set; }
    }
}
