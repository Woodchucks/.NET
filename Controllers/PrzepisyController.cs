using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labo6.Data;
using Labo6.Models;

namespace Labo6.Controllers
{
    public class PrzepisyController : Controller
    {
        private readonly Labo6DbContext _context;

        public PrzepisyController(Labo6DbContext context)
        {
            _context = context;
        }

        // GET: Przepis
        public async Task<IActionResult> Index()
        {
            var labo6DbContext = _context.Przepisy.Include(p => p.Kategoria);
            return View(await labo6DbContext.ToListAsync());
        }

        // GET: Przepis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepis = await _context.Przepisy
                .Include(p => p.Kategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (przepis == null)
            {
                return NotFound();
            }

            return View(przepis);
        }

        // GET: Przepis/Create
        public IActionResult Create()
        {
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa");
            return View();
        }

        // POST: Przepis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Hasztag,Ocena,Tresc,KategoriaId")] Przepis przepis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(przepis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", przepis.Nazwa); //zmienilam z przepis.KategoriaId na Nazwa
            return View(przepis);
        }

        // GET: Przepis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepis = await _context.Przepisy.FindAsync(id);
            if (przepis == null)
            {
                return NotFound();
            }
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", przepis.Nazwa); //równiez zmiana
            return View(przepis);
        }

        // POST: Przepis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Hasztag,Ocena,Tresc,KategoriaId")] Przepis przepis)
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
            ViewData["KategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", przepis.Nazwa); //zmiana
            return View(przepis);
        }

        // GET: Przepis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepis = await _context.Przepisy
                .Include(p => p.Kategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (przepis == null)
            {
                return NotFound();
            }

            return View(przepis);
        }

        // POST: Przepis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var przepis = await _context.Przepisy.FindAsync(id);
            _context.Przepisy.Remove(przepis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzepisExists(int id)
        {
            return _context.Przepisy.Any(e => e.Id == id);
        }
    }
}
