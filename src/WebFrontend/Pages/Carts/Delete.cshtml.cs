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

namespace WebFrontend.Pages.Carts
{
    [Authorize(Roles = "Admin, User")]
    public class DeleteModel : PageModel
    {
        private readonly WebFrontend.Data.WebFrontendContext _context;

        public DeleteModel(WebFrontend.Data.WebFrontendContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cart Cart { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cart = await _context.ShoppingCartItems.FirstOrDefaultAsync(m => m.ItemId == id);

            if (Cart == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cart = await _context.ShoppingCartItems.FindAsync(id);

            if (Cart != null)
            {
                _context.ShoppingCartItems.Remove(Cart);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
