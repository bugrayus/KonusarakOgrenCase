using KonusarakOgrenCase.Domain.Entities;
using KonusarakOgrenCase.Persistence.Abstract;
using Microsoft.EntityFrameworkCore;

namespace KonusarakOgrenCase.Persistence.Concrete;

public class CartRepository : GenericRepository<Cart>, ICartRepository
{
    public CartRepository(KonusarakOgrenCaseContext context) : base(context)
    {
    }

    #region GetByUserIdAsync

    public async Task<Cart?> GetByUserIdAsync(int userId)
    {
        return await Context.Carts
            .Include(e => e.Products)!
            .ThenInclude(e => e.Product)
            .FirstOrDefaultAsync(e => e.UserId == userId && e.IsActive);
    }

    #endregion
}