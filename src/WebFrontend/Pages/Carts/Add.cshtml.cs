using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class CreateModel : PageModel
    {
        private readonly WebFrontend.Data.WebFrontendContext _context;

        public CreateModel(WebFrontend.Data.WebFrontendContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Cart Cart { get; set; }
        public string SQLmessage { get; set; }
        public IList<Cart> CurrentCart { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Cart.UserId = UserID;
            SQLmessage = "Select * From ShoppingCartItems Where UserId = '" + Cart.UserId + "'";
            CurrentCart = await _context.ShoppingCartItems.FromSqlRaw(SQLmessage).ToListAsync();
            if (CurrentCart.Count == 0)
            {
                SQLmessage = "Select * From ShoppingCartItems";
                IList<Cart> AllCart = await _context.ShoppingCartItems.FromSqlRaw(SQLmessage).ToListAsync();
                if (AllCart.Count == 0)
                {
                    Cart.CartId = 1;
                }
                else
                {
                    Cart.CartId = AllCart[AllCart.Count - 1].CartId + 1;
                }
            }
            else if (CurrentCart[CurrentCart.Count - 1].Paid)
            {
                SQLmessage = "Select * From ShoppingCartItems";
                IList<Cart> AllCart = await _context.ShoppingCartItems.FromSqlRaw(SQLmessage).ToListAsync();
                Cart.CartId = AllCart[AllCart.Count - 1].CartId + 1;
            }
            else
            {
                Cart.CartId = CurrentCart[CurrentCart.Count - 1].CartId;
            }
            _context.ShoppingCartItems.Add(Cart);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
