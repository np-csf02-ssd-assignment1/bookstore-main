using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebFrontend.Data;
using WebFrontend.Model;

namespace WebFrontend.Pages.Admin.Orders
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly WebFrontend.Data.WebFrontendAuditableContext _context;

        public CreateModel(WebFrontend.Data.WebFrontendAuditableContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PaymentID"] = new SelectList(_context.Set<PaymentType>(), "PaymentTypeID", "PaymentTypeID");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderList.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
