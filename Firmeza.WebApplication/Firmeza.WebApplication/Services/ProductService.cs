using Firmeza.WebApplication.Interfaces;
using Firmeza.WebApplication.Models;
using Firmeza.WebApplication.Utils;

namespace Firmeza.WebApplication.Services;

public class ProductService
{
    private readonly IProductRepository _repo;
    private readonly IStringSanitizer _sanitizer;
    private readonly IDateTimeProvider _clock;

    public ProductService(IProductRepository repo, IStringSanitizer sanitizer, IDateTimeProvider clock)
        => (_repo, _sanitizer, _clock) = (repo, sanitizer, clock);

    public Task<List<Product>> SearchAsync(string? search, CancellationToken ct = default)
        => _repo.ListAsync(_sanitizer.NormalizeForSearch(search), ct);

    public async Task<ServiceResult<Guid>> CreateAsync(string? name, decimal price, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ServiceResult<Guid>.Fail(ErrorMessages.RequiredName);
        if (price <= 0)
            return ServiceResult<Guid>.Fail(ErrorMessages.PriceMustBePositive);

        try
        {
            var entity = new Product(name, price);
            await _repo.AddAsync(entity, ct);
            return ServiceResult<Guid>.Ok(entity.Id);
        }
        catch
        {
            return ServiceResult<Guid>.Fail(ErrorMessages.Unexpected);
        }
    }
}