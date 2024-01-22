using iscas_lune_api.Domain.Entities;

namespace iscas_lune_api.Infrastructure.Interfaces;

public interface IFuncionarioRepository
{
    Task<Funcionario?> LoginFuncionarioAsync(string email);
}
