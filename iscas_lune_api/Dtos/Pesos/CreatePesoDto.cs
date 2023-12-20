using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Base;
using System.ComponentModel.DataAnnotations;

namespace iscaslune.Api.Dtos.Cores;

public class CreatePesoDto : BaseDto<Peso>
{
    [Required]
    [MaxLength(255)]
    public string Descricao { get; set; } = string.Empty;

    public override Peso ForEntity()
    {
        var date = DateTime.Now;
        return new(Guid.NewGuid(), date, date, 0, Descricao);
    }
}
