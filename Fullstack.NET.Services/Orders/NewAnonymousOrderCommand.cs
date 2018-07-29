using System;
using System.Collections.Generic;

namespace Fullstack.NET.Services.Orders
{
    public class NewAnonymousOrderCommand
    {
        public readonly string PhoneNumber;
        public readonly IReadOnlyList<Guid> SelectedProductIds;

        public NewAnonymousOrderCommand(
            string phoneNumber, 
            IReadOnlyList<Guid> selectedProductIds)
        {
            this.PhoneNumber = phoneNumber;
            this.SelectedProductIds = selectedProductIds;
        }
    }
}
