using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public class Products
    {
        public int Id { set; get; }
        [Required]
        [StringLength(256)]
        public string Name { set; get; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Price need to be positive number.")]
        public float Price { set; get; }
        public string Description { set; get; }
        [Required]
        public string OS { set; get; }
        public int BatteryAmpere { set; get; }
        public int RAM { set; get; }
        public int Memory { set; get; } //GB
        public string Processor { set; get; }

        //public Brand Brand { set; get; }
        //public int BrandId { set; get; }
    }
}
