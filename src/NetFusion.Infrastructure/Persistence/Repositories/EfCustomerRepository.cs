using Microsoft.EntityFrameworkCore;
using NetFusion.Domain.Entities;
using NetFusion.Domain.Interfaces;

namespace NetFusion.Infrastructure.Persistence.Repositories;

public class EfCustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _db;

    public EfCustomerRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _db.Customers.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Customer customer)
    {
        await _db.Customers.AddAsync(customer);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _db.Customers.Update(customer);
        await _db.SaveChangesAsync();
    }
}
