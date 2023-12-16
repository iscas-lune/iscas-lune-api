using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iscaslune.Api.Infrastructure.Repositories;

public class BannerRepository(IscasLuneContext context) 
    : GenericRepository<Banner>(context), IBannerRepository
{
    private readonly IscasLuneContext _context = context;

    public async Task<List<Banner>> GetBannerAsync()
    {
        return await _context.Banners.Where(x => x.Ativo).ToListAsync();
    }
}
