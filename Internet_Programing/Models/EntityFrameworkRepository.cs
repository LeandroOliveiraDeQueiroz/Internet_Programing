using Internet_Programing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public class EntityFrameworkRepository : ShoppingRepository
    {
        private ShoppingDbContext dbContext;

        public EntityFrameworkRepository(ShoppingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Products> Products => dbContext.Product;
    }
}
