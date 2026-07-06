namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Desctiption, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

internal class UpdateProductCommandHandler(IDocumentSession sessions, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle Has Been Called With {@command}.", command);

        var product = await sessions.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null) throw new ProductNotFoundException();

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
