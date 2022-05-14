using KonusarakOgrenCase.Application.Abstract;
using KonusarakOgrenCase.Domain.Entities;
using KonusarakOgrenCase.Domain.ResponseModels.Cart;
using KonusarakOgrenCase.Persistence.Abstract;

namespace KonusarakOgrenCase.Application.Concrete;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;

    public CartService(ICartRepository cartRepository, IUserRepository userRepository)
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
    }

    #region AddToCartAsync

    public async Task AddToCartAsync(int productId, int quantity)
    {
        var cart = await _cartRepository.GetByUserIdAsync(_userRepository.GetUserByToken()!.Id)
                   ?? await CreateCart();

        var cartItem = cart.Products?.FirstOrDefault(e => e.ProductId == productId);

        if (cartItem != null)
            cartItem.Quantity += quantity;
        else
            cart.Products?.Add(new CartProduct
            {
                ProductId = productId,
                Quantity = quantity,
                CartId = cart.Id
            });

        await _cartRepository.UpdateAsync(cart);
    }

    #endregion

    #region RemoveFromCartAsync

    public async Task RemoveFromCartAsync(int productId, int quantity)
    {
        var cart = await _cartRepository.GetByUserIdAsync(_userRepository.GetUserByToken()!.Id)
                   ?? await CreateCart();

        var cartItem = cart.Products?.FirstOrDefault(e => e.ProductId == productId);

        if (cartItem != null)
        {
            if (cartItem.Quantity > quantity)
                cartItem.Quantity -= quantity;
            else
                cart.Products?.Remove(cartItem);

            await _cartRepository.UpdateAsync(cart);
        }
    }

    #endregion

    #region GetCartItemAsync

    public async Task<CartProduct?> GetCartItemAsync(int productId)
    {
        var cart = await _cartRepository.GetByUserIdAsync(_userRepository.GetUserByToken()!.Id);
        if (cart != null)
            return cart.Products?.FirstOrDefault(e => e.ProductId == productId);
        await CreateCart();
        return null;
    }

    #endregion

    #region GetCartDetailsAsync

    public async Task<CartResponse> GetCartDetailsAsync()
    {
        var cart = await _cartRepository.GetByUserIdAsync(_userRepository.GetUserByToken()!.Id)
                   ?? await CreateCart();

        if (cart.Products == null || cart.Products.Count == 0)
            return new CartResponse();

        var cartItemResponse = cart.Products
            .Select(cartItem =>
                new CartItemResponse
                {
                    Quantity = cartItem.Quantity,
                    Product = cartItem.Product
                })
            .ToList();

        return new CartResponse
        {
            CartItems = cartItemResponse
        };
    }

    #endregion

    #region CreateCart

    public async Task<Cart> CreateCart()
    {
        var cart = new Cart
        {
            User = _userRepository.GetUserByToken()!
        };
        await _cartRepository.CreateAsync(cart);
        return cart;
    }

    #endregion
}