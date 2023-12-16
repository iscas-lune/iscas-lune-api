namespace iscaslune.Api.Domain.Entities;

public sealed class TamanhosProdutos(Guid id, Guid produtoId, Guid tamanhoId)
{
    public Guid Id { get; private set; } = id;
    public Guid ProdutoId { get; private set; } = produtoId;
    public Produto Produto { get; set; } = null!;
    public Guid TamanhoId { get; private set; } = tamanhoId;
    public Tamanho Tamanho { get; set; } = null!;
}
