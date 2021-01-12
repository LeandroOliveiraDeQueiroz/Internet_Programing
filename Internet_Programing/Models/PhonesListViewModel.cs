using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public class PhonesListViewModel
    {
        public IEnumerable<Products> Products { get; set; }
        public PagingInfo Pagination { get; set; }

        public string SearchProduct { get; set; }
    }
}
