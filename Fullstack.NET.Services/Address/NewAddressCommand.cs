using System;

namespace Fullstack.NET.Services.Address
{
    public class NewAddressCommand
    {
        public readonly Guid UserId;
        public readonly string DeptNumber;
        public readonly string City;
        public readonly string Street;
        public readonly string StreetNumber;

        public NewAddressCommand(
            Guid userId, 
            string deptNumber, 
            string city, 
            string street, 
            string streetNumber)
        {
            this.UserId = userId;
            this.DeptNumber = deptNumber;
            this.City = city;
            this.Street = street;
            this.StreetNumber = streetNumber;
        }
    }
}
