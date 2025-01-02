﻿using Catalog.API.Models;
using Utilities.CQRS;

namespace Catalog.API.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImagePath, decimal Price) : ICommand<CreateProductresult>;
    public record CreateProductresult(Guid Id);
    public class CreateProductHandlerCommandHandler : ICommandHandler<CreateProductCommand, CreateProductresult>
    {
        public async Task<CreateProductresult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //Create productfrom request
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                ImagePath = request.ImagePath,
                Price = request.Price
            };
            //Save product to database

            //Return result
            return await Task.FromResult(new CreateProductresult(product.Id));
        }
    }
}