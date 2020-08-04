using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebFrontend.Data;
using WebFrontend.Model;

namespace WebFrontend.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly WebFrontend.Data.WebFrontendContext _context;

        public IndexModel(WebFrontend.Data.WebFrontendContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get; set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Order.ToListAsync();
        }
    }
}
