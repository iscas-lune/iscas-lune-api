using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Model.Funcionarios;
using iscas_lune_api.Model.Login;

namespace iscas_lune_api.Application.Interfaces;

public interface IFuncionarioService
{
    Task<(string token, FuncionarioViewModel? funcionario)> LoginFuncionarioAsync(RequestLoginModel requestLoginModel);
}
