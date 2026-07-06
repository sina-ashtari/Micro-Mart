namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession sessions, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery Query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle Has Been Called With {@Query}.", Query);

        var products = await sessions.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}
