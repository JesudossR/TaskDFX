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
    public class ApplicationDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public ApplicationDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationDetails
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ApplicationDetails.Include(a => a.Applications).Include(a => a.Client).Include(a => a.Environments).Include(a => a.UserRole);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ApplicationDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationDetails = await _context.ApplicationDetails
                .Include(a => a.Applications)
                .Include(a => a.Client)
                .Include(a => a.Environments)
                .Include(a => a.UserRole)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationDetails == null)
            {
                return NotFound();
            }

            return View(applicationDetails);
        }

        // GET: ApplicationDetails/Create
        public IActionResult Create()
        {
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationName");
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientName");
            ViewData["EnvironmentID"] = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentName");
            ViewData["UserId"] = new SelectList(_context.UserRoles, "UserID", "Role");
            return View();
        }

        // POST: ApplicationDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientID,EnvironmentID,ApplicationID,Link,Path,User,UserId,Password")] ApplicationDetails applicationDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationName", applicationDetails.ApplicationID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientName", applicationDetails.ClientID);
            ViewData["EnvironmentID"] = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentName", applicationDetails.EnvironmentID);
            ViewData["UserId"] = new SelectList(_context.UserRoles, "UserID", "Role", applicationDetails.UserId);
            return View(applicationDetails);
        }

        // GET: ApplicationDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationDetails = await _context.ApplicationDetails.FindAsync(id);
            if (applicationDetails == null)
            {
                return NotFound();
            }
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationName", applicationDetails.ApplicationID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientName", applicationDetails.ClientID);
            ViewData["EnvironmentID"] = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentName", applicationDetails.EnvironmentID);
            ViewData["UserId"] = new SelectList(_context.UserRoles, "UserID", "Role", applicationDetails.UserId);
            return View(applicationDetails);
        }

        // POST: ApplicationDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientID,EnvironmentID,ApplicationID,Link,Path,User,UserId,Password")] ApplicationDetails applicationDetails)
        {
            if (id != applicationDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationDetailsExists(applicationDetails.Id))
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
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationName", applicationDetails.ApplicationID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientName", applicationDetails.ClientID);
            ViewData["EnvironmentID"] = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentName", applicationDetails.EnvironmentID);
            ViewData["UserId"] = new SelectList(_context.UserRoles, "UserID", "Role", applicationDetails.UserId);
            return View(applicationDetails);
        }

        // GET: ApplicationDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationDetails = await _context.ApplicationDetails
                .Include(a => a.Applications)
                .Include(a => a.Client)
                .Include(a => a.Environments)
                .Include(a => a.UserRole)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationDetails == null)
            {
                return NotFound();
            }

            return View(applicationDetails);
        }

        // POST: ApplicationDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationDetails = await _context.ApplicationDetails.FindAsync(id);
            if (applicationDetails != null)
            {
                _context.ApplicationDetails.Remove(applicationDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationDetailsExists(int id)
        {
            return _context.ApplicationDetails.Any(e => e.Id == id);
        }
    }
}
