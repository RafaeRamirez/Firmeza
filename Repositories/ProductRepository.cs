using Firmeza.WebApplication.Data;
using Firmeza.WebApplication.Interfaces;
using Firmeza.WebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace Firmeza.WebApplication.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;
    public ProductRepository(AppDbContext db) => _db = db;

    public Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<List<Product>> ListAsync(string? search, CancellationToken ct = default)
    {
        var q = _db.Products.AsNoTracking().Where(p => p.IsActive);
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLowerInvariant();
            q = q.Where(p => p.Name.ToLower().Contains(term));
        }
        return await q.OrderBy(p => p.Name).ToListAsync(ct);
    }

    public async Task AddAsync(Product entity, CancellationToken ct = default)
    { await _db.Products.AddAsync(entity, ct); await _db.SaveChangesAsync(ct); }

    public async Task UpdateAsync(Product entity, CancellationToken ct = default)
    { _db.Products.Update(entity); await _db.SaveChangesAsync(ct); }

    public async Task DeleteAsync(Product entity, CancellationToken ct = default)
    { _db.Products.Remove(entity); await _db.SaveChangesAsync(ct); }
}
