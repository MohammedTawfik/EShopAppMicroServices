using Catalog.API.CreateProduct;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCreateProductEndpoint();

app.Run();
