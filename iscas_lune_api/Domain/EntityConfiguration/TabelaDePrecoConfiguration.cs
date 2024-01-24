using iscas_lune_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iscas_lune_api.Domain.EntityConfiguration;

public class TabelaDePrecoConfiguration : IEntityTypeConfiguration<TabelaDePreco>
{
    public void Configure(EntityTypeBuilder<TabelaDePreco> builder)
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
        builder.Property(x => x.AtivaEcommerce)
            .IsRequired();
        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasMany(x => x.ItensTabelaDePreco)
            .WithOne(x => x.TabelaDePreco)
            .HasForeignKey(x => x.TabelaDePrecoId);
    }
}
