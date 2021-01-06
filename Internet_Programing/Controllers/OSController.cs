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
    public class OSController : Controller
    {
        private readonly ShoppingDbContext _context;

        public OSController(ShoppingDbContext context)
        {
            _context = context;
        }

        // GET: OS
        public async Task<IActionResult> Index(string message = "", string color = "")
        {
            ViewData["message"] = message;
            ViewData["color"] = color;
            return View(await _context.OS.ToListAsync());
        }

        // GET: OS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oS = await _context.OS
                .FirstOrDefaultAsync(m => m.OSId == id);
            if (oS == null)
            {
                return NotFound();
            }

            return View(oS);
        }

        // GET: OS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OSId,Name,Version")] OS oS)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { message = "Create Sucess", color = "green" });
            }
            return View(oS);
        }

        // GET: OS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oS = await _context.OS.FindAsync(id);
            if (oS == null)
            {
                return NotFound();
            }
            return View(oS);
        }

        // POST: OS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OSId,Name,Version")] OS oS)
        {
            if (id != oS.OSId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OSExists(oS.OSId))
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
            return View(oS);
        }

        // GET: OS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oS = await _context.OS
                .FirstOrDefaultAsync(m => m.OSId == id);
            if (oS == null)
            {
                return NotFound();
            }

            return View(oS);
        }

        // POST: OS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oS = await _context.OS.FindAsync(id);
            _context.OS.Remove(oS);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { message = "Delete Sucess", color = "red" });
        }

        private bool OSExists(int id)
        {
            return _context.OS.Any(e => e.OSId == id);
        }
    }
}
