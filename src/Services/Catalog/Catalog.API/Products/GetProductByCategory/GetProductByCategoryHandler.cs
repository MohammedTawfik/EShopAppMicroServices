
namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);

    public class GetProductsByCategoryHandler
        (IDocumentSession documentSession, ILogger<GetProductsByCategoryHandler> logger)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation($"GetProductsByCategoryHandler.Handle called with query: {query}");
            var category = query.Category;
            var products = await documentSession.Query<Product>().Where(p => p.Category.Contains(category)).ToListAsync(cancellationToken);
            return new GetProductsByCategoryResult(products);
        }
    }
}
