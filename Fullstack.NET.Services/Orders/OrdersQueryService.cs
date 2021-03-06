﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fullstack.NET.Database;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.NET.Services.Orders
{
    public class OrdersQueryService : IOrdersQueryService
    {
        private readonly StoreDbContext ctx;

        public OrdersQueryService(StoreDbContext ctx) => this.ctx = ctx;

        public async Task<OrderModel> GetOrder(Guid orderId)
        {
            var data = await this.ctx
                .Set<Order>()
                .Where(_ => _.Id == orderId)
                .SingleOrDefaultAsync();

            return new OrderModel(data.Id, data.CreatedDate, data.UserId);
        }

        public async Task<IReadOnlyList<OrderModel>> GetOrders(Guid userID)
        {
            var data = await this.ctx
                .Set<Order>()
                .Where(_ => _.UserId == userID)
                .OrderByDescending(_ => _.CreatedDate)
                .ToListAsync();

            return data
                .Select(_ => new OrderModel(_.Id, _.CreatedDate, _.UserId))
                .ToList();
        }
    }
}
