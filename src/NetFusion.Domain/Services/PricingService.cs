using NetFusion.Domain.ValueObjects;

namespace NetFusion.Domain.Services
{
    public class PricingService
    {
        public Money ApplyDiscount(Money total, decimal discountPercent)
        {
            if (discountPercent < 0 || discountPercent > 100)
                throw new ArgumentException("Discount must be between 0 and 100.");
            var discounted = total.Amount - (total.Amount * discountPercent / 100);
            return new Money(discounted, total.Currency);
        }
    }
}
