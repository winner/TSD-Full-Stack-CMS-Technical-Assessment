using System.Text.Json;
using TSDTechAssessment2026.API.Models;

namespace TSDTechAssessment2026.API.Services;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<List<Product>> GetProductsByCategoryAsync(string category);
}

public class ProductService : IProductService
{
    private readonly string _dataFilePath;
    private List<Product>? _cachedProducts;
    private readonly SemaphoreSlim _lock = new(1, 1);

    public ProductService(IWebHostEnvironment environment)
    {
        _dataFilePath = Path.Combine(environment.ContentRootPath, "data", "products.json");
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await LoadProductsAsync();
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(string category)
    {
        var products = await LoadProductsAsync();
        return products
            .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    private async Task<List<Product>> LoadProductsAsync()
    {
        if (_cachedProducts is not null)
            return _cachedProducts;

        await _lock.WaitAsync();
        try
        {
            if (_cachedProducts is not null)
                return _cachedProducts;

            var json = await File.ReadAllTextAsync(_dataFilePath);
            var response = JsonSerializer.Deserialize<ProductsResponse>(json)
                ?? throw new InvalidOperationException("Failed to deserialize products.json");

            _cachedProducts = response.Products;
            return _cachedProducts;
        }
        finally
        {
            _lock.Release();
        }
    }
}
