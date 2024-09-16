using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DolphinFx.Models;

namespace DolphinFx.Controllers
{
    public class EnvironmentsController : Controller
    {
        private readonly AppDbContext _context;

        public EnvironmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Environments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Environments.ToListAsync());
        }

        // GET: Environments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environment = await _context.Environments
                .FirstOrDefaultAsync(m => m.EnvironmentID == id);
            if (environment == null)
            {
                return NotFound();
            }

            return View(environment);
        }

        // GET: Environments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Environments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnvironmentID,EnvironmentName,EnvironmentDescription")] Models.Environment environment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(environment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(environment);
        }

        // GET: Environments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environment = await _context.Environments.FindAsync(id);
            if (environment == null)
            {
                return NotFound();
            }
            return View(environment);
        }

        // POST: Environments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnvironmentID,EnvironmentName,EnvironmentDescription")] Models.Environment environment)
        {
            if (id != environment.EnvironmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(environment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvironmentExists(environment.EnvironmentID))
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
            return View(environment);
        }

        // GET: Environments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environment = await _context.Environments
                .FirstOrDefaultAsync(m => m.EnvironmentID == id);
            if (environment == null)
            {
                return NotFound();
            }

            return View(environment);
        }

        // POST: Environments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var environment = await _context.Environments.FindAsync(id);
            if (environment != null)
            {
                _context.Environments.Remove(environment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnvironmentExists(int id)
        {
            return _context.Environments.Any(e => e.EnvironmentID == id);
        }
    }
}
