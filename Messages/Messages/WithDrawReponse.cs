
using DomainBase;
using MessageBase;

namespace Messages.Messages
{
    public class WithDrawReponse : BusMessage, IMessage
    {
        public string CardNumber { get; set; }
        public bool WithDrawValid { get; set; }
        public double Quantity { get; set; }
        public string Message { get; set; }

        public WithDrawReponse(AuditInfo auditInfo, string cardNumber,double quantity, bool withDrawValid, string message = ""): base(auditInfo)
        {
            CardNumber = cardNumber;
            WithDrawValid = withDrawValid;
            Quantity = quantity;
            Message = message;
        }
    }
}

