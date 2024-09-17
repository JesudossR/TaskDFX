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
            return View(await _context.ApplicationDetails.ToListAsync());
        }

        // GET: ApplicationDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationDetails = await _context.ApplicationDetails
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
            // Populate dropdowns
            ViewBag.ClientID = new SelectList(_context.Clients, "ClientID", "ClientID");
            ViewBag.ApplicationID = new SelectList(_context.Applications, "ApplicationID", "ApplicationID");
            ViewBag.EnvironmentID = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentID");

            return View();
        }

        // POST: ApplicationDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientID,ApplicationID,ClientName,EnvironmentID,Environment,ApplicationName,Link,Path")] ApplicationDetails applicationDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdowns in case of validation errors
            ViewBag.ClientID = new SelectList(_context.Clients, "ClientID", "ClientID", applicationDetails.ClientID);
            ViewBag.ApplicationID = new SelectList(_context.Applications, "ApplicationID", "ApplicationID", applicationDetails.ApplicationID);
            ViewBag.EnvironmentID = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentID", applicationDetails.EnvironmentID);
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

            // Populate dropdowns
            ViewBag.ClientID = new SelectList(_context.Clients, "ClientID", "ClientID", applicationDetails.ClientID);
            ViewBag.ApplicationID = new SelectList(_context.Applications, "ApplicationID", "ApplicationID", applicationDetails.ApplicationID);
            ViewBag.EnvironmentID = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentID", applicationDetails.EnvironmentID);

            return View(applicationDetails);
        }

        // POST: ApplicationDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientID,ApplicationID,ClientName,Environment,ApplicationName,Link,Path")] ApplicationDetails applicationDetails)
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

            // Repopulate dropdowns in case of validation errors
            ViewBag.ClientID = new SelectList(_context.Clients, "ClientID", "ClientName", applicationDetails.ClientID);
            ViewBag.ApplicationID = new SelectList(_context.Applications, "ApplicationID", "ApplicationName", applicationDetails.ApplicationID);
            ViewBag.EnvironmentID = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentID", applicationDetails.EnvironmentID);
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

        public JsonResult GetClientName(int clientId)
        {
            var client = _context.Clients.FirstOrDefault(c => c.ClientID == clientId);
            if (client != null)
            {
                return Json(client.ClientName); // Return the client name as JSON
            }
            return Json("");
        }

        public JsonResult GetApplicationName(int applicationId)
        {
            var application = _context.Applications.FirstOrDefault(a => a.ApplicationID == applicationId);
            if (application != null)
            {
                return Json(application.ApplicationName); // Return the application name as JSON
            }
            return Json("");
        }

        public JsonResult GetEnvironmentName(int environmentId)
        {
            var environment = _context.Environments.FirstOrDefault(e => e.EnvironmentID == environmentId);
            if (environment != null)
            {
                return Json(environment.EnvironmentName); // Adjust property name as necessary
            }
            return Json("");
        }
    }
}



//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using DolphinFx.Models;

//namespace DolphinFx.Controllers
//{
//    public class ApplicationDetailsController : Controller
//    {
//        private readonly AppDbContext _context;

//        public ApplicationDetailsController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: ApplicationDetails
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.ApplicationDetails.ToListAsync());
//        }

//        // GET: ApplicationDetails/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var applicationDetails = await _context.ApplicationDetails
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (applicationDetails == null)
//            {
//                return NotFound();
//            }

//            return View(applicationDetails);
//        }

//        // GET: ApplicationDetails/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: ApplicationDetails/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,ClientID,ApplicationID,ClientName,Environment,ApplicationName,Link,Path")] ApplicationDetails applicationDetails)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(applicationDetails);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(applicationDetails);
//        }

//        // GET: ApplicationDetails/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var applicationDetails = await _context.ApplicationDetails.FindAsync(id);
//            if (applicationDetails == null)
//            {
//                return NotFound();
//            }
//            return View(applicationDetails);
//        }

//        // POST: ApplicationDetails/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientID,ApplicationID,ClientName,Environment,ApplicationName,Link,Path")] ApplicationDetails applicationDetails)
//        {
//            if (id != applicationDetails.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(applicationDetails);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ApplicationDetailsExists(applicationDetails.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(applicationDetails);
//        }

//        // GET: ApplicationDetails/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var applicationDetails = await _context.ApplicationDetails
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (applicationDetails == null)
//            {
//                return NotFound();
//            }

//            return View(applicationDetails);
//        }

//        // POST: ApplicationDetails/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var applicationDetails = await _context.ApplicationDetails.FindAsync(id);
//            if (applicationDetails != null)
//            {
//                _context.ApplicationDetails.Remove(applicationDetails);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ApplicationDetailsExists(int id)
//        {
//            return _context.ApplicationDetails.Any(e => e.Id == id);
//        }
//    }
//}
