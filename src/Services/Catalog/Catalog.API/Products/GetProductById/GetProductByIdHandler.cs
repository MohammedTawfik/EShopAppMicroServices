namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);

    internal class GetProductByIdQueryHandler
        (IDocumentSession documentSession, ILogger<GetProductByIdQueryHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var productId = query.Id;
            var product = await documentSession.LoadAsync<Product>(productId, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(productId);
            }
            return new GetProductByIdResult(product);
        }
    }
}
