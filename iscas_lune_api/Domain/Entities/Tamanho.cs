﻿using iscaslune.Api.Domain.Entities.Bases;

namespace iscaslune.Api.Domain.Entities;

public sealed class Tamanho: BaseEntity
{
    public Tamanho(Guid id, DateTime dataCriacao, DateTime dataAtualizacao, long numero, string descricao) : base(id, dataCriacao, dataAtualizacao, numero)
    {
        Descricao = descricao;
    }

    public string Descricao { get; private set; }
    public List<Produto> Produtos { get; set; } = new();
}