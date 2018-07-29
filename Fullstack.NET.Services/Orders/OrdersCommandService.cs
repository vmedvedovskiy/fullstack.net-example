using System;
using System.Linq;
using System.Threading.Tasks;
using Fullstack.NET.Database;

namespace Fullstack.NET.Services.Orders
{
    public class OrdersCommandService : IOrdersCommandService
    {
        private readonly StoreDbContext ctx;

        public OrdersCommandService(StoreDbContext ctx) => this.ctx = ctx;

        public async Task CreateOrder(NewOrderCommand createCommand)
        {
            var orderId = Guid.NewGuid();

            using (var tx = await ctx.Database.BeginTransactionAsync())
            {
                this.ctx.Add(new Order
                {
                    CreatedDate = DateTimeOffset.UtcNow,
                    Id = orderId,
                    UserId = createCommand.UserId
                });

                createCommand.ProductIds
                    .Select(productId => this.ctx.Add(new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        ProductId = productId
                    }));

                await this.ctx.SaveChangesAsync();

                tx.Commit();
            }
        }
    }
}
