using KonusarakOgrenCase.Domain.Entities;

namespace KonusarakOgrenCase.Persistence.Abstract;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<Product?> GetByIdAsync(int id);
}