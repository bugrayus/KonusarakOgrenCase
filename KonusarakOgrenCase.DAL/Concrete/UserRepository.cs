using KonusarakOgrenCase.Domain.Entities;
using KonusarakOgrenCase.Persistence.Abstract;
using KonusarakOgrenCase.Persistence.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KonusarakOgrenCase.Persistence.Concrete;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserRepository(KonusarakOgrenCaseContext context, IHttpContextAccessor httpContextAccessor) : base(context)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    #region GetByMailAsync

    public async Task<User?> GetByMailAsync(string mail)
    {
        return await Context.Users.FirstOrDefaultAsync(u => u.Mail == mail && u.IsActive);
    }

    #endregion

    #region GetByIdAsync

    public async Task<User?> GetByIdAsync(int id)
    {
        return await Context.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsActive);
    }

    #endregion

    #region GetUserByToken

    public User? GetUserByToken()
    {
        return _httpContextAccessor.HttpContext.GetThisUser(Context);
    }

    #endregion
}