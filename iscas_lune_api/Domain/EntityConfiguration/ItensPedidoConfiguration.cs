﻿using iscas_lune_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iscas_lune_api.Domain.EntityConfiguration;

public class ItensPedidoConfiguration : IEntityTypeConfiguration<ItensPedido>
{
    public void Configure(EntityTypeBuilder<ItensPedido> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DataCriacao)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("now()");
        builder.Property(x => x.DataAtualizacao)
            .IsRequired()
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("now()");
        builder.Property(x => x.Numero)
            .ValueGeneratedOnAdd();
        builder.Ignore(x => x.ValorTotal);
        builder.Property(x => x.ValorUnitario)
            .IsRequired()
            .HasPrecision(12, 2);
        builder.Property(x => x.Quantidade)
            .IsRequired()
            .HasPrecision(12, 2);
        builder.HasOne(x => x.Tamanho)
            .WithMany(x => x.ItensPedido)
            .HasForeignKey(x => x.TamanhoId);
        builder.HasOne(x => x.Peso)
            .WithMany(x => x.ItensPedido)
            .HasForeignKey(x => x.PesoId);
        builder.HasOne(x => x.Produto)
            .WithMany(x => x.ItensPedido)
            .HasForeignKey(x => x.ProdutoId);
    }
}
