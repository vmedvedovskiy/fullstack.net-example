using System.ComponentModel.DataAnnotations;

namespace Fullstack.NET.Models
{
    public class NewAddressModel
    {
        [Required]
        public string DeptNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string StreetNumber { get; set; }
    }
}
