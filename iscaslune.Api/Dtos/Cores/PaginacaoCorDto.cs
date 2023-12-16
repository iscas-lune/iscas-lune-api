using iscaslune.Api.Infrastructure.Filters.Model;

namespace iscaslune.Api.Dtos.Cores;

public class PaginacaoCorDto : FilterModel
{
    public string? Descricao { get; set; }
}
