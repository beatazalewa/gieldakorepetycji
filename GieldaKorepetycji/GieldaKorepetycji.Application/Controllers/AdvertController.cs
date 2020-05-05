using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GieldaKorepetycji.Infrastructure.Models;

namespace GieldaKorepetycji.Application.Controllers
{
    public class AdvertController : Controller
    {
        private readonly ETDatabaseContext _context;

        public AdvertController(ETDatabaseContext context)
        {
            _context = context;
        }

        // GET: Advert
        public async Task<IActionResult> Index()
        {
            var eTDatabaseContext = _context.Adverts.Include(a => a.Location).Include(a => a.User);
            return View(await eTDatabaseContext.ToListAsync());
        }

        // GET: Advert/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adverts = await _context.Adverts
                .Include(a => a.Location)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AdvertId == id);
            if (adverts == null)
            {
                return NotFound();
            }

            return View(adverts);
        }

        // GET: Advert/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Advert/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvertId,Title,Content,StartDate,DueDate,UserId,LocationId")] Adverts adverts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adverts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City", adverts.LocationId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", adverts.UserId);
            return View(adverts);
        }

        // GET: Advert/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adverts = await _context.Adverts.FindAsync(id);
            if (adverts == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City", adverts.LocationId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", adverts.UserId);
            return View(adverts);
        }

        // POST: Advert/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvertId,Title,Content,StartDate,DueDate,UserId,LocationId")] Adverts adverts)
        {
            if (id != adverts.AdvertId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adverts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertsExists(adverts.AdvertId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City", adverts.LocationId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", adverts.UserId);
            return View(adverts);
        }

        // GET: Advert/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adverts = await _context.Adverts
                .Include(a => a.Location)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AdvertId == id);
            if (adverts == null)
            {
                return NotFound();
            }

            return View(adverts);
        }

        // POST: Advert/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adverts = await _context.Adverts.FindAsync(id);
            _context.Adverts.Remove(adverts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertsExists(int id)
        {
            return _context.Adverts.Any(e => e.AdvertId == id);
        }
    }
}
