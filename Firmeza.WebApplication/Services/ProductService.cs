using Firmeza.WebApplication.Data;
using Firmeza.WebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace Firmeza.WebApplication.Services;

/// <summary>
/// Business logic for Products (keeps controllers thin).
/// </summary>
public class ProductService
{
    private readonly AppDbContext _db;
    public ProductService(AppDbContext db) => _db = db;

    public Task<List<Product>> ListAsync(string? q = null, CancellationToken ct = default) =>
        _db.Products
           .Where(p => q == null || p.Name.Contains(q))
           .OrderBy(p => p.Name)
           .ToListAsync(ct);

    public async Task CreateAsync(string name, decimal unitPrice, CancellationToken ct = default)
    {
        var product = new Product(name, unitPrice);
        _db.Products.Add(product);
        await _db.SaveChangesAsync(ct);
    }
}
