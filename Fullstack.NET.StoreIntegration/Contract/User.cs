using System;

namespace Fullstack.NET.StoreIntegration.Contract
{
    [Serializable]
    public class User
    {
        public readonly Guid Id;
        public readonly string Username;
        public readonly string PhoneNumber;

        public User(Guid id, string username, string phoneNumber)
        {
            this.Id = id;
            this.Username = username;
            this.PhoneNumber = phoneNumber;
        }
    }
}
