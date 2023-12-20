using iscas_lune_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iscas_lune_api.Domain.EntityConfiguration;

public class PrecoProdutoPesoConfiguration : IEntityTypeConfiguration<PrecoProdutoPeso>
{
    public void Configure(EntityTypeBuilder<PrecoProdutoPeso> builder)
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
        builder.Property(x => x.Preco)
            .IsRequired()
            .HasPrecision(12, 2);
        builder.Property(x => x.PrecoCusto)
            .HasPrecision(12, 2);
        builder.Property(x => x.PrecoPromocional)
            .HasPrecision(12, 2);
        builder.HasOne(x => x.Peso)
            .WithOne(x => x.PrecoProdutoPeso)
            .HasForeignKey<PrecoProdutoPeso>(x => x.PesoId);
    }
}
