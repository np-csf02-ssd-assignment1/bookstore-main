using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebFrontend.Model;

namespace WebFrontend.Data
{
    public class WebFrontendContext : DbContext
    {
        public WebFrontendContext(DbContextOptions<WebFrontendContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Order> OrderList { get; set; }
        public DbSet<Cart> ShoppingCartItems { get; set; }
        public DbSet<WebFrontend.Model.AuditEntry> AuditEntries { get; set; }
    }
}
