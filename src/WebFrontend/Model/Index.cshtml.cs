using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebFrontend.Data;

namespace WebFrontend.Model
{
    public class IndexModel : PageModel
    {
        private readonly WebFrontend.Data.WebFrontendContext _context;

        public IndexModel(WebFrontend.Data.WebFrontendContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Product.ToListAsync();
        }
    }
}
