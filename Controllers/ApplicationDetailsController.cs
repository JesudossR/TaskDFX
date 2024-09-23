using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DolphinFx.Models;
using OfficeOpenXml;

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
            return PartialView();
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
                TempData["SuccessMessage"] = "Application detail added.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationName", applicationDetails.ApplicationID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientName", applicationDetails.ClientID);
            ViewData["EnvironmentID"] = new SelectList(_context.Environments, "EnvironmentID", "EnvironmentName", applicationDetails.EnvironmentID);
            ViewData["UserId"] = new SelectList(_context.UserRoles, "UserID", "Role", applicationDetails.UserId);
            return PartialView(applicationDetails);
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
            return PartialView(applicationDetails);
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
                    TempData["SuccessMessage"] = "Application detail updated.";
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

            var applicationDetails = await _context.ApplicationDetails //taking all details from ApplicationDetails and returning to view and showing that "Are you sure to delete ?"
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
            TempData["SuccessMessage"] = "Application detail deleted.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ExportToExcel()
        {
            var applicationDetails = await _context.ApplicationDetails
                .Include(a => a.Client)
                .Include(a => a.Environments)
                .Include(a => a.Applications)
                .Include(a => a.UserRole)
                .ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ApplicationDetails");

                // Add header
                worksheet.Cells[1, 1].Value = "Client Name";
                worksheet.Cells[1, 2].Value = "Environment Name";
                worksheet.Cells[1, 3].Value = "Application Name";
                worksheet.Cells[1, 4].Value = "Link";
                worksheet.Cells[1, 5].Value = "Path";
                worksheet.Cells[1, 6].Value = "User";
                worksheet.Cells[1, 7].Value = "User Role";
                worksheet.Cells[1, 8].Value = "Password";

                // Add data
                for (int i = 0; i < applicationDetails.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = applicationDetails[i].Client?.ClientName;
                    worksheet.Cells[i + 2, 2].Value = applicationDetails[i].Environments?.EnvironmentName;
                    worksheet.Cells[i + 2, 3].Value = applicationDetails[i].Applications?.ApplicationName;
                    worksheet.Cells[i + 2, 4].Value = applicationDetails[i].Link;
                    worksheet.Cells[i + 2, 5].Value = applicationDetails[i].Path;
                    worksheet.Cells[i + 2, 6].Value = applicationDetails[i].User;
                    worksheet.Cells[i + 2, 7].Value = applicationDetails[i].UserRole?.Role;
                    worksheet.Cells[i + 2, 8].Value = applicationDetails[i].Password;
                }

                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                // Set content type and file name
                var stream = new MemoryStream();
                package.SaveAs(stream);
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "ApplicationDetails.xlsx";

                return File(stream.ToArray(), contentType, fileName);
            }
        }

        private bool ApplicationDetailsExists(int id)
        {
            return _context.ApplicationDetails.Any(e => e.Id == id);
        }
    }
}
