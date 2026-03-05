using System.Text.Json.Serialization;

namespace TSDTechAssessment2026.API.Models;

public class ProductsResponse
{
    [JsonPropertyName("products")]
    public List<Product> Products { get; set; } = [];

    [JsonPropertyName("metadata")]
    public ProductMetadata Metadata { get; set; } = new();
}

public class ProductMetadata
{
    [JsonPropertyName("totalProducts")]
    public int TotalProducts { get; set; }

    [JsonPropertyName("generated")]
    public string Generated { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;
}

public class Product
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("stock")]
    public int? Stock { get; set; }

    [JsonPropertyName("images")]
    public List<string> Images { get; set; } = [];

    [JsonPropertyName("sku")]
    public string Sku { get; set; } = string.Empty;

    [JsonPropertyName("variants")]
    public List<Variant>? Variants { get; set; }

    [JsonPropertyName("brand")]
    public string Brand { get; set; } = string.Empty;

    [JsonPropertyName("rating")]
    public double Rating { get; set; }

    [JsonPropertyName("reviewCount")]
    public int ReviewCount { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }
}

public class Variant
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("sku")]
    public string Sku { get; set; } = string.Empty;

    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }
}
