using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebFrontend.Model;

namespace WebFrontend.Data
{
    public class WebFrontendAuditableContext : WebFrontendContext
    {
        public WebFrontendAuditableContext(DbContextOptions<WebFrontendContext> options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {
            ChangeTracker
                .Entries()
                .Where(p => p.State == EntityState.Modified).ToList().ForEach(entry =>
            {
                Audit(entry);
            });

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker
                .Entries()
                .Where(p => p.State == EntityState.Modified).ToList().ForEach(entry =>
            {
                Audit(entry);
            });

            return await base.SaveChangesAsync(cancellationToken);
        }

    private void Audit(EntityEntry entry)
        {
            Console.WriteLine("audit triggered");
            foreach (var property in entry.Properties)
            {
                Console.WriteLine("property triggered");
                var auditEntry = new AuditEntry
                {
                    Entity = entry.Entity.GetType().Name,
                    Property = property.Metadata.Name,
                    OldValue = property.OriginalValue.ToString(),
                    NewValue = property.OriginalValue.ToString(),
                    Date = DateTime.Now
                };

                this.AuditEntries.Add(auditEntry);
            }
        }
    }
}
