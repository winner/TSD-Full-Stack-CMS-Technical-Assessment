using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;

namespace TSDTechAssessment2026.WWW.Composing;

public class ProductCatalogueComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddNotificationAsyncHandler<UmbracoApplicationStartedNotification, ProductCatalogueSeeder>();
    }
}

public class ProductCatalogueSeeder : INotificationAsyncHandler<UmbracoApplicationStartedNotification>
{
    private readonly IContentTypeService _contentTypeService;
    private readonly IContentService _contentService;
    private readonly IFileService _fileService;
    private readonly IShortStringHelper _shortStringHelper;
    private readonly ILogger<ProductCatalogueSeeder> _logger;

    public ProductCatalogueSeeder(
        IContentTypeService contentTypeService,
        IContentService contentService,
        IFileService fileService,
        IShortStringHelper shortStringHelper,
        ILogger<ProductCatalogueSeeder> logger)
    {
        _contentTypeService = contentTypeService;
        _contentService = contentService;
        _fileService = fileService;
        _shortStringHelper = shortStringHelper;
        _logger = logger;
    }

    public Task HandleAsync(UmbracoApplicationStartedNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            if (_contentTypeService.Get("home") != null)
            {
                _logger.LogInformation("CMS already seeded, skipping.");
                return Task.CompletedTask;
            }

            _logger.LogInformation("Seeding CMS content types and content...");

            SeedElementType();
            SeedHomeDocumentType();
            SeedHomeContent();

            _logger.LogInformation("CMS seeding complete.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to seed CMS content.");
        }

        return Task.CompletedTask;
    }

    private void SeedElementType()
    {
        var elementType = new ContentType(_shortStringHelper, -1)
        {
            Alias = "productCatalogue",
            Name = "Product Catalogue",
            Icon = "icon-store",
            Description = "Displays a product catalogue fetched from the API. Can be added to any Block List or Block Grid.",
            IsElement = true,
        };

        _contentTypeService.Save(elementType);
        _logger.LogInformation("Created Element Type: productCatalogue");
    }

    private void SeedHomeDocumentType()
    {
        var template = _fileService.GetTemplate("Home");
        if (template == null)
        {
            template = new Template(_shortStringHelper, "Home", "Home");
            _fileService.SaveTemplate(template);
            _logger.LogInformation("Created template: Home");
        }

        var homeDocType = new ContentType(_shortStringHelper, -1)
        {
            Alias = "home",
            Name = "Home",
            Icon = "icon-home",
            Description = "Homepage with product catalogue",
            AllowedAsRoot = true,
        };

        homeDocType.AllowedTemplates = new[] { template };
        homeDocType.SetDefaultTemplate(template);

        _contentTypeService.Save(homeDocType);
        _logger.LogInformation("Created Document Type: home");
    }

    private void SeedHomeContent()
    {
        var homeDocType = _contentTypeService.Get("home");
        if (homeDocType == null) return;

        var existing = _contentService.GetRootContent()?.FirstOrDefault(c => c.ContentType.Alias == "home");
        if (existing != null) return;

        var homePage = _contentService.Create("Product Catalogue", -1, "home");
        _contentService.Save(homePage);

        var publishResult = _contentService.Publish(homePage, ["*"]);
        if (publishResult.Success)
        {
            _logger.LogInformation("Created and published Home content.");
        }
        else
        {
            _logger.LogWarning("Home content created but publish returned: {Result}", publishResult.Result);
        }
    }
}
