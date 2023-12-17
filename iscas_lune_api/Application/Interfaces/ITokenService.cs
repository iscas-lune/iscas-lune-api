using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Model;

namespace iscas_lune_api.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
    UsuarioViewModel? GetClaims();
}
