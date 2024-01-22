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
    public DbSet<Peso> Pesos { get; set; }
    public DbSet<Tamanho> Tamanhos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<PesosProdutos> PesosProdutos { get; set; }
    public DbSet<TamanhosProdutos> TamanhosProdutos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<PrecoProduto> PrecosProduto { get; set; }
    public DbSet<PrecoProdutoPeso> PrecosProdutoPeso { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItensPedido> ItensPedidos { get; set; }
    public DbSet<PedidosEmAberto> PedidosEmAberto { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BannerConfiguration());
        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new PesoConfiguration());
        modelBuilder.ApplyConfiguration(new PesosProdutosConfiguration());
        modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
        modelBuilder.ApplyConfiguration(new TamanhoConfiguration());
        modelBuilder.ApplyConfiguration(new TamanhosProdutosConfiguration());
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        modelBuilder.ApplyConfiguration(new PrecoProdutoConfiguration());
        modelBuilder.ApplyConfiguration(new PrecoProdutoPesoConfiguration());
        modelBuilder.ApplyConfiguration(new PedidoConfiguration());
        modelBuilder.ApplyConfiguration(new ItensPedidoConfiguration());
        modelBuilder.ApplyConfiguration(new FuncionarioConfiguration());
    }
}
