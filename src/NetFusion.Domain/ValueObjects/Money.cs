namespace NetFusion.Domain.ValueObjects
{
    public record Money(decimal Amount, string Currency)
    {
        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot add different currencies.");
            return new Money(a.Amount + b.Amount, a.Currency);
        }
    }
}
