using Microsoft.AspNetCore.Mvc;
using FarSis.Data; // Use your DbContext namespace
using Microsoft.EntityFrameworkCore;

namespace FarSis.Components
{
    public class DocumentSummaryViewComponent : ViewComponent
    {
        private readonly FarSisContext _context;

        public DocumentSummaryViewComponent(FarSisContext context)
        {
            _context = context;
        }

        public async Task<string> InvokeAsync()
        {
            var count = await _context.Documents.CountAsync();
            return count.ToString();
        }
    }
}