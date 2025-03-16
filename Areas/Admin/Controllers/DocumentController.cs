using Microsoft.AspNetCore.Mvc;
using FarSis.Data; // Use your DbContext namespace
using FarSis.Models; // Use your model namespace
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarSis.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DocumentController : Controller
    {
        private readonly FarSisContext _context;

        public DocumentController(FarSisContext context)
        {
            _context = context;
        }

        // GET: Admin/Document
        public async Task<IActionResult> Index(int? departmentId)
        {
            // Fetch the list of departments for the filter dropdown
            var departments = await _context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            // Fetch documents based on the selected department
            var documentsQuery = _context.Documents
                .Include(d => d.Department) // Include Department data
                .AsQueryable();

            if (departmentId.HasValue)
            {
                // Filter documents by the selected department
                documentsQuery = documentsQuery.Where(d => d.DepartmentId == departmentId.Value);
            }

            var documents = await documentsQuery.ToListAsync();

            return View(documents);
        }

        // GET: Admin/Document/Create
        public async Task<IActionResult> Create()
        {
            // Fetch the list of departments from the database
            var departments = await _context.Departments.ToListAsync();

            // Pass the departments to the view using ViewBag or a ViewModel
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View();
        }

        // POST: Admin/Document/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Document document)
        {
            if (ModelState.IsValid)
            {
                // Attach the selected Department to the context
                var department = await _context.Departments.FindAsync(document.DepartmentId);
                if (department != null)
                {
                    document.Department = department; // Attach the existing department
                }

                // Add the document to the database
                _context.Documents.Add(document);
                await _context.SaveChangesAsync();

                // Redirect to the Index view after successful save
                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, repopulate the departments dropdown
            var departments = await _context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            // Return to the Create view with validation errors
            return View(document);
        }

        // GET: Admin/Document/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            // Fetch the list of departments from the database
            var departments = await _context.Departments.ToListAsync();

            // Pass the departments to the view using ViewBag
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View(document);
        }

        // POST: Admin/Document/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Document document)
        {
            if (ModelState.IsValid)
            {
                // Attach the selected Department to the context
                var department = await _context.Departments.FindAsync(document.DepartmentId);
                if (department != null)
                {
                    document.Department = department; // Attach the existing department
                }

                // Update the document in the database
                _context.Documents.Update(document);
                await _context.SaveChangesAsync();

                // Redirect to the Index view after successful update
                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, repopulate the departments dropdown
            var departments = await _context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            // Return to the Edit view with validation errors
            return View(document);
        }

        // GET: Admin/Document/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Document/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.User)       // Include related User data
                .Include(d => d.Department) // Include related Department data
                .FirstOrDefaultAsync(m => m.Id == id);

            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }
    }
}