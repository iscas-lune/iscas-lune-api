using iscaslune.Api.Domain.Entities;

namespace iscaslune.Api.Infrastructure.Interfaces;

public interface IBannerRepository : IGenericRepository<Banner>
{
    Task<List<Banner>> GetBannerAsync();
}
