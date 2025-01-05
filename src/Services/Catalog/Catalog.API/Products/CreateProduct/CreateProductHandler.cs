namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImagePath, decimal Price) : ICommand<CreateProductresult>;
    public record CreateProductresult(Guid Id);

    public class CreateProductCommandHandler
        (IDocumentSession documentSession, ILogger<CreateProductCommandHandler> logger)
        : ICommandHandler<CreateProductCommand, CreateProductresult>
    {
        public async Task<CreateProductresult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateProductCommandHandler.Handle called with command {Name}", command);
            //Create productfrom request
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImagePath = command.ImagePath,
                Price = command.Price
            };
            //Save product to database
            documentSession.Store(product);
            await documentSession.SaveChangesAsync();
            //Return result
            return await Task.FromResult(new CreateProductresult(product.Id));
        }
    }
}
