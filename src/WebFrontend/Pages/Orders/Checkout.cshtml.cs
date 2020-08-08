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

namespace WebFrontend.Pages.Orders
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
        public Order Order { get; set; }
        IList<Cart> AllCart { get; set; }
        public string SQLmessage { get; private set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Order.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            SQLmessage = "Select * From ShoppingCartItems Where UserId = '" + Order.UserID + "' AND Paid = 'False'";
            AllCart = await _context.ShoppingCartItems.FromSqlRaw(SQLmessage).ToListAsync();
            Order.CartID = AllCart[AllCart.Count - 1].CartId;
            foreach (Cart item in AllCart)
            {
                Order.TotalPrice += (item.Cost * item.Quantity);
            }
            Order.CreatedTime = DateTime.Now;
            _context.OrderList.Add(Order);
            await _context.SaveChangesAsync();
            SQLmessage = "Update ShoppingCartItems SET Paid = 'True' Where UserId = '" + Order.UserID + "' AND Paid = 'False'";
            _ = _context.ShoppingCartItems.FromSqlRaw(SQLmessage).ToListAsync();
            return RedirectToPage("./Index");
        }
    }
}
