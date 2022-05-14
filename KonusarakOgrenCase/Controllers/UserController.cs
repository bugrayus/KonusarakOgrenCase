using KonusarakOgrenCase.Application.Abstract;
using KonusarakOgrenCase.Application.Common;
using KonusarakOgrenCase.Domain.RequestModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KonusarakOgrenCase.API.Controllers;

[Route("api/user")]
[ApiController]
[ValidateModel]
[Authorize]
public class UserController : BaseApiController
{
    private readonly ICartService _cartService;
    private readonly IUserService _userService;

    public UserController(IUserService userService, ICartService cartService)
    {
        _userService = userService;
        _cartService = cartService;
    }

    #region LoginAsync

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var user = await _userService.LoginAsync(request);
        return Success("Successfully logged in.", null, user);
    }

    #endregion

    #region GetCartDetailsAsync

    [HttpGet("cart")]
    public async Task<IActionResult> GetCartDetailsAsync()
    {
        var cartDetails = await _cartService.GetCartDetailsAsync();
        return Success("Cart details fetched successfully.", null, cartDetails);
    }

    #endregion
}