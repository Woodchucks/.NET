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
using Microsoft.AspNetCore.Authorization;

namespace Labo7_vol2.Controllers
{
    public class ProduktyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduktyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produkty
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produkt.ToListAsync());
        }

        // GET: Produkty/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // GET: Produkty/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produkty/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Cena")] Produkt produkt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produkt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }

        // GET: Produkty/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt.FindAsync(id);
            if (produkt == null)
            {
                return NotFound();
            }
            return View(produkt);
        }

        // POST: Produkty/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Cena")] Produkt produkt)
        {
            if (id != produkt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produkt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktExists(produkt.Id))
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
            return View(produkt);
        }

        // GET: Produkty/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // POST: Produkty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produkt = await _context.Produkt.FindAsync(id);
            _context.Produkt.Remove(produkt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktExists(int id)
        {
            return _context.Produkt.Any(e => e.Id == id);
        }
        public ProduktyIOperacje DajKoszyk()
        {
            if (HttpContext.Session.Get<ProduktyIOperacje>("produktyIOperacje") == null)
                HttpContext.Session.Set<ProduktyIOperacje>("produktyIOperacje", new ProduktyIOperacje());
            return HttpContext.Session.Get<ProduktyIOperacje>("produktyIOperacje");
        }
        public void UaktualnijKoszyk(ProduktyIOperacje koszyk)
        {
            HttpContext.Session.Set<ProduktyIOperacje>("produktyIOperacje", koszyk);
        }
        public ActionResult Dodaj(int id)
        {
            ProduktyIOperacje koszyk = DajKoszyk();
            bool ret = koszyk.Dodaj(_context.Produkt.Find(id));
            UaktualnijKoszyk(koszyk);
            return RedirectToAction("Index");
        }
    }
}
