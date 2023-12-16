using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Domain.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Domain.Context;

public class IscasLuneContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Cor> Cores { get; set; }
    public DbSet<Tamanho> Tamanhos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<CoresProdutos> CoresProdutos { get; set; }
    public DbSet<TamanhosProdutos> TamanhosProdutos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BannerConfiguration());
        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new CorConfiguration());
        modelBuilder.ApplyConfiguration(new CoresProdutosConfiguration());
        modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
        modelBuilder.ApplyConfiguration(new TamanhoConfiguration());
        modelBuilder.ApplyConfiguration(new TamanhosProdutosConfiguration());
    }
}
