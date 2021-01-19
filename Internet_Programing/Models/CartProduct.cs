using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public class CartProduct
    {
        public int ProductsId { get; set; }
        public Products Products { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Price need to be positive number.")]
        public int Quantity { get; set; }
    }
}
