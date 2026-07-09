namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken CancellationToken = default);
    Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default);
}
