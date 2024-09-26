using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DolphinFx.Models;
using OfficeOpenXml;
using X.PagedList;

namespace DolphinFx.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly AppDbContext _context;

        public UserRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserRoles
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 5; // Number of records per page
            int pageNumber = page ?? 1; // Default to page 1 if no page is specified
            var result = await _context.UserRoles.ToListAsync();
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        // GET: UserRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userRole == null)
            {
                return NotFound();
            }

            // Return the partial view for the modal
            return PartialView("Details", userRole);
        }

        // GET: UserRoles/Create
        public IActionResult Create()
        {
            ViewData["Roles"] = GetRoles(); // Populate the dropdown
            return View();
        }

        // POST: UserRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Role,RoleDescription")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRole);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User Role created.";
                return RedirectToAction(nameof(Index));
            }

            // Re-populate the dropdown if there's an error
            ViewData["Roles"] = GetRoles();
            return View(userRole);
        }

        // GET: UserRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }

            // Populate the dropdown with roles
            ViewData["Roles"] = GetRoles();
            return View(userRole);
        }

        // POST: UserRoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,Role,RoleDescription")] UserRole userRole)
        {
            if (id != userRole.UserID)
            {
                return NotFound();
            }
            var existingUserRole = await _context.UserRoles.AsNoTracking()
                              .FirstOrDefaultAsync(ur => ur.UserID == id);

            if (existingUserRole == null)
            {
                return NotFound();
            }

            // Check if there are any changes between the existing and new object
            if (existingUserRole.Role == userRole.Role &&
                existingUserRole.RoleDescription == userRole.RoleDescription)
            {
                // No changes detected
                TempData["InfoMessage"] = "No changes were made.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRole);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "User Role updated.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRoleExists(userRole.UserID))
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

            // Re-populate the dropdown if there's an error
            ViewData["Roles"] = GetRoles();
            return View(userRole);
        }

        // GET: UserRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (userRole == null)
            {
                return NotFound();
            }

            return View(userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "User Role deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        private bool UserRoleExists(int id)
        {
            return _context.UserRoles.Any(e => e.UserID == id);
        }

        // Helper method to get roles for dropdown
        private IEnumerable<SelectListItem> GetRoles()
        {
            // Example roles; replace with actual role retrieval logic
            var roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Maker", Text = "Maker" },
                new SelectListItem { Value = "Checker", Text = "Checker" },
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "View", Text = "View" },
                new SelectListItem { Value = "Both", Text = "Both" }
            };
            return roles;
        }
        public async Task<IActionResult> ExportToExcel()
        {
            var userRoles = await _context.UserRoles.ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("UserRoles");

                // Add headers
                worksheet.Cells[1, 1].Value = "Role";
                worksheet.Cells[1, 2].Value = "Role Description";

                // Add data
                for (int i = 0; i < userRoles.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = userRoles[i].Role;
                    worksheet.Cells[i + 2, 2].Value = userRoles[i].RoleDescription;
                }

                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                // Set content type and file name
                var stream = new MemoryStream();
                package.SaveAs(stream);
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "UserRoles.xlsx";

                return File(stream.ToArray(), contentType, fileName);
            }
        }

    }
}
