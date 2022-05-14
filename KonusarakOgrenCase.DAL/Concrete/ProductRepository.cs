using KonusarakOgrenCase.Domain.Entities;
using KonusarakOgrenCase.Persistence.Abstract;
using Microsoft.EntityFrameworkCore;

namespace KonusarakOgrenCase.Persistence.Concrete;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(KonusarakOgrenCaseContext context) : base(context)
    {
    }

    #region GetByIdAsync

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await Context.Products.FirstOrDefaultAsync(e => e.Id == id);
    }

    #endregion
}