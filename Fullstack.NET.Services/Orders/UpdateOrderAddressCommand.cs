using System;

namespace Fullstack.NET.Services.Orders
{
    public class UpdateOrderAddressCommand
    {
        public readonly Guid OrderId;
        public readonly string DeptNumber;
        public readonly string City;
        public readonly string Street;
        public readonly string StreetNumber;

        public UpdateOrderAddressCommand(
            Guid orderId,
            string deptNumber, 
            string city, 
            string street, 
            string streetNumber)
        {
            this.OrderId = orderId;
            this.DeptNumber = deptNumber;
            this.City = city;
            this.Street = street;
            this.StreetNumber = streetNumber;
        }
    }
}
