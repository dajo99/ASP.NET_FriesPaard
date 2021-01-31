using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BSFP.Data;
using BSFP.Models;

namespace BSFP.Controllers
{
    public class NieuwsController : Controller
    {
        private readonly BSFPContext _context;

        public NieuwsController(BSFPContext context)
        {
            _context = context;
        }

        // GET: Nieuws
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nieuws.ToListAsync());
        }

        // GET: Nieuws/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuws = await _context.Nieuws
                .FirstOrDefaultAsync(m => m.NieuwsID == id);
            if (nieuws == null)
            {
                return NotFound();
            }

            return View(nieuws);
        }

        // GET: Nieuws/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nieuws/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NieuwsID,Titel,Omschrijving,Datum")] Nieuws nieuws)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nieuws);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nieuws);
        }

        // GET: Nieuws/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuws = await _context.Nieuws.FindAsync(id);
            if (nieuws == null)
            {
                return NotFound();
            }
            return View(nieuws);
        }

        // POST: Nieuws/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NieuwsID,Titel,Omschrijving,Datum")] Nieuws nieuws)
        {
            if (id != nieuws.NieuwsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nieuws);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NieuwsExists(nieuws.NieuwsID))
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
            return View(nieuws);
        }

        // GET: Nieuws/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuws = await _context.Nieuws
                .FirstOrDefaultAsync(m => m.NieuwsID == id);
            if (nieuws == null)
            {
                return NotFound();
            }

            return View(nieuws);
        }

        // POST: Nieuws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nieuws = await _context.Nieuws.FindAsync(id);
            _context.Nieuws.Remove(nieuws);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NieuwsExists(int id)
        {
            return _context.Nieuws.Any(e => e.NieuwsID == id);
        }
    }
}
