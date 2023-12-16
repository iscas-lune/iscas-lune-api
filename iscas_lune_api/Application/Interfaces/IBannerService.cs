using iscaslune.Api.Dtos.Banners;
using iscaslune.Api.Model.Banners;

namespace iscaslune.Api.Application.Interfaces;

public interface IBannerService
{
    Task<List<BannerViewModel>> GetBannersAsync();
    Task<BannerViewModel?> CreateBannerAsync(CreateBannerDto createBannerDto);
}
