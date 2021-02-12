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
    public class KlantenController : Controller
    {
        private readonly KlantenContext _context;

        public KlantenController(KlantenContext context)
        {
            _context = context;
        }

        // GET: Klantens
        public async Task<IActionResult> Index(string PostcodeNummer, string searchString)
        {

            var Klanten = from m in _context.Klant
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Klanten = Klanten.Where(s => s.Achternaam.Contains(searchString));
            }

            return View(await Klanten.ToListAsync());

        }

        // GET: Klantens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klanten = await _context.Klant
                .FirstOrDefaultAsync(m => m.KlantID == id);
            if (klanten == null)
            {
                return NotFound();
            }

            return View(klanten);
        }

        // GET: Klantens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klantens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KlantID,Voornaam,Achternaam,Woonplaats,Postcode,Telefoon")] Klanten klanten)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klanten);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klanten);
        }

        // GET: Klantens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klanten = await _context.Klant.FindAsync(id);
            if (klanten == null)
            {
                return NotFound();
            }
            return View(klanten);
        }

        // POST: Klantens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KlantID,Voornaam,Achternaam,Woonplaats,Postcode,Telefoon")] Klanten klanten)
        {
            if (id != klanten.KlantID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klanten);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlantenExists(klanten.KlantID))
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
            return View(klanten);
        }

        // GET: Klantens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klanten = await _context.Klant
                .FirstOrDefaultAsync(m => m.KlantID == id);
            if (klanten == null)
            {
                return NotFound();
            }

            return View(klanten);
        }

        // POST: Klantens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klanten = await _context.Klant.FindAsync(id);
            _context.Klant.Remove(klanten);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlantenExists(int id)
        {
            return _context.Klant.Any(e => e.KlantID == id);
        }
    }
}
