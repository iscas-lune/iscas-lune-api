using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Base;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iscaslune.Api.Dtos.Banners;

public class CreateBannerDto : BaseDto<Banner>
{
    [Required]
    public string Foto { get; set; } = string.Empty;
    public override Banner ForEntity()
    {
        var foto = Encoding.UTF8.GetBytes(Foto);
        var date = DateTime.Now;
        return new Banner(
            Guid.NewGuid(),
            date,
            date,
            0,
            foto,
            true);
    }
}
