using iscaslune.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iscaslune.Api.Domain.EntityConfiguration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
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
        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(x => x.Descricao);
        builder.Property(x => x.EspecificacaoTecnica)
            .HasDefaultValue(null)
            .HasMaxLength(1000);
        builder.Property(x => x.Foto)
            .IsRequired();
        builder.Property(x => x.Referencia)
            .HasMaxLength(255)
            .HasDefaultValue(null);
        builder.HasMany(x => x.Cores)
            .WithMany(x => x.Produtos)
            .UsingEntity<CoresProdutos>();
        builder.HasMany(x => x.Tamanhos)
            .WithMany(x => x.Produtos)
            .UsingEntity<TamanhosProdutos>();
    }
}
