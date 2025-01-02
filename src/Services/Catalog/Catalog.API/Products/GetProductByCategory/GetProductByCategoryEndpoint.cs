namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductsByCategoryResponse(IEnumerable<Product> Products);
    public static class GetProductsByCategoryEndpoint
    {
        public static void MapGetProductsByCategoryEndpoint(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("api/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsByCategoryQuery(category));
                var response = result.Adapt<GetProductsByCategoryResult>();
                return TypedResults.Ok(response);
            })
                .WithName("GetProductByCategory")
                .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Gets products by category")
                .WithDescription("Gets all products in the catalog by category");
        }
    }
}
