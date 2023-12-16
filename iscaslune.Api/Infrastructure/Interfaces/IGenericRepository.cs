namespace iscaslune.Api.Infrastructure.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<bool> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}
