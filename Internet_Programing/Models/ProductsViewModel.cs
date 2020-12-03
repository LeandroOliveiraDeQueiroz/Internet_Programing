using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public class ProductsViewModel
    {
        public IEnumerable <Products> products { get; set; }
        public ProductPagination pagination { get; set; }
    }
}
