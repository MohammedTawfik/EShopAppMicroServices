using Catalog.API.CreateProduct;
using Catalog.API.GetProductById;
using Catalog.API.GetProducts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("CatalogConnection")!);
}).UseLightweightSessions();

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCreateProductEndpoint();
app.MapGetProductsEndpoint();
app.MapGetProductByIdEndpoint();

app.Run();
