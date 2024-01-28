using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Model.Usuarios;

namespace iscas_lune_api.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
    string GenerateTokenFuncionario(Funcionario funcionario);
    UsuarioViewModel GetClaims();
    bool IsFuncionario();
}
