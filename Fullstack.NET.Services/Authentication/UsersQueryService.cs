using System;
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

            return MapUser(user);
        }

        public async Task<UserModel> Find(string phoneNumber)
        {
            var user = await this.ctx.Set<User>()
                .FirstOrDefaultAsync(_ =>
                    _.PhoneNumber == phoneNumber);

            return MapUser(user);
        }

        public async Task<UserModel> Find(Guid userId)
        {
            var user = await this.ctx.Set<User>()
                .FirstOrDefaultAsync(_ =>
                    _.Id == userId);

            return MapUser(user);
        }

        private static void InitializeIfNeeded(AuthenticationDbContext ctx)
            => AuthenticationDbContextInitializer.SeedIfNeeded(ctx);

        private static UserModel MapUser(User user) 
            => user == null ? 
            null 
            : new UserModel(user.Id, user.Username, user.PhoneNumber);
    }
}
