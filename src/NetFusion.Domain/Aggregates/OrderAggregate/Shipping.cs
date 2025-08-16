using NetFusion.Domain.ValueObjects;

namespace NetFusion.Domain.Aggregates.OrderAggregate
{
    // Placeholder for shipping logic, e.g., tracking, carrier, etc.
    public class Shipping
    {
        public Address ShippingAddress { get; private set; }

        public Shipping(Address address)
        {
            ShippingAddress = address;
        }
    }
}
