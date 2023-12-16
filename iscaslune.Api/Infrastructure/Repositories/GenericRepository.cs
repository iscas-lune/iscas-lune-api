using iscaslune.Api.Domain.Context;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Infrastructure.Repositories;

public class GenericRepository<T>(IscasLuneContext context) : IGenericRepository<T> where T : class
{
    private readonly IscasLuneContext _context = context;

    public async Task<bool> AddAsync(T entity)
    {
        //_context.Entry(entity).Property("Numero").CurrentValue = await GetNextNumberAsync();
        await _context.Set<T>().AddAsync(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.Entry(entity).Property("DataCriacao").IsModified = false;
        _context.Entry(entity).Property("DataAtualizacao").IsModified = false;
        _context.Entry(entity).Property("Numero").IsModified = false;
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<long> GetNextNumberAsync()
    {
        try
        {
            return await _context
                .Set<T>()
                .AsQueryable()
                .MaxAsync(x => EF.Property<long>(x, "Numero")) + 1;
        }
        catch (Exception)
        {
            return 1;
        }
    }
}
