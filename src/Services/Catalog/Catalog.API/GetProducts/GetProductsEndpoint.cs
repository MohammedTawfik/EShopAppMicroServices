using Catalog.API.Models;
using Mapster;
using MediatR;

namespace Catalog.API.GetProducts
{
    public record GetProductsResponse(IEnumerable<Product> Products);
    public static class GetProductsEndpoint
    {
        public static void MapGetProductsEndpoint(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("api/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                var response = result.Adapt<GetProductsResponse>();
                return TypedResults.Ok(response);
            })
                .WithName("GetProducts")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Gets all products")
                .WithDescription("Gets all products in the catalog");
        }
    }
}
