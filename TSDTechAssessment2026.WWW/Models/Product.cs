using System.Text.Json.Serialization;

namespace TSDTechAssessment2026.WWW.Models;

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

    public decimal DisplayPrice =>
        Price ?? Variants?.FirstOrDefault(v => v.Active)?.Price
              ?? Variants?.FirstOrDefault()?.Price
              ?? 0;

    public bool IsInvalid => !Active;

    public bool IsOutOfStock
    {
        get
        {
            if (Variants is { Count: > 0 })
                return Variants.All(v => v.Stock <= 0);
            return Stock is not null && Stock <= 0;
        }
    }
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
