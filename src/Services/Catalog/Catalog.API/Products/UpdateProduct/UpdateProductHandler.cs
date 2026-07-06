using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Desctiption, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Is Required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category Is Required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile Is Required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Should Be Greater than zero");
    }
}

internal class UpdateProductCommandHandler(IDocumentSession sessions, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle Has Been Called With {@command}.", command);

        var product = await sessions.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null) throw new ProductNotFoundException(command.Id);

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Desctiption;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        sessions.Update(product);
        await sessions.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}
