
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsDeleted);

    internal class DeleteProductCommandHandler
        (IDocumentSession documentSession, ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Deleting product with command {command}");
            var product = await documentSession.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            documentSession.Delete(product);
            await documentSession.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
