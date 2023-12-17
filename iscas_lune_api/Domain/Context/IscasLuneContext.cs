using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Domain.EntityConfiguration;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Domain.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Domain.Context;

public class IscasLuneContext : DbContext
{
    public IscasLuneContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Cor> Cores { get; set; }
    public DbSet<Tamanho> Tamanhos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<CoresProdutos> CoresProdutos { get; set; }
    public DbSet<TamanhosProdutos> TamanhosProdutos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BannerConfiguration());
        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new CorConfiguration());
        modelBuilder.ApplyConfiguration(new CoresProdutosConfiguration());
        modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
        modelBuilder.ApplyConfiguration(new TamanhoConfiguration());
        modelBuilder.ApplyConfiguration(new TamanhosProdutosConfiguration());
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
    }
}
