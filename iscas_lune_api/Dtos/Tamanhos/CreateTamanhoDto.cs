using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Base;
using System.ComponentModel.DataAnnotations;

namespace iscaslune.Api.Dtos.Tamanhos;

public class CreateTamanhoDto : BaseDto<Tamanho>
{
    [Required]
    [MaxLength(255)]
    public string Descricao { get; set; } = string.Empty;
    public override Tamanho ForEntity()
    {
        var data = DateTime.Now;
        return new Tamanho(
            Guid.NewGuid(),
            data,
            data,
            0,
            Descricao);
    }
}
