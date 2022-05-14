using KonusarakOgrenCase.Domain.Entities;

namespace KonusarakOgrenCase.Application.Abstract;

public interface IProductService
{
    Task<Product?> GetByIdAsync(int id);
}