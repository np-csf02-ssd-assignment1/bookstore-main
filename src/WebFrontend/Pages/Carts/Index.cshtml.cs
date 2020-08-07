using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class IndexModel : PageModel
    {
        private readonly WebFrontend.Data.WebFrontendContext _context;

        public IndexModel(WebFrontend.Data.WebFrontendContext context)
        {
            _context = context;
        }

        public string SQLmessage { get; private set; }
        public IList<Cart> Cart { get; set; }

        public async Task OnGetAsync()
        {
            string UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            SQLmessage = "Select * From ShoppingCartItems where UserId ='" + UserID + "' AND Paid = 'False'";
            Cart = await _context.ShoppingCartItems.FromSqlRaw(SQLmessage).ToListAsync();
        }
    }
}
