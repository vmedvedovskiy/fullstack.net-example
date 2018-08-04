using System;
using System.Threading.Tasks;

namespace Fullstack.NET.Services.Authentication
{
    public interface IUsersQueryService
    {
        Task<UserModel> Find(string username, string password);

        Task<UserModel> Find(string phoneNumber);

        Task<UserModel> Find(Guid userId);
    }
}
