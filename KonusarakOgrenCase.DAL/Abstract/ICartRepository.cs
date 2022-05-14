using KonusarakOgrenCase.Domain.Entities;

namespace KonusarakOgrenCase.Persistence.Abstract;

public interface ICartRepository : IGenericRepository<Cart>
{
    Task<Cart?> GetByUserIdAsync(int id);
}