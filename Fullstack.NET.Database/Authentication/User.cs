using System;

namespace Fullstack.NET.Database.Authentication
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }
    }
}