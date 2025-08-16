using NetFusion.Domain.ValueObjects;

namespace NetFusion.Domain.Aggregates.OrderAggregate
{
    public class OrderItem
    {
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public Money Price { get; private set; }
        public Money Total => new Money(Price.Amount * Quantity, Price.Currency);

        private OrderItem() { }

        public OrderItem(string productName, int quantity, Money price)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive.");
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
    }
}
