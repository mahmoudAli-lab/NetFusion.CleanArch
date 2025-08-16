using NetFusion.Domain.Entities;

namespace NetFusion.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task<Customer?> GetByIdAsync(Guid id);
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}
