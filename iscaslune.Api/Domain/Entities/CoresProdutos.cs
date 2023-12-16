namespace iscaslune.Api.Domain.Entities;

public sealed class CoresProdutos(Guid id, Guid produtoId, Guid corId)
{
    public Guid Id { get; private set; } = id;
    public Guid ProdutoId { get; private set; } = produtoId;
    public Produto Produto { get; set; } = null!;
    public Guid CorId { get; private set; } = corId;
    public Cor Cor { get; set; } = null!;
}
