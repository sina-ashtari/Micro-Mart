using BuildingBlocks.CQRS;
using Catalog.API.Models;

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


internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
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


        // return a result CreateProductResult

        return new CreateProductResult(Guid.NewGuid());
        
    }
}
