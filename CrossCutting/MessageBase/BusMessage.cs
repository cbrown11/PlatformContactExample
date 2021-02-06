using System;

namespace MessageBase
{
    public class BusMessage
    {
        public Guid TransactionId { get; set; }
 
        public BusMessage()
        {
            TransactionId = Guid.NewGuid();
        }
    }
}
