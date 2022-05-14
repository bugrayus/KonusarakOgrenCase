using KonusarakOgrenCase.Application.Abstract;
using KonusarakOgrenCase.Domain.Entities;
using KonusarakOgrenCase.Persistence.Abstract;

namespace KonusarakOgrenCase.Application.Concrete;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    #region GetByIdAsync

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    #endregion
}