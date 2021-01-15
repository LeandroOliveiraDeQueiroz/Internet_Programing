using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set;}
        [Required]
        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
