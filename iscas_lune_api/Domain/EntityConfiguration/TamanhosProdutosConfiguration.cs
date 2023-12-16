using iscaslune.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iscaslune.Api.Domain.EntityConfiguration;

public class TamanhosProdutosConfiguration : IEntityTypeConfiguration<TamanhosProdutos>
{
    public void Configure(EntityTypeBuilder<TamanhosProdutos> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
