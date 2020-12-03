using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public class ProductPagination
    {
        public const int DEFAULT_PAGE_SIZE = 5;
        public int TotalProducts { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int) Math.Ceiling((double) TotalProducts / PageSize);
    }
}
