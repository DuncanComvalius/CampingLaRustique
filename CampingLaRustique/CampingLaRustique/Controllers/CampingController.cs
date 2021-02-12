using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CampingLaRustique.Data;
using CampingLaRustique.Models;

namespace CampingLaRustique.Controllers
{
    public class CampingController : Controller
    {
        private readonly KlantenContext _context;

        public CampingController(KlantenContext context)
        {
            _context = context;
        }

        // GET: Camping
        public async Task<IActionResult> Index(string Typess, string searchString)
        {
            IQueryable<string> campingQuery = from m in _context.Camping
                                            orderby m.Type
                                            select m.Type;

            var campings = from m in _context.Camping
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                campings = campings.Where(s => s.Ligging.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(Typess))
            {
                campings = campings.Where(x => x.Type == Typess);
            }

            var typeVM = new CampingViewModel
            {
                Types = new SelectList(await campingQuery.Distinct().ToListAsync()),
                campings = await campings.ToListAsync()
            };

            return View(typeVM);
        }

        // GET: Camping/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camping = await _context.Camping
                .FirstOrDefaultAsync(m => m.PlekID == id);
            if (camping == null)
            {
                return NotFound();
            }

            return View(camping);
        }

        // GET: Camping/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Camping/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlekID,Type,Oppervlakte,GratisDouche,Huisdieren,Elektriciteit,Ligging")] Camping camping)
        {
            if (ModelState.IsValid)

            {
                _context.Add(camping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(camping);
        }

        // GET: Camping/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camping = await _context.Camping.FindAsync(id);
            if (camping == null)
            {
                return NotFound();
            }
            return View(camping);
        }

        // POST: Camping/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlekID,Type,Oppervlakte,GratisDouche,Huisdieren,Elektriciteit,Ligging")] Camping camping)
        {
            if (id != camping.PlekID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(camping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampingExists(camping.PlekID))
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
            return View(camping);
        }

        // GET: Camping/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camping = await _context.Camping
                .FirstOrDefaultAsync(m => m.PlekID == id);
            if (camping == null)
            {
                return NotFound();
            }

            return View(camping);
        }

        // POST: Camping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camping = await _context.Camping.FindAsync(id);
            _context.Camping.Remove(camping);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampingExists(int id)
        {
            return _context.Camping.Any(e => e.PlekID == id);
        }
    }
}
