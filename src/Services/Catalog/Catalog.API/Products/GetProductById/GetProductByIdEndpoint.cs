namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdResponse(Product Product);
    public static class GetProductByIdEndpoint
    {
        public static void MapGetProductByIdEndpoint(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("api/products/{productId}", async (Guid productId, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(productId));
                var response = result.Adapt<GetProductByIdResponse>();
                return TypedResults.Ok(response);
            })
                .WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Gets a product by id")
                .WithDescription("Gets a product in the catalog by its id");
        }
    }
}
