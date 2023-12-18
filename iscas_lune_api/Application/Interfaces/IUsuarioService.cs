using iscas_lune_api.Dtos.Usuarios;
using iscas_lune_api.Model.Usuarios;

namespace iscas_lune_api.Application.Interfaces;

public interface IUsuarioService
{
    UsuarioViewModel? GetConta();
    Task<UsuarioCreateViewModel?> CreateUsuarioAsync(CreateUsuarioDto createUsuarioDto);
}
