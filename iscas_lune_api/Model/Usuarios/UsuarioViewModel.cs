﻿using iscas_lune_api.Domain.Entities;
using iscaslune.Api.Model.Base;

namespace iscas_lune_api.Model.Usuarios;

public class UsuarioViewModel : BaseModel<Usuario, UsuarioViewModel>
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Cnpj { get; set; }

    public override UsuarioViewModel? ForModel(Usuario? entity)
    {
        if (entity == null) return null;

        Id = entity.Id;
        DataCriacao = entity.DataCriacao;
        Email = entity.Email;
        Numero = entity.Numero;
        Telefone = entity.Telefone;
        Nome = entity.Nome;
        Cnpj = entity.Cnpj;
        return this;
    }
}
