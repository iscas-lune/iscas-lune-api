using iscas_lune_api.Dtos.Usuarios;
using iscas_lune_api.Model.Login;
using iscas_lune_api.Model.Usuarios;

namespace iscas_lune_api.Application.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioViewModel?> GetConta();
    Task<ResponseLoginModel?> CreateUsuarioAsync(CreateUsuarioDto createUsuarioDto);
    Task<ResponseLoginModel?> UpdateUsuarioAsync(UpdateUsuarioDto updateUsuarioDto);
    Task<bool> UpdateSenhaAsync(UpdateSenhaUsuarioDto updateSenhaUsuarioDto);
}
