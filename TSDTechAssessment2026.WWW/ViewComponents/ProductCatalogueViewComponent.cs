using Microsoft.AspNetCore.Mvc;
using TSDTechAssessment2026.WWW.Services;

namespace TSDTechAssessment2026.WWW.ViewComponents;

public class ProductCatalogueViewComponent : ViewComponent
{
    private readonly IProductApiClient _apiClient;

    public ProductCatalogueViewComponent(IProductApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IViewComponentResult> InvokeAsync(string? categoryFilter = null)
    {
        var products = await _apiClient.GetProductsAsync(categoryFilter);
        return View(products);
    }
}
