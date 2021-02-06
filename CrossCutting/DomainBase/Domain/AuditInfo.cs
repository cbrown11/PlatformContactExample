using System;

namespace DomainBase
{
    public class AuditInfo
    {
        public DateTime Created { get; set; }
        public string By { get; set; }
        public AuditInfo()
        {
            Created = DateTime.UtcNow;
        }
    }
}
