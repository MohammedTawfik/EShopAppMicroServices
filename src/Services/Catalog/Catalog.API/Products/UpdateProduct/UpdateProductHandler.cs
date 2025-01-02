
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImagePath, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsUpdated);

    internal class UpdateProductCommandHandler
        (IDocumentSession documentSession, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Updating product with command {command}");
            var product = await documentSession.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException();
            }
            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImagePath = command.ImagePath;
            product.Price = command.Price;
            documentSession.Update(product);
            await documentSession.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
