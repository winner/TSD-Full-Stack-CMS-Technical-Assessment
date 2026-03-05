using TSDTechAssessment2026.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IProductService, ProductService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCms", policy =>
    {
        policy.WithOrigins(
                  "https://localhost:44321",
                  "http://localhost:13894",
                  "http://localhost:5266",
                  "http://www:8080")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowCms");

app.MapGet("/products", async (IProductService productService, string? category) =>
{
    var products = string.IsNullOrWhiteSpace(category)
        ? await productService.GetAllProductsAsync()
        : await productService.GetProductsByCategoryAsync(category);

    return Results.Ok(products);
})
.WithName("products");

app.Run();
