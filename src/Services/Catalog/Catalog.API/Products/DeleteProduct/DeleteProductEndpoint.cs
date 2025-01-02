namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductResponse(bool IsDeleted);
    public static class DeleteProductEndpoint
    {
        public static void MapDeleteProductEndpoint(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapDelete("api/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                var response = result.Adapt<DeleteProductResponse>();
                return TypedResults.Ok(response);
            })
                .WithName("DeleteProduct")
                .WithDescription("Delete Product")
                .WithSummary("Delete Product")
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound);
        }
    }
}
