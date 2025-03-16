using Microsoft.AspNetCore.Mvc;
using FarSis.Data; // Use your DbContext namespace
using Microsoft.EntityFrameworkCore;

namespace FarSis.Components
{
    public class DepartmentsMenuViewComponent : ViewComponent
    {
        private readonly FarSisContext _context;

        public DepartmentsMenuViewComponent(FarSisContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var departments = await _context.Departments.ToListAsync();
            return View(departments);
        }
    }
}