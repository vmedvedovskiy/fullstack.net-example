using System;

namespace Fullstack.NET.StoreIntegration.Contract
{
    public class User
    {
        public readonly Guid Id;
        public readonly string Username;
        public readonly string PhoneNumber;

        public UserModel(Guid id, string username, string phoneNumber)
        {
            this.Id = id;
            this.Username = username;
            this.PhoneNumber = phoneNumber;
        }
    }
}
