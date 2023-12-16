using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Model.Base;
using System.Text;

namespace iscaslune.Api.Model.Banners;

public class BannerViewModel : BaseModel<Banner, BannerViewModel>
{
    public string Foto { get; set; } = string.Empty;
    public override BannerViewModel? ForModel(Banner? entity)
    {
        if (entity == null) return null;

        Id = entity.Id;
        DataCriacao = entity.DataCriacao;
        Numero = entity.Numero;
        Foto = Encoding.UTF8.GetString(entity.Foto);

        return this;
    }
}
