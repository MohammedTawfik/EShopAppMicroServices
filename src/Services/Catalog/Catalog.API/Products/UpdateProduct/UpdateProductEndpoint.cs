namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductrequest(Guid Id, string Name, List<string> Category, string Description, string ImagePath, decimal Price);
    public record UpdateProductResponse(bool IsUpdated);
    public static class UpdateProductEndpoint
    {
        public static void MapUpdateProductEndpoint(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPut("api/products", async (UpdateProductrequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();
                return TypedResults.Ok(response);
            })
                .WithName("UpdateProduct")
                .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Product")
                .WithDescription("Update Product");
        }
    }
}
