using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Base;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iscaslune.Api.Dtos.Produtos;

public class CreateProdutoDto : BaseDto<Produto>
{
    [Required]
    public string Foto { get; set; } = string.Empty;
    [Required]
    [MaxLength(255)]
    public string Descricao { get; set; } = string.Empty;
    public string? EspecificacaoTecnica { get; set; }
    public string? Referencia { get; set; }
    public Guid CategoriaId { get; set; }
    public override Produto ForEntity()
    {
        var date = DateTime.Now;
        var foto = Encoding.UTF8.GetBytes(Foto);

        return new Produto(Guid.NewGuid(), date, date, 0, Descricao, EspecificacaoTecnica, foto, CategoriaId, Referencia);
    }
}
