namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession sessions) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery Query, CancellationToken cancellationToken)
    {

        var products = await sessions.Query<Product>().ToPagedListAsync(Query.PageNumber ?? 1, Query.PageSize ?? 10, cancellationToken);

        return new GetProductsResult(products);
    }
}
