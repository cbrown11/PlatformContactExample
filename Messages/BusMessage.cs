
using DomainBase;

namespace Messages
{
    public class BusMessage : MessageBase.BusMessage
    {
        public AuditInfo AuditInfo { get; set; }

        public BusMessage(AuditInfo auditInfo) : base()
        {
            AuditInfo = auditInfo;
        }
    }
}