using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labo7_vol2.Data;
using Labo7_vol2.Models;
using Labo7_vol2.Code;

namespace Labo7_vol2.Controllers
{
    public class PrzepisyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrzepisyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Przepisy
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Produkty.Include(p => p.Kategoria);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Przepisy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepis = await _context.Produkty
                .Include(p => p.Kategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (przepis == null)
            {
                return NotFound();
            }

            return View(przepis);
        }

        // GET: Przepisy/Create
        public IActionResult Create()
        {
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa");
            return View();
        }

        // POST: Przepisy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Hasztag,Ocena,Tresc,KategoriaId, Cena")] Przepis przepis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(przepis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", przepis.KategoriaId);
            return View(przepis);
        }

        // GET: Przepisy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepis = await _context.Produkty.FindAsync(id);
            if (przepis == null)
            {
                return NotFound();
            }
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", przepis.KategoriaId);
            return View(przepis);
        }

        // POST: Przepisy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Hasztag,Ocena,Tresc,KategoriaId, Cena")] Przepis przepis)
        {
            if (id != przepis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(przepis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzepisExists(przepis.Id))
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
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", przepis.KategoriaId);
            return View(przepis);
        }

        // GET: Przepisy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepis = await _context.Produkty
                .Include(p => p.Kategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (przepis == null)
            {
                return NotFound();
            }

            return View(przepis);
        }

        // POST: Przepisy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var przepis = await _context.Produkty.FindAsync(id);
            _context.Produkty.Remove(przepis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzepisExists(int id)
        {
            return _context.Produkty.Any(e => e.Id == id);
        }
    }
}
