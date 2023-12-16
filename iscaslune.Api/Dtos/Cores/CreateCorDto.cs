using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Base;
using System.ComponentModel.DataAnnotations;

namespace iscaslune.Api.Dtos.Cores;

public class CreateCorDto : BaseDto<Cor>
{
    [Required]
    [MaxLength(255)]
    public string Descricao { get; set; } = string.Empty;

    public override Cor ForEntity()
    {
        var date = DateTime.Now;
        return new(Guid.NewGuid(), date, date, 0, Descricao);
    }
}
