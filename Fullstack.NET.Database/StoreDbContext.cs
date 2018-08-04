using Microsoft.EntityFrameworkCore;

namespace Fullstack.NET.Database
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }
    }
}