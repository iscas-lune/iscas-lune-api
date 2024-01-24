using iscas_lune_api.Domain.Entities;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.Infrastructure.Repositories;
using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscas_lune_api.Infrastructure.Cached;

public class TabelaDePrecoCached : GenericRepository<TabelaDePreco>, ITabelaDePrecoRepository
{
    private readonly ICachedService<TabelaDePreco> _cachedService;
    private readonly TabelaDePrecoRepository _tabelaDePrecoRepository;
    private const string _key = "tabela-de-preco";
    public TabelaDePrecoCached(IscasLuneContext context, ICachedService<TabelaDePreco> cachedService, TabelaDePrecoRepository tabelaDePrecoRepository) : base(context)
    {
        _cachedService = cachedService;
        _tabelaDePrecoRepository = tabelaDePrecoRepository;
    }

    public async Task<TabelaDePreco?> GetTabelaDePrecoAtivaEcommerceAsync()
    {
        var tabelaDePreco = await _cachedService.GetItemAsync(_key);

        if (tabelaDePreco == null)
        {
            tabelaDePreco = await _tabelaDePrecoRepository.GetTabelaDePrecoAtivaEcommerceAsync();
            if (tabelaDePreco != null)
            {
                tabelaDePreco.ItensTabelaDePreco.ForEach(item =>
                {
                    item.TabelaDePreco = null;
                });
                await _cachedService.SetItemAsync(_key, tabelaDePreco);
            }
        }

        return tabelaDePreco;
    }
}
