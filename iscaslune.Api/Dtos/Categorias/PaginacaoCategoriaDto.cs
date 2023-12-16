using iscaslune.Api.Infrastructure.Filters.Model;

namespace iscaslune.Api.Dtos.Categorias;

public class PaginacaoCategoriaDto : FilterModel
{
    public string? Descricao { get; set; }
}
