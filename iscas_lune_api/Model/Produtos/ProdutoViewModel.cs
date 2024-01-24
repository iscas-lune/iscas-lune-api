using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Model.Base;
using iscaslune.Api.Model.Categorias;
using iscaslune.Api.Model.Cores;
using iscaslune.Api.Model.Tamanhos;
using System.Text;
using System.Text.Json.Serialization;

namespace iscaslune.Api.Model.Produtos;

public class ProdutoViewModel : BaseModel<Produto, ProdutoViewModel>
{
    public string Descricao { get; set; } = string.Empty;
    public string? EspecificacaoTecnica { get; set; }
    public string Foto { get; set; } = string.Empty;
    public List<TamanhoViewModel>? Tamanhos { get; set; } = new();
    public List<PesoViewModel>? Pesos { get; set; } = new();
    public Guid CategoriaId { get; set; }
    public CategoriaViewModel? Categoria { get; set; } = null!;
    public string? Referencia { get; private set; }
    public override ProdutoViewModel? ForModel(Produto? entity)
    {
        if (entity == null) return null;

        Id = entity.Id;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        Descricao = entity.Descricao;
        EspecificacaoTecnica = entity.EspecificacaoTecnica;
        Foto = Encoding.UTF8.GetString(entity.Foto);
        Tamanhos = entity.Tamanhos.OrderBy(x => x.Numero).Select(x => new TamanhoViewModel().ForModel(x) ?? new()).ToList();
        Pesos = entity.Pesos.OrderBy(x => x.Numero).Select(x => new PesoViewModel().ForModel(x) ?? new()).ToList();
        Categoria = new CategoriaViewModel().ForModel(entity.Categoria);
        CategoriaId = entity.CategoriaId;
        Referencia = entity.Referencia;
        return this;
    }
}
