using DomainBase;
using MessageBase;

namespace Messages.Commands
{
    public class WithdrawMoney : BusMessage, ICommand
    {
        public string CardNumber { get; set; }
        public double Quantity { get; set; }

        public WithdrawMoney(AuditInfo auditInfo, string cardNumber, double quantity) : base(auditInfo)
        {
            CardNumber = cardNumber;
            Quantity = quantity;
        }
    }
}
