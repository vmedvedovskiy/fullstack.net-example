using System;

namespace Fullstack.NET.Models
{
    public class NewOrderModel
    {
        public string PhoneNumber { get; set; }

        public Guid[] SelectedProductIds { get; set; }
    }
}
