using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Fullstack.NET.Database.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.NET.Services.Authentication
{
    public class UsersQueryService : IUsersQueryService
    {
        private readonly AuthenticationDbContext ctx;
        private readonly SHA256 hasher = SHA256.Create();

        public UsersQueryService(AuthenticationDbContext ctx)
        {
            this.ctx = ctx;

            InitializeIfNeeded(ctx);
        }

        public async Task<UserModel> Find(string username, string password)
        {
            var hashedPassword = Encoding.UTF8.GetString(
                this.hasher.ComputeHash(
                    Encoding.UTF8.GetBytes(password)));

            var user = await this.ctx.Set<User>()
                .FirstOrDefaultAsync(_ =>
                _.Username == username
                && _.PasswordHash == hashedPassword);

            return user == null ? null : new UserModel(user.Username);
        }

        private static void InitializeIfNeeded(AuthenticationDbContext ctx)
        {
            if(!ctx.Users.Any(_ => _.Username == "test"))
            {
                AuthenticationDbContextInitializer.Seed(ctx);
            }
        }
    }
}
