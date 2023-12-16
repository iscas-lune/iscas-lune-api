using iscaslune.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iscaslune.Api.Domain.EntityConfiguration;

public class CoresProdutosConfiguration : IEntityTypeConfiguration<CoresProdutos>
{
    public void Configure(EntityTypeBuilder<CoresProdutos> builder)
    {
        builder.HasKey(x => x.Id);        
    }
}
