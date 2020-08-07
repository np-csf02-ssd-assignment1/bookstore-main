using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFrontend.Data;
using WebFrontend.Model;

namespace WebFrontend.Pages.Carts
{
    [Authorize(Roles = "Admin, User")]
    public class EditModel : PageModel
    {
        private readonly WebFrontend.Data.WebFrontendContext _context;

        public EditModel(WebFrontend.Data.WebFrontendContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(Cart.ItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CartExists(int id)
        {
            return _context.ShoppingCartItems.Any(e => e.ItemId == id);
        }
    }
}
