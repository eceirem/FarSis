using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FarSis.Data;
using FarSis.Models;

namespace FarSis.Controllers
{
    public class DocumentController : Controller
    {
        private readonly FarSisContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DocumentController(FarSisContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, DocumentText")] Document document)
        {
            if (ModelState.IsValid)
            {
                // Get the current logged-in user
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    // Set the document properties
                    document.Id = user.Id; // Assign logged-in user's ID to the document
                    document.DepartmentId = user.DepartmentId; // Assign department ID
                    document.Label = DateTime.Now;

                    _context.Add(document);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(document);
        }
    }
}
