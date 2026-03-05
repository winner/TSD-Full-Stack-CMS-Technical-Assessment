using System.Net.Http.Json;
using TSDTechAssessment2026.WWW.Models;

namespace TSDTechAssessment2026.WWW.Services;

public interface IProductApiClient
{
    Task<List<Product>> GetProductsAsync(string? category = null);
}

public class ProductApiClient : IProductApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductApiClient> _logger;

    public ProductApiClient(HttpClient httpClient, ILogger<ProductApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<Product>> GetProductsAsync(string? category = null)
    {
        try
        {
            var url = string.IsNullOrWhiteSpace(category)
                ? "/products"
                : $"/products?category={Uri.EscapeDataString(category)}";

            var products = await _httpClient.GetFromJsonAsync<List<Product>>(url);
            return products ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch products from API");
            return [];
        }
    }
}
