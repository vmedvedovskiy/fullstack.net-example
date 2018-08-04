using System;
using System.Threading.Tasks;
using Fullstack.NET.Database;

namespace Fullstack.NET.Services.Address
{
    using Address = Database.Address;

    public class AddressCommandService : IAddressCommandService
    {
        private readonly StoreDbContext ctx;

        public AddressCommandService(StoreDbContext ctx) 
            => this.ctx = ctx;

        public async Task CreateAddress(NewAddressCommand newAddress)
        {
            using (var tx = await ctx.Database.BeginTransactionAsync())
            {
                this.ctx.Add(new Address
                {
                    Id = Guid.NewGuid(),
                    City = newAddress.City,
                    DeptNumber = newAddress.DeptNumber,
                    Street = newAddress.Street,
                    StreetNumber = newAddress.StreetNumber,
                    UserId = newAddress.UserId
                });

                await this.ctx.SaveChangesAsync();

                tx.Commit();
            }
        }
    }
}
