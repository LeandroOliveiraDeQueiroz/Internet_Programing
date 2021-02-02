using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Internet_Programing.Models;
using Internet_Programing.Data;
using Microsoft.EntityFrameworkCore;

namespace Internet_Programing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShoppingDbContext _context;
        private ShoppingRepository repository;

        public HomeController(ILogger<HomeController> logger, 
            ShoppingDbContext context, 
            ShoppingRepository repository)
        {
            _logger = logger;
            _context = context;
            this.repository = repository;
        }

        //GET
        public IActionResult Index( string name = null, int brand = -1, int page = 1)
        {
            var pagination = new PagingInfo
            {
                CurrentPage = page,
                PageSize = PagingInfo.DEFAULT_PAGE_SIZE,
                TotalItems = _context.Phone.Include(p => p.OS).Include(p => p.Brand)
                    .Where(p => name == null && brand == -1
                        || brand == -1 && p.Name.Contains(name)
                        || name == null && p.Brand.BrandId == brand
                        || p.Name.Contains(name) && p.Brand.BrandId == brand).Count()
            };
            
            return View(
                new PhonesListViewModel
                {
                    Phones = _context.Phone.Include(p => p.OS).Include(p => p.Brand)
                        .Where(p => name == null && brand == -1
                            || brand == -1 && p.Name.Contains(name)
                            || name == null && p.Brand.BrandId == brand
                            || p.Name.Contains(name) && p.Brand.BrandId == brand)
                        .OrderBy(p => p.Price)
                        .Skip((page - 1) * pagination.PageSize)
                        .Take(pagination.PageSize),
                    Pagination = pagination,
                    SearchProduct = name,
                    Brands = _context.Brand.ToList(),
                    FilterBrand = brand
                }
            ); ;
        }

        public async Task<IActionResult> AddInCart(int product, string name = null, int brand = -1, int page = 1)
        {
            string username = User.Identity.Name;

            var customer = await _context.Customer.SingleOrDefaultAsync(c => c.Email == username);

            CartPhone cartProduct = new CartPhone { PhoneId = product, CustomerId = customer.CustomerId, Quantity = 1 };

            CartPhone databaseCartProduct = await _context.FindAsync<CartPhone>(cartProduct.PhoneId, cartProduct.CustomerId);

            if (databaseCartProduct != null)
            {
                databaseCartProduct.Quantity += 1;
                _context.Update(databaseCartProduct);
            }
            else
            {
                _context.Add(cartProduct);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { name, brand, page }); ;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
