namespace Catalog.API.Products.CreateProduct
{
    public record CreateCommandRequest(string Name, List<string> Category, string Description, string ImagePath, decimal Price);
    public record CreateCommandResponse(Guid Id);
    public static class CreateProductEndpoint
    {
        public static void MapCreateProductEndpoint(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPost("/api/products", async (CreateCommandRequest request, ISender sender) =>
            {
                var product = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(product);
                var response = result.Adapt<CreateCommandResponse>();
                return TypedResults.Created($"/api/products/{response.Id}", response);
            })
                .WithName("CreateProduct")
                .Produces<CreateCommandResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Creates a new product")
                .WithDescription("Creates a new product in the catalog");
        }
    }
}
