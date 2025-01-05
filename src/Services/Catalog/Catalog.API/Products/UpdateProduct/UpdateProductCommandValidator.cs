namespace Catalog.API.Products.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name maximum length is 50");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImagePath).NotEmpty().WithMessage("Image is required");
        }
    }
}
