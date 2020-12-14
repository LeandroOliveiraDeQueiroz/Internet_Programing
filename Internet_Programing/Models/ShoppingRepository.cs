using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public interface ShoppingRepository
    {
        public IEnumerable<Products> Products { get; }

        public IEnumerable<OS> OS { get; }
    }
}
