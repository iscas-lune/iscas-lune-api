using iscas_lune_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iscas_lune_api.Domain.EntityConfiguration;

public class ItensTabelaDePrecoConfiguration : IEntityTypeConfiguration<ItensTabelaDePreco>
{
    public void Configure(EntityTypeBuilder<ItensTabelaDePreco> builder)
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
        builder.Property(x => x.ValorUnitario)
            .IsRequired()
            .HasPrecision(12, 2);
        builder.HasOne(x => x.Produto)
            .WithMany(x => x.ItensTabelaDePreco)
            .HasForeignKey(x => x.ProdutoId);
    }
}
