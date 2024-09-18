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
    public class DatabaseDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public DatabaseDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DatabaseDetails
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DatabaseDetails.Include(d => d.Application).Include(d => d.Client).Include(d => d.Environments);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DatabaseDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var databaseDetail = await _context.DatabaseDetails
                .Include(d => d.Application)
                .Include(d => d.Client)
                .Include(d => d.Environments)
                .FirstOrDefaultAsync(m => m.DbId == id);
            if (databaseDetail == null)
            {
                return NotFound();
            }

            return View(databaseDetail);
        }

        // GET: DatabaseDetails/Create
        public IActionResult Create()
        {
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationName");
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientID", "ClientName");
            ViewData["EnvironmentID"] = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentName");
            return View();
        }

        // POST: DatabaseDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DbId,Datasource,Username,Password,ClientId,EnvironmentID,ApplicationID")] DatabaseDetail databaseDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(databaseDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationName", databaseDetail.ApplicationID);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientID", "ClientName", databaseDetail.ClientId);
            ViewData["EnvironmentID"] = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentName", databaseDetail.EnvironmentID);
            return View(databaseDetail);
        }

        // GET: DatabaseDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var databaseDetail = await _context.DatabaseDetails.FindAsync(id);
            if (databaseDetail == null)
            {
                return NotFound();
            }
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationName", databaseDetail.ApplicationID);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientID", "ClientName", databaseDetail.ClientId);
            ViewData["EnvironmentID"] = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentName", databaseDetail.EnvironmentID);
            return View(databaseDetail);
        }

        // POST: DatabaseDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DbId,Datasource,Username,Password,ClientId,EnvironmentID,ApplicationID")] DatabaseDetail databaseDetail)
        {
            if (id != databaseDetail.DbId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(databaseDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatabaseDetailExists(databaseDetail.DbId))
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
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationName", databaseDetail.ApplicationID);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientID", "ClientName", databaseDetail.ClientId);
            ViewData["EnvironmentID"] = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentName", databaseDetail.EnvironmentID);
            return View(databaseDetail);
        }

        // GET: DatabaseDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var databaseDetail = await _context.DatabaseDetails
                .Include(d => d.Application)
                .Include(d => d.Client)
                .Include(d => d.Environments)
                .FirstOrDefaultAsync(m => m.DbId == id);
            if (databaseDetail == null)
            {
                return NotFound();
            }

            return View(databaseDetail);
        }

        // POST: DatabaseDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var databaseDetail = await _context.DatabaseDetails.FindAsync(id);
            if (databaseDetail != null)
            {
                _context.DatabaseDetails.Remove(databaseDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatabaseDetailExists(int id)
        {
            return _context.DatabaseDetails.Any(e => e.DbId == id);
        }
    }
}
