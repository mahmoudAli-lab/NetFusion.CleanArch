using NetFusion.Domain.ValueObjects;
using NetFusion.Domain.Aggregates.OrderAggregate.Events;

namespace NetFusion.Domain.Aggregates.OrderAggregate
{
    public class Order
    {
        private readonly List<OrderItem> _items = new();
        private readonly List<object> _events = new();

        public OrderId Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public Money Total => new Money(_items.Sum(i => i.Total.Amount), "USD");
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public IReadOnlyCollection<object> Events => _events.AsReadOnly();

        public bool IsPaid { get; private set; }
        public bool IsShipped { get; private set; }

        private Order() { }

        public Order(Guid customerId)
        {
            Id = new OrderId(Guid.NewGuid());
            CustomerId = customerId;
            _events.Add(new OrderCreatedEvent(Id.Value, customerId));
        }

        public void AddItem(string productName, int quantity, Money price)
        {
            if (IsPaid) throw new InvalidOperationException("Cannot add items after payment.");
            _items.Add(new OrderItem(productName, quantity, price));
        }

        public void MarkAsPaid()
        {
            if (IsPaid) throw new InvalidOperationException("Order already paid.");
            IsPaid = true;
            _events.Add(new OrderPaidEvent(Id.Value, Total.Amount));
        }

        public void MarkAsShipped(Address shippingAddress)
        {
            if (!IsPaid) throw new InvalidOperationException("Cannot ship unpaid order.");
            if (IsShipped) throw new InvalidOperationException("Order already shipped.");
            IsShipped = true;
            _events.Add(new OrderShippedEvent(Id.Value, shippingAddress));
        }
    }
}
