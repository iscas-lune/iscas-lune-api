using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Base;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iscaslune.Api.Dtos.Categorias;

public class CreateCategoriaDto : BaseDto<Categoria>
{
    [Required]
    [MaxLength(255)]
    public string Descricao { get; set; } = string.Empty;
    public string? Foto { get; set; }

    public override Categoria ForEntity()
    {
        var date = DateTime.Now;
        var foto = !string.IsNullOrWhiteSpace(Foto)
            ? Encoding.UTF8.GetBytes(Foto) : null;
        return new Categoria(
            Guid.NewGuid(),
            date,
            date,
            0,
            Descricao,
            foto);
    }
}
