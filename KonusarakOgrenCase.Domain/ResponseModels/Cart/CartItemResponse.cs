using KonusarakOgrenCase.Domain.Entities;

namespace KonusarakOgrenCase.Domain.ResponseModels.Cart;

public class CartItemResponse
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}