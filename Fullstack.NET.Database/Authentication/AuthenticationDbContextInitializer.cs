using System;
using System.Security.Cryptography;
using System.Text;

namespace Fullstack.NET.Database.Authentication
{
    public class AuthenticationDbContextInitializer
    {
        public static void Seed(AuthenticationDbContext ctx)
        {
            ctx.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                PasswordHash = Encoding.UTF8.GetString(
                    SHA256.Create().ComputeHash(
                        Encoding.UTF8.GetBytes("test"))),
                Username = "test"
            });

            ctx.SaveChanges();
        }
    }
}
