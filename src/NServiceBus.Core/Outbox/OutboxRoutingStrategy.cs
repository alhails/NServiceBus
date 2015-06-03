namespace NServiceBus
{
    using System.Collections.Generic;
    using System.Linq;
    using NServiceBus.ConsistencyGuarantees;
    using NServiceBus.DeliveryConstraints;
    using NServiceBus.Outbox;
    using NServiceBus.Pipeline;
    using NServiceBus.Transports;

    class OutboxRoutingStrategy : DispatchStrategy
    {
        OutboxMessage currentOutboxMessage;
        Dictionary<string, string> options;

        public OutboxRoutingStrategy(OutboxMessage currentOutboxMessage, Dictionary<string, string> options)
        {
            this.currentOutboxMessage = currentOutboxMessage;
            this.options = options;
        }

        public override void Dispatch(OutgoingMessage message,
            RoutingStrategy routingStrategy,
            ConsistencyGuarantee minimumConsistencyGuarantee,
            IEnumerable<DeliveryConstraint> constraints,
            BehaviorContext currentContext)
        {
          
            //routingStrategy.Deserialize(options);


            constraints.ToList().ForEach(c => c.Serialize(options));
          
            currentOutboxMessage.TransportOperations.Add(new TransportOperation(message.MessageId, options, message.Body, message.Headers));                    
        }
    }
}