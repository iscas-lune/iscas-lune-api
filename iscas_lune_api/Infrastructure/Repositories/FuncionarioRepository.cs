using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iscas_lune_api.Infrastructure.Repositories;

public class FuncionarioRepository : GenericRepository<Funcionario>, IFuncionarioRepository
{
    private readonly IscasLuneContext _context;
    public FuncionarioRepository(IscasLuneContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Funcionario?> LoginFuncionarioAsync(string email)
    {
        return await _context
            .Funcionarios
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);
    }
}
