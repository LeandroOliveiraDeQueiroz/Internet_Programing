using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Internet_Programing.Data;
using Internet_Programing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Internet_Programing.Controllers
{
    
    public class PhonesController : Controller
    {
        private readonly ShoppingDbContext _context;

        public PhonesController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: Phones
        [Authorize(Roles = "admin, productManager")]
        public async Task<IActionResult> Index(string message = "", string color = "")
        {
            ViewData["message"] = message;
            ViewData["color"] = color;
            var shoppingDbContext = _context.Phone.Include(p => p.OS).Include(p => p.Brand);
            return View(await shoppingDbContext.ToListAsync());
        }

        // GET: Phones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phone
                .Include(p => p.OS)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        public async Task<IActionResult> AddInCart(int product)
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

            return RedirectToAction(nameof(Details), new { id = product }); ;
        }

        // GET: Phones/Create
        [Authorize(Roles = "admin, productManager")]
        public IActionResult Create()
        {
            ViewData["OSId"] = new SelectList(_context.OS, "OSId", "Name");
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "Name");
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,BrandId,OSId,BatteryAmpere,RAM,Memory,Processor")] Phone phone, IFormFile photoFile)
        {
            if (ModelState.IsValid)
            {
                if(photoFile != null && photoFile.Length > 0)
                {
                    using (var memFile = new MemoryStream())
                    {
                        photoFile.CopyTo(memFile);
                        phone.Photo = memFile.ToArray();
                    }
                }

                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { message = "Create Sucess", color = "green" });
            }
            ViewData["OSId"] = new SelectList(_context.OS, "OSId", "Name", phone.OSId);
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "Name", phone.BrandId);
            return View(phone);
        }

        // GET: Phones/Edit/5
        [Authorize(Roles = "admin, productManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phone.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            ViewData["OSId"] = new SelectList(_context.OS, "OSId", "Name", phone.OSId);
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "Name", phone.BrandId);
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, productManager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,BrandId,OSId,BatteryAmpere,RAM,Memory,Processor")] Phone phone, IFormFile photoFile)
        {
            if (id != phone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (photoFile != null && photoFile.Length > 0)
                    {
                        using (var memFile = new MemoryStream())
                        {
                            photoFile.CopyTo(memFile);
                            phone.Photo = memFile.ToArray();
                        }
                        _context.Update(phone);
                    } else
                    {
                        var oldPhone = await _context.Phone.FindAsync(id);

                        oldPhone.Name = phone.Name;
                        oldPhone.Price = phone.Price;
                        oldPhone.Description = phone.Description;
                        oldPhone.BrandId = phone.BrandId;
                        oldPhone.OSId = phone.OSId;
                        oldPhone.BatteryAmpere = phone.BatteryAmpere;
                        oldPhone.RAM = phone.RAM;
                        oldPhone.Memory = phone.Memory;
                        oldPhone.Processor = phone.Processor;
                        _context.Update(oldPhone);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneExists(phone.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OSId"] = new SelectList(_context.OS, "OSId", "Name", phone.OSId);
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "Name", phone.BrandId);
            return View(phone);
        }

        // GET: Phones/Delete/5
        [Authorize(Roles = "admin, productManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phone
                .Include(p => p.OS)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, productManager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phone = await _context.Phone.FindAsync(id);
            _context.Phone.Remove(phone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { message = "Delete Sucess", color = "red" });
        }

        private bool PhoneExists(int id)
        {
            return _context.Phone.Any(e => e.Id == id);
        }
    }
}
