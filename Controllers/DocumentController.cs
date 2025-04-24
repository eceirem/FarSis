using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FarSis.Data;
using FarSis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FarSis.Controllers
{
    
    public class DocumentController : Controller
    {
        private readonly FarSisContext _context;
        private readonly UserManager<User> _userManager;

        public DocumentController(FarSisContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Documents
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge(); // Kullanıcı oturum açmamışsa login sayfasına yönlendir
            }

            // Sadece giriş yapan kullanıcının oluşturduğu belgeleri getir
            var documents = await _context.Documents
                .Where(d => d.UserId == user.Id)
                .OrderByDescending(d => d.Label)
                .ToListAsync();

            return View(documents);

        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Document document)
        {
            if (ModelState.IsValid)
            {
                // Get the current logged-in user
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    // Set the document properties
                    document.UserId = user.Id; // Assign logged-in user's ID to the documents users id
                    document.DepartmentId = user.DepartmentId; // Assign department ID
                    document.Label = DateTime.Now;

                    _context.Add(document);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(document);
        }
        // GET: Document/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            // Fetch the list of departments from the database
            var departments = await _context.Departments.ToListAsync();

            return View(document);
        }

        //  POST: Document/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Document document)
        {
            if (ModelState.IsValid)
            {
                 // Update the document in the database
                _context.Documents.Update(document);
                await _context.SaveChangesAsync();

                // Redirect to the Index view after successful update
                return RedirectToAction(nameof(Index));
            }

            // Return to the Edit view with validation errors
            return View(document);
        }
    }
}
