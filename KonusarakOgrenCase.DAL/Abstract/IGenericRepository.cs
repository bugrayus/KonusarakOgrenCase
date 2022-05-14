namespace KonusarakOgrenCase.Persistence.Abstract;

public interface IGenericRepository<in T> where T : class
{
    Task UpdateAsync(T model);
    Task CreateAsync(T model);
}