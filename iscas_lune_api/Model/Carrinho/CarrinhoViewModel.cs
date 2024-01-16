using iscaslune.Api.Model.Categorias;
using iscaslune.Api.Model.Cores;
using iscaslune.Api.Model.Tamanhos;
using System.Text.Json.Serialization;

namespace iscas_lune_api.Model.Carrinho;

public class CarrinhoViewModel
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EspecificacaoTecnica { get; set; }
    public string Foto { get; set; } = string.Empty;
    public List<TamanhoCarrinhoViewModel>? Tamanhos { get; set; } = new();
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<PesoCarrinhoViewModel>? Pesos { get; set; } = new();
    public Guid CategoriaId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CategoriaViewModel? Categoria { get; set; } = null!;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Referencia { get; set; }
}
