namespace Basket.API.Models;

public class ShoppingCart
{
    public ShoppingCart(string userName, List<ShoppingCartItem> items)
    {
        UserName = userName;
    }
    public ShoppingCart()
    {

    }

    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(s => s.Price * s.Quantity);

}
