using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscaslune.Api.Infrastructure.Cached;

public class BannerCached(IscasLuneContext context, BannerRepository bannerRepository, ICachedService<Banner> cachedService) 
    : GenericRepository<Banner>(context), IBannerRepository
{
    private readonly BannerRepository _bannerRepository = bannerRepository;
    private readonly ICachedService<Banner> _cachedService = cachedService;
    private const string _key = "banners";

    public async Task<List<Banner>> GetBannerAsync()
    {
        var banners = await _cachedService.GetListItemAsync(_key);

        if(banners == null)
        {
            banners = await _bannerRepository.GetBannerAsync();
            if(banners.Count > 0)
                await _cachedService.SetListItemAsync(_key, banners);
        }

        return banners;
    }
}
