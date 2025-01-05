namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImagePath, decimal Price) : ICommand<CreateProductresult>;
    public record CreateProductresult(Guid Id);

    public class CreateProductHandlerCommandHandler
        (IDocumentSession documentSession, IValidator<CreateProductCommand> validator)
        : ICommandHandler<CreateProductCommand, CreateProductresult>
    {
        public async Task<CreateProductresult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Validate command
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
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
