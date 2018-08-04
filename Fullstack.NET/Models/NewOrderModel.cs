using System;
using System.ComponentModel.DataAnnotations;

namespace Fullstack.NET.Models
{
    public class NewOrderModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        public Guid[] SelectedProductIds { get; set; }
    }
}
