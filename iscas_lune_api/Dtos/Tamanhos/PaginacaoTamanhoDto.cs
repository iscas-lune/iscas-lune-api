using iscaslune.Api.Infrastructure.Filters.Model;

namespace iscaslune.Api.Dtos.Tamanhos;

public class PaginacaoTamanhoDto : FilterModel
{
    public string? Descricao { get; set; }
}
