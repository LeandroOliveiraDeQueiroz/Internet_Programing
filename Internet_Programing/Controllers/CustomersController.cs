using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Internet_Programing.Data;
using Internet_Programing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Internet_Programing.Views
{
    public class CustomersController : Controller
    {
        private readonly ShoppingDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomersController(ShoppingDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Customers
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.ToListAsync());
        }

        // GET: Customers/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterCustomerViewModel registerCustomer)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(registerCustomer.Email);

                if (user != null)
                {
                    ModelState.AddModelError("", "Already exists a user with this email");
                    return View(registerCustomer);
                }

                user = new IdentityUser { UserName = registerCustomer.Email, Email = registerCustomer.Email };
                IdentityResult result = await _userManager.CreateAsync(user,  registerCustomer.Password);
                await _userManager.AddToRoleAsync(user, "customer");

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Error");
                    return View(registerCustomer);
                }

                Customer costumer = new Customer
                {
                    Name = registerCustomer.Name,
                    Email = registerCustomer.Email
                };
                _context.Add(costumer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(registerCustomer);
        }

        // GET: Customers/Edit/
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> Edit(string message)
        {
            ViewData["message"] = message;
            string username = User.Identity.Name;

            var customer = await _context.Customer.SingleOrDefaultAsync(c => c.Email == username);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SuperEdit(int? id, string message)
        {
            ViewData["superEdit"] = "true";
            ViewData["message"] = message;
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,Name, Email")] Customer customer)
        {
            string user = _userManager.GetUserId(User);

            if (id != customer.CustomerId && customer.Email != User.Identity.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit), new { message = "Edit Sucess!"});
            }
            return View(customer);
        }

        [Authorize(Roles = "customer")]
        public async Task<IActionResult> Cart()
        {
            string username = User.Identity.Name;

            var customer = await _context.Customer.SingleOrDefaultAsync(c => c.Email == username);
            if (customer == null)
            {
                return NotFound();
            }
            //customer.Cart

            var cart = _context.CartProduct.Where(cp => cp.CustomerId == customer.CustomerId);

            if (cart == null)
            {
                return NotFound();
            }

            //customer.Cart = new ICollection<Products>();

            foreach(CartProduct cp in cart)
            {
                cp.Products = await _context.Product.FindAsync(cp.ProductsId);
                customer.Cart.Add(cp);
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
