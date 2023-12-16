using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Infrastructure.Repositories;

public class BannerRepository
    : GenericRepository<Banner>, IBannerRepository
{
    private readonly IscasLuneContext _context;

    public BannerRepository(IscasLuneContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Banner>> GetBannerAsync()
    {
        return await _context.Banners.Where(x => x.Ativo).ToListAsync();
    }
}
