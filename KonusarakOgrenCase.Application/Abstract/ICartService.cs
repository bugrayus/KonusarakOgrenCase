using KonusarakOgrenCase.Domain.Entities;
using KonusarakOgrenCase.Domain.ResponseModels.Cart;

namespace KonusarakOgrenCase.Application.Abstract;

public interface ICartService
{
    Task RemoveFromCartAsync(int productId, int quantity);
    Task<CartResponse> GetCartDetailsAsync();
    Task<CartProduct?> GetCartItemAsync(int productId);
    Task AddToCartAsync(int productId, int quantity);
}