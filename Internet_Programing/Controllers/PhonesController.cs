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

namespace Internet_Programing.Controllers
{
    [Authorize(Roles = "admin")]
    public class PhonesController : Controller
    {
        private readonly ShoppingDbContext _context;

        public PhonesController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: Phones
        public async Task<IActionResult> Index(string message = "", string color = "")
        {
            ViewData["message"] = message;
            ViewData["color"] = color;
            var shoppingDbContext = _context.Phone.Include(p => p.OS);
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // GET: Phones/Create
        public IActionResult Create()
        {
            ViewData["OSId"] = new SelectList(_context.OS, "OSId", "Name");
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,OSId,BatteryAmpere,RAM,Memory,Processor")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { message = "Create Sucess", color = "green" });
            }
            ViewData["OSId"] = new SelectList(_context.OS, "OSId", "Name", phone.OSId);
            return View(phone);
        }

        // GET: Phones/Edit/5
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
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,OSId,BatteryAmpere,RAM,Memory,Processor")] Phone phone)
        {
            if (id != phone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phone);
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
            return View(phone);
        }

        // GET: Phones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phone
                .Include(p => p.OS)
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
