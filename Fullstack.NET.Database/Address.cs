using System;

namespace Fullstack.NET.Database
{
    public class Address
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string DeptNumber { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }
    }
}
