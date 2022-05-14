using KonusarakOgrenCase.Domain.Entities;

namespace KonusarakOgrenCase.Persistence.Abstract;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByMailAsync(string mail);
    Task<User?> GetByIdAsync(int id);
    User? GetUserByToken();
}