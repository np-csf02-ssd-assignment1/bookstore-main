using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebFrontend.Data;
using WebFrontend.Model;

namespace WebFrontend.Pages.Orders
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
        public IList<Order> Order { get; set; }
        public string UserID { get; set; }

        public async Task OnGetAsync()
        {
            UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            SQLmessage = "Select * From OrderList where UserId ='" + UserID + "'";
            Order = await _context.OrderList.FromSqlRaw(SQLmessage).ToListAsync();
        }
    }
}
