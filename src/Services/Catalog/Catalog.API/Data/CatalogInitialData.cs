using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        // The Data Seeding Done Before
        if (await session.Query<Product>().AnyAsync()) return;

        session.Store<Product>(GetPreConfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>
    {
        // the seeding data, Im "Goshad"
    };
}
