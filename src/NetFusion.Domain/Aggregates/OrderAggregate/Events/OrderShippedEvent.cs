using NetFusion.Domain.ValueObjects;

namespace NetFusion.Domain.Aggregates.OrderAggregate.Events;

public record OrderShippedEvent(Guid OrderId, Address ShippingAddress);
