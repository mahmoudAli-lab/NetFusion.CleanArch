using NetFusion.Domain.Aggregates.OrderAggregate;

namespace NetFusion.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(OrderId id);
        Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId);
    }
}
