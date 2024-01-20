using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Model.Base;
using iscaslune.Api.Model.Produtos;
using System.Text;

namespace iscaslune.Api.Model.Categorias;

public class CategoriaViewModel : BaseModel<Categoria, CategoriaViewModel>
{
    public string Descricao { get; set; } = string.Empty;
    public string? Foto { get; set; }
    public List<ProdutoViewModel>? Produtos { get; set; }

    public override CategoriaViewModel? ForModel(Categoria? entity)
    {
        if (entity == null) return null;

        Id = entity.Id;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        Descricao = entity.Descricao;
        Foto = entity.Foto != null ? Encoding.UTF8.GetString(entity.Foto) : null;
        Produtos = entity.Produtos.Select(x => new ProdutoViewModel().ForModel(x) ?? new()).ToList();

        return this;
    }
}
