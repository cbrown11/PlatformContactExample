using System;
using Domain.Events;
using Domain.Exception;
using DomainBase;
using DomainBase.Interfaces;

namespace Domain.Aggregate
{
    public class CashCard: AggregateRoot, IAggregate
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public double Balance { get; set; }
        public DateTime Created { get; private set; }
        public override string AggregateId => Id;

        public CashCard()
        {
            RegisterTransition<CashCardCreated>(Apply);
            RegisterTransition<MoneyDeposited>(Apply);
            RegisterTransition<MoneyWithdrawn>(Apply);
        }

        private void Apply(CashCardCreated obj)
        {
            Id = obj.Id;
            ClientId = obj.ClientId;
            Balance = 0;
            Created = DateTime.UtcNow;
        }

        private void Apply(MoneyDeposited obj)
        {
            Balance += obj.Quantity;
        }

        private void Apply(MoneyWithdrawn obj)
        {
            Balance -= obj.Quantity;
        }

        public CashCard(AuditInfo auditInfo, string id, string clientId, DateTime timeStamp) : this()
        {
            RaiseEvent(new CashCardCreated(auditInfo, id, clientId, timeStamp));
        }

        public static CashCard RequestCashCard(AuditInfo auditInfo, string id, string clientId, DateTime timeStamp)
        {
            return new CashCard(auditInfo, id, clientId, timeStamp);
        }

        public void Deposit(AuditInfo auditInfo, double quantity , DateTime timeStamp) 
        {
            RaiseEvent(new MoneyDeposited(auditInfo, Id, quantity, timeStamp));
        }
        public void Withdraw(AuditInfo auditInfo, double quantity, DateTime timeStamp)
        {
            if (quantity > Balance)
                throw new WithDrawException($"Can not withdraw {quantity} as insufficent funds. Balance on the card is {Balance}.");
            RaiseEvent(new MoneyWithdrawn(auditInfo, Id, quantity, timeStamp));
        }
    }
}
