using iscaslune.Api.Infrastructure.Filters.Model;

namespace iscaslune.Api.Dtos.Produtos;

public class PaginacaoProdutoDto : FilterModel
{
    public string? Descricao { get; set; }
}
