using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebFrontend.Data;
using WebFrontend.Model;

namespace WebFrontend.Pages.Admin.Books
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly WebFrontend.Data.WebFrontendContext _context;

        public DetailsModel(WebFrontend.Data.WebFrontendContext context)
        {
            _context = context;
        }

        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book.FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
