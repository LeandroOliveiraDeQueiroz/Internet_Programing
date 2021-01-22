using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public class CartPhone
    {
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity need to be positive number.")]
        public int Quantity { get; set; }
    }
}
