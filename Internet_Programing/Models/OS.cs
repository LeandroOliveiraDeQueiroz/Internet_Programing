using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public class OS
    {
        public int OSId { set; get; }
        [Required]
        public string Name { set; get; }
        public int Version { set; get; }
    }
}
