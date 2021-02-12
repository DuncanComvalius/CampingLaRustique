using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CampingLaRustique.Data;
using CampingLaRustique.Models;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace CampingLaRustique.Controllers
{
    public class ReserveringController : Controller
    {
        private readonly KlantenContext _context;

        public ReserveringController(KlantenContext context)
        {
            _context = context;
        }

        // GET: Reservering
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservering.ToListAsync());
        }


        // GET: Reservering/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservering = await _context.Reservering
                .FirstOrDefaultAsync(m => m.Reserveringsnummer == id);
            if (reservering == null)
            {
                return NotFound();
            }

            return View(reservering);
        }

        // GET: Reservering/Create
        public IActionResult Create()
        {


            return View();
        }

        // GET: Reservering/Create
        public async Task<IActionResult> Create2()
        {
            //var campings = from m in _context.Camping
            //                select m;

            DateTime dt;
            Reservering rsv = new Reservering();
            DateTime.TryParse(HttpContext.Session.GetString("Datum"), out dt);


            var ReserveringIDs = _context.Reservering.Where(y => y.Datum == dt).Select(x => x.PlekID).ToList();
            var PlekID = _context.Camping.Select(x => x.PlekID).ToList().Except(ReserveringIDs);
            var CampingsList = _context.Camping.Select(x => x).ToList();

            var result = new List<Camping>();
            foreach (var v in CampingsList)
            {
                if (PlekID.Contains(v.PlekID))
                    result.Add(v);
            }
            
            var ReserveringVM = new ReserveringViewModel
            {
                campings = result
            };

            return View(ReserveringVM);
        }

        // GET: Reservering/Create
        public async Task<IActionResult> Create3()
        {
            var klanten = from m in _context.Klant
                          select m;

            var ReserveringVM = new ReserveringViewModel
            {
                klanten = await klanten.ToListAsync()
            };

            return View(ReserveringVM);
        }


        // Get Create4
        public async Task<IActionResult> Create4(ReserveringViewModel model)
        {
            var campings = from m in _context.Camping
                           select m;
            var klanten = from m in _context.Klant
                          select m;

            var ReserveringVM = new ReserveringViewModel
            {
                klanten = await klanten.ToListAsync(),
                campings = await campings.ToListAsync(),
                //reserverings = await reservings.ToListAsync()
            };

            DateTime dt;
            DateTime.TryParse(HttpContext.Session.GetString("Datum"), out dt);
            Decimal pr;
            Decimal.TryParse(HttpContext.Session.GetString("Prijs"), out pr);

            List<Reservering> ReserveringList = new List<Reservering>();

            ReserveringList.Add(new Reservering()
            {
                Datum = dt,
                PlekID = (int)HttpContext.Session.GetInt32("PlekId"),
                KlantID = (int)HttpContext.Session.GetInt32("KlantId"),
                Prijs = pr
            });

            ReserveringVM.reserverings = ReserveringList;

            return View(ReserveringVM);

        }

        // POST: Reservering/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Reserveringsnummer,KlantID,PlekID,Datum,Prijs,Betaald")] Reservering model, string BtnVolgende)
        {
            if (BtnVolgende != null)
            {
                if (ModelState.IsValid)
                {
                    HttpContext.Session.SetString("Datum", model.Datum.ToString("dd-MM-yyyy"));
                    //var ReserveringVM = new ReserveringViewModel();
                    // ReserveringVM.reserverings[0].Datum = model.Datum;


                    return Redirect("Create2");
                }
                return View();
            }
            return View();
        }

        //Create2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2([Bind("Reserveringsnummer,KlantID,PlekID,Datum,Prijs,Betaald")] string BtnVorige, string BtnVolgende, ReserveringViewModel model)
        {
            DateTime dt;
            Reservering rsv = new Reservering();
            DateTime.TryParse(HttpContext.Session.GetString("Datum"), out dt);
            rsv.Datum = dt;

            var camping = await _context.Camping.FindAsync(model.IsSelected);

            if (BtnVorige != null)
            {
                return View("Create", rsv);
            }

            if (BtnVolgende != null)
            {
                if (ModelState.IsValid)
                {
                    HttpContext.Session.SetInt32("PlekId", model.IsSelected);
                    HttpContext.Session.SetString("Prijs", camping.Prijs.ToString());

                    return Redirect("Create3");
                }
                return View();
            }
            return Redirect("Create3");
        }

        //Create3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create3([Bind("Reserveringsnummer,KlantID,PlekID,Datum,Prijs,Betaald")] string BtnVorige, string BtnVolgende, ReserveringViewModel model)
        {
            if (BtnVorige != null)
            {
                return View("Create2");
            }

            if (BtnVolgende != null)
            {
                if (ModelState.IsValid)
                {
                    HttpContext.Session.SetInt32("KlantId", model.IsSelected);
                    return Redirect("Create4");
                }
                return View();
            }
            return Redirect("Create4");
        }


        //Create4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create4([Bind("Reserveringsnummer,KlantID,PlekID,Datum,Prijs,Betaald")] ReserveringViewModel model, string BtnVorige, string BtnOpslaan)
        {

            if (BtnOpslaan != null)
            {
                if (ModelState.IsValid)
                {
                    DateTime dt;
                    Reservering rsv = new Reservering();
                    DateTime.TryParse(HttpContext.Session.GetString("Datum"), out dt);
                    Decimal pr;
                    Decimal.TryParse(HttpContext.Session.GetString("Prijs"), out pr);

                    rsv.Datum = dt;
                    rsv.PlekID = (int)HttpContext.Session.GetInt32("PlekId");
                    rsv.KlantID = (int)HttpContext.Session.GetInt32("KlantId");
                    rsv.Prijs = pr;
                    _context.Add(rsv);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                    // return Redirect("Index");
                }
                return View("Index");
            }
            return View("Index");
        }




        // GET: Reservering/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservering = await _context.Reservering.FindAsync(id);
            if (reservering == null)
            {
                return NotFound();
            }
            return View(reservering);
        }

        // POST: Reservering/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Reserveringsnummer,KlantID,PlekID,Datum,Prijs,Betaald")] Reservering reservering)
        {
            if (id != reservering.Reserveringsnummer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservering);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveringExists(reservering.Reserveringsnummer))
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
            return View(reservering);
        }

        // GET: Reservering/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservering = await _context.Reservering
                .FirstOrDefaultAsync(m => m.Reserveringsnummer == id);
            if (reservering == null)
            {
                return NotFound();
            }

            return View(reservering);
        }

        // POST: Reservering/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservering = await _context.Reservering.FindAsync(id);
            _context.Reservering.Remove(reservering);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveringExists(int id)
        {
            return _context.Reservering.Any(e => e.Reserveringsnummer == id);
        }
    }
}
