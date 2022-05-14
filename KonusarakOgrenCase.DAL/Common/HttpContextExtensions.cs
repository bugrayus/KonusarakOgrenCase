using System.Security.Claims;
using KonusarakOgrenCase.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace KonusarakOgrenCase.Persistence.Common;

public static class HttpContextExtensions
{
    public static User? GetThisUser(this HttpContext context, KonusarakOgrenCaseContext dbContext)
    {
        if (!context.User.Claims.Any()) return null;

        var claims = context.User.Claims.ToDictionary(u => u.Type, u => u.Value);
        var userId = claims[ClaimTypes.NameIdentifier];
        var user = dbContext.Users.FirstOrDefault(s => s.Id == int.Parse(userId) && s.IsActive);

        return user;
    }
}