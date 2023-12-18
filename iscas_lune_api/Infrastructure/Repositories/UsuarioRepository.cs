using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iscas_lune_api.Infrastructure.Repositories;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    private readonly IscasLuneContext _context;
    public UsuarioRepository(IscasLuneContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetUsuarioByEmailAsync(string email)
    {
        return await _context
            .Usuarios
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<Usuario?> GetUsuarioByIdAsync(Guid id)
    {
        return await _context
            .Usuarios
            .FirstOrDefaultAsync(x => x.Id == id);

    }
}
