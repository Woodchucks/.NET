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
    public class KategorieController : Controller
    {
        private readonly Labo6DbContext _context;

        public KategorieController(Labo6DbContext context)
        {
            _context = context;
        }

        // GET: Kategorie
        public async Task<IActionResult> Index()
        {
            var labo6DbContext = _context.Kategorie.Include(k => k.NadKategoria);
            return View(await labo6DbContext.ToListAsync());
        }

        // GET: Kategorie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategorie
                .Include(k => k.NadKategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategoria == null)
            {
                return NotFound();
            }

            return View(kategoria);
        }

        // GET: Kategorie/Create
        public IActionResult Create()
        {
            ViewData["NadKategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa");
            return View();
        }

        // POST: Kategorie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,NadKategoriaId")] Kategoria kategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NadKategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", kategoria.NadKategoriaId);
            return View(kategoria);
        }

        // GET: Kategorie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategorie.FindAsync(id);
            if (kategoria == null)
            {
                return NotFound();
            }
            ViewData["NadKategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", kategoria.NadKategoriaId);
            return View(kategoria);
        }

        // POST: Kategorie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,NadKategoriaId")] Kategoria kategoria)
        {
            if (id != kategoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriaExists(kategoria.Id))
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
            ViewData["NadKategoriaId"] = new SelectList(_context.Kategorie, "Id", "Nazwa", kategoria.NadKategoriaId);
            return View(kategoria);
        }

        // GET: Kategorie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategorie
                .Include(k => k.NadKategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategoria == null)
            {
                return NotFound();
            }

            return View(kategoria);
        }

        // POST: Kategorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategoria = await _context.Kategorie.FindAsync(id);
            _context.Kategorie.Remove(kategoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriaExists(int id)
        {
            return _context.Kategorie.Any(e => e.Id == id);
        }

        public IActionResult Menu(int? id)
        {
            if (id == null) { id = 1; }
            var kategoria = _context.Kategorie.Find(id);
            var przepis = _context.Przepisy.Find(id);
            if (kategoria == null) 
            { 
                return NotFound(); 
            }
            int x = (int)id;
            int y = (int)id;
            ViewBag.SubCategories = (from Kategoria in _context.Kategorie where Kategoria.NadKategoriaId == x select Kategoria).ToList();
            ViewBag.UpperCategory = _context.Kategorie.FirstOrDefault(x => (x.Id == kategoria.NadKategoriaId));
            ViewBag.SubCategoriesPrzepisy = (from Przepis in _context.Przepisy where Przepis.KategoriaId == x select Przepis).ToList();
            ViewBag.UpperCategoryPrzepisy = _context.Przepisy.FirstOrDefault(y => (y.KategoriaId == x));
            return View(kategoria);
        }

        public IActionResult Labo6Widok()
        {
            return View();
        }
    }
}
