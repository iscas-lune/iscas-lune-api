using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace iscas_lune_api.Infrastructure.Repositories;

public class LoginRepository : ILoginRepository
{
    private readonly IscasLuneContext _context;

    public LoginRepository(IscasLuneContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> LoginAsync(string email)
    {
        return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
    }
}
