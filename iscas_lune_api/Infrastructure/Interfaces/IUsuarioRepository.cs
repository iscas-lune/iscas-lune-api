﻿using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Infrastructure.Interfaces;

namespace iscas_lune_api.Infrastructure.Interfaces;

public interface IUsuarioRepository : IGenericRepository<Usuario>
{
    Task<Usuario?> GetUsuarioByIdAsync(Guid id);
    Task<Usuario?> GetUsuarioByEmailAsync(string email);
}
