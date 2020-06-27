using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labo7_vol2.Data;
using Labo7_vol2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Labo7_vol2.Controllers
{
    public class ProduktyWKoszykuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduktyWKoszykuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProduktyWKoszyku
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProduktWKoszyku.Include(p => p.Przepis);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProduktyWKoszyku/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktWKoszyku = await _context.ProduktWKoszyku
                .Include(p => p.Przepis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produktWKoszyku == null)
            {
                return NotFound();
            }

            return View(produktWKoszyku);
        }

        // GET: ProduktyWKoszyku/Create
        public IActionResult Create()
        {
            ViewData["PrzepisId"] = new SelectList(_context.Produkty, "Id", "Hasztag");
            return View();
        }

        // POST: ProduktyWKoszyku/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ilosc,PrzepisId")] ProduktWKoszyku produktWKoszyku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produktWKoszyku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrzepisId"] = new SelectList(_context.Produkty, "Id", "Hasztag", produktWKoszyku.PrzepisId);
            return View(produktWKoszyku);
        }

        // GET: ProduktyWKoszyku/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktWKoszyku = await _context.ProduktWKoszyku.FindAsync(id);
            if (produktWKoszyku == null)
            {
                return NotFound();
            }
            ViewData["PrzepisId"] = new SelectList(_context.Produkty, "Id", "Hasztag", produktWKoszyku.PrzepisId);
            return View(produktWKoszyku);
        }

        // POST: ProduktyWKoszyku/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ilosc,PrzepisId")] ProduktWKoszyku produktWKoszyku)
        {
            if (id != produktWKoszyku.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produktWKoszyku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktWKoszykuExists(produktWKoszyku.Id))
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
            ViewData["PrzepisId"] = new SelectList(_context.Produkty, "Id", "Hasztag", produktWKoszyku.PrzepisId);
            return View(produktWKoszyku);
        }

        // GET: ProduktyWKoszyku/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produktWKoszyku = await _context.ProduktWKoszyku
                .Include(p => p.Przepis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produktWKoszyku == null)
            {
                return NotFound();
            }

            return View(produktWKoszyku);
        }

        // POST: ProduktyWKoszyku/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produktWKoszyku = await _context.ProduktWKoszyku.FindAsync(id);
            _context.ProduktWKoszyku.Remove(produktWKoszyku);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktWKoszykuExists(int id)
        {
            return _context.ProduktWKoszyku.Any(e => e.Id == id);
        }

        [Authorize]
        public async Task<IActionResult> ZatwierdzenieZakupow()
        {
            return View();
        }
    }
}
