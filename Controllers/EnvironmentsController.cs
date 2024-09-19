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
        public async Task<IActionResult> ExportToExcel()
        {
            var environments = await _context.Environments.ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Environments");

                // Add headers
                worksheet.Cells[1, 1].Value = "Environment Name";
                worksheet.Cells[1, 2].Value = "Environment Description";

                // Add data
                for (int i = 0; i < environments.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = environments[i].EnvironmentName;
                    worksheet.Cells[i + 2, 2].Value = environments[i].EnvironmentDescription;
                }

                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                // Set content type and file name
                var stream = new MemoryStream();
                package.SaveAs(stream);
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Environments.xlsx";

                return File(stream.ToArray(), contentType, fileName);
            }
        }

        private bool EnvironmentExists(int id)
        {
            return _context.Environments.Any(e => e.EnvironmentID == id);
        }
    }
}
