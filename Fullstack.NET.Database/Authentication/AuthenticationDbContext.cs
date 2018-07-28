using Microsoft.EntityFrameworkCore;

namespace Fullstack.NET.Database.Authentication
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
