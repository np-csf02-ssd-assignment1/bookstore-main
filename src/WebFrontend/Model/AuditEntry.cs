using System;

namespace WebFrontend.Model
{
    public class AuditEntry
    {
        public int ID { get; set; }
        public string Entity { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Date { get; set; }
    }
}
