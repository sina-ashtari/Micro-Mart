

namespace Catalog.API.Products.CreateProduct;

/// <summary>
/// The Data We need to create a product 
/// </summary>
/// <param name="Name"></param>
/// <param name="Category"></param>
/// <param name="Desctiption"></param>
/// <param name="ImageFile"></param>
/// <param name="Price"></param>
public record CreateProductCommand(string Name,List<string> Category, string Desctiption, string ImageFile, decimal Price) : ICommand<CreateProductResult>;

/// <summary>
/// result of create product
/// </summary>
/// <param name="Id"></param>
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category Is Required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile Is Required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Should Be Greater than zero");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Bussiness Logical To Create A Product 

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Desctiption,
            ImageFile = command.ImageFile,
            Price = command.Price
        };


        // save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // return a result CreateProductResult

        return new CreateProductResult(product.Id);
        
    }
}
