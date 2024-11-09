using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }

    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}