namespace iscaslune.Api.Domain.Entities;

public sealed class CoresProdutos
{
    public CoresProdutos(Guid id, Guid produtoId, Guid corId)
    {
        Id = id;
        ProdutoId = produtoId;
        CorId = corId;
    }

    public Guid Id { get; private set; }
    public Guid ProdutoId { get; private set; }
    public Produto Produto { get; set; } = null!;
    public Guid CorId { get; private set; }
    public Cor Cor { get; set; } = null!;
}
