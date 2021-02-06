using DomainBase;
using MessageBase;

namespace Messages.Commands
{
    public class RequestCashCard: BusMessage, ICommand
    {

        public string CardNumber { get; set; }
        public string ClientId { get; set; }

        public RequestCashCard(AuditInfo auditInfo, string cardNumber, string clientId) : base(auditInfo)
        {
            CardNumber = cardNumber;
            ClientId = clientId;
        }
    }
}
