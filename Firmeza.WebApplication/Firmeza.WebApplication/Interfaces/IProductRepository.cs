using Firmeza.WebApplication.Models;


namespace Firmeza.WebApplication.Interfaces;

public interface IProductRepository
{
Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Product>> ListAsync(string? search, CancellationToken ct = default);
    Task AddAsync(Product entity, CancellationToken ct = default);
    Task UpdateAsync(Product entity, CancellationToken ct = default);
    Task DeleteAsync(Product entity, CancellationToken ct = default);
}