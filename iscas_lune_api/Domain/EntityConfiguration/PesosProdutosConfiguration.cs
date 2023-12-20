using iscaslune.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iscaslune.Api.Domain.EntityConfiguration;

public class PesosProdutosConfiguration : IEntityTypeConfiguration<PesosProdutos>
{
    public void Configure(EntityTypeBuilder<PesosProdutos> builder)
    {
        builder.HasKey(x => x.Id);        
    }
}
