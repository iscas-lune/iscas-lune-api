using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Infrastructure.Repositories;
using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscas_lune_api.Infrastructure.Cached;

public class UsuarioCached : GenericRepository<Usuario>, IUsuarioRepository
{
    private readonly ICachedService<Usuario> _cachedService;
    private readonly UsuarioRepository _usuarioRepository;
    public UsuarioCached(IscasLuneContext context, ICachedService<Usuario> cachedService, UsuarioRepository usuarioRepository = null) : base(context)
    {
        _cachedService = cachedService;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario?> GetUsuarioByEmailAsync(string email) =>
        await _usuarioRepository.GetUsuarioByEmailAsync(email);

    public async Task<Usuario?> GetUsuarioByIdAsync(Guid id)
    {
        var key = id.ToString();
        var usuario = await _cachedService.GetItemAsync(key);

        if(usuario == null)
        {
            usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if(usuario != null)
                await _cachedService.SetItemAsync(key, usuario);
        }

        return usuario;
    }
}
