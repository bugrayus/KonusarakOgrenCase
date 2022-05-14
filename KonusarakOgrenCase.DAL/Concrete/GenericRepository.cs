using KonusarakOgrenCase.Persistence.Abstract;

namespace KonusarakOgrenCase.Persistence.Concrete;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly KonusarakOgrenCaseContext Context;

    public GenericRepository(KonusarakOgrenCaseContext context)
    {
        Context = context;
    }

    #region UpdateAsync

    public async Task UpdateAsync(T model)
    {
        Context.Set<T>().Update(model);
        await Context.SaveChangesAsync();
    }

    #endregion

    #region CreateAsync

    public async Task CreateAsync(T model)
    {
        await Context.Set<T>().AddAsync(model);
        await Context.SaveChangesAsync();
    }

    #endregion
}