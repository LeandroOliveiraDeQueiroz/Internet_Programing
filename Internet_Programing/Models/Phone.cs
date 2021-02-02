using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public class Phone
    {
        //private const decimal V = 1.0m;

        public int Id { set; get; }
        [Required]
        [StringLength(256)]
        public string Name { set; get; }
        [Required]
        [Range(0.1, Double.MaxValue, ErrorMessage = "Price need to be positive number.")]
        public decimal Price { set; get; }
        public string Description { set; get; }
        public int OSId { set; get; }
        public OS OS { set; get; }
        [Range(1, int.MaxValue, ErrorMessage = "Battery Ampere need to be positive number.")]
        public int BatteryAmpere { set; get; }
        [Range(1, int.MaxValue, ErrorMessage = "RAM need to be positive number.")]
        public int RAM { set; get; }
        [Range(1, int.MaxValue, ErrorMessage = "Memory need to be positive number.")]
        public int Memory { set; get; }
        public string Processor { set; get; }

        public ICollection<CartPhone> Cart { get; set; }

        public byte[] Photo { set; get; }

        public Brand Brand { set; get; }
        public int BrandId { set; get; }
    }
}
