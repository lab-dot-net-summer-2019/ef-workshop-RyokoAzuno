using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace WebApp.Controllers
{
    public class KatanaController : Controller
    {
        private readonly SamuraiContext _context;

        public KatanaController(SamuraiContext context)
        {
            _context = context;
        }

        // GET: Katanas
        public async Task<IActionResult> Index()
        {
            var katanas = await _context.Katanas.ToListAsync();

            var sumurais = katanas.Select(katana => katana.Samurai).ToList();

            return View(katanas);
        }

        // GET: Katanas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var katana = await _context.Katanas
                .Include(k => k.Samurai)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (katana == null)
            {
                return NotFound();
            }

            return View(katana);
        }

        // GET: Katanas/Create
        public IActionResult Create(int samuraiId)
        {
            ViewData["SamuraiList"] = new SelectList(_context.Samurais, "Id", "Name",samuraiId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SamuraiId")] Katana katana)
        {
            if (ModelState.IsValid)
            {
                _context.Add(katana);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Samurais", new { id = katana.SamuraiId });
            }
            //ViewData["SamuraiId"] = new SelectList(_context.Samurais, "Id", "Id", katana.SamuraiId);
            return RedirectToAction("Details", "Samurais", new { id = katana.SamuraiId});
        }

        // GET: Katanas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var katana = await _context.Katanas.SingleOrDefaultAsync(m => m.Id == id);
            if (katana == null)
            {
                return NotFound();
            }
            ViewData["SamuraiId"] = new SelectList(_context.Samurais, "Id", "Name", katana.SamuraiId);

            return View(katana); 

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SamuraiId")] Katana katana) {
            if (id != katana.Id) {
                return NotFound();
            }
            if (ModelState.IsValid) {
                try {
                    _context.Update(katana);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!KatanaExists(katana.Id)) {
                        return NotFound();
                    }
                    else { throw; }
                }

                return RedirectToAction("Details", "Samurais", new { id = katana.SamuraiId });
            }
            ViewData["SamuraiId"] = new SelectList(_context.Samurais, "Id", "Id", katana.SamuraiId);

            return RedirectToAction("Details", "Samurais", new { id = katana.SamuraiId });
        }

        // GET: Katanas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var katana = await _context.Katanas
                .Include(q => q.Samurai)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (katana == null)
            {
                return NotFound();
            }

            return View(katana);
        }

        // POST: Katanas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var katana = await _context.Katanas.SingleOrDefaultAsync(m => m.Id == id);
            _context.Katanas.Remove(katana);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool KatanaExists(int id)
        {
            return _context.Katanas.Any(e => e.Id == id);
        }
    }
}
