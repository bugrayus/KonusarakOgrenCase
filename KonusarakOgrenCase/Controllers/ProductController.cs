using KonusarakOgrenCase.Application.Abstract;
using KonusarakOgrenCase.Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KonusarakOgrenCase.API.Controllers;

[Route("api/product")]
[ApiController]
[ValidateModel]
[Authorize]
public class ProductController : BaseApiController
{
    private readonly ICartService _cartService;
    private readonly IProductService _productService;

    public ProductController(ICartService cartService, IProductService productService)
    {
        _cartService = cartService;
        _productService = productService;
    }

    #region AddToCartAsync

    [HttpPost("{id}/cart")]
    public async Task<IActionResult> AddToCartAsync([FromRoute(Name = "id")] int productId, int quantity)
    {
        if (productId <= 0)
            return BadRequest("Product id cannot be zero or less.", null, false);

        if (quantity <= 0)
            return BadRequest("Quantity cannot be zero or less.", null, false);

        var product = await _productService.GetByIdAsync(productId);

        if (product == null)
            return NotFound("Product not found by that id.", null, false);

        await _cartService.AddToCartAsync(productId, quantity);

        return Success("Cart item added successfully.", null, true);
    }

    #endregion

    #region RemoveFromCartAsync

    [HttpPut("{id}/cart")]
    public async Task<IActionResult> RemoveFromCartAsync([FromRoute(Name = "id")] int productId, int quantity)
    {
        if (productId <= 0)
            return BadRequest("Product id cannot be zero or less.", null, false);

        if (quantity <= 0)
            return BadRequest("Quantity cannot be zero or less.", null, false);

        var product = await _productService.GetByIdAsync(productId);

        if (product == null)
            return NotFound("Product not found by that id.", null, false);

        var cartItem = await _cartService.GetCartItemAsync(productId);

        if (cartItem == null)
            return NotFound("Item you want to remove does not exist in cart.", null, false);

        await _cartService.RemoveFromCartAsync(productId, quantity);

        return Success("Cart item removed successfully.", null, true);
    }

    #endregion
}