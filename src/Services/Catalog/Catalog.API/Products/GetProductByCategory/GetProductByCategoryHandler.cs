using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category): IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession sessions) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {

        var products = await sessions.Query<Product>().Where(w => w.Category.Contains(query.Category)).ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
