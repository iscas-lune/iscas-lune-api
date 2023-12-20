using iscas_lune_api.Dtos.Usuarios;

namespace iscas_lune_api.Application.Interfaces;

public interface IEsqueceSenhaService
{
    Task<bool> RecuperarSenhaAsync(EsqueceuSenhaDto esqueceuSenhaDto);
}
