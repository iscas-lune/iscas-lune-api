﻿using iscaslune.Api.Application.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Tamanhos;
using iscaslune.Api.Infrastructure.Interfaces;
using iscaslune.Api.Infrastructure.Repositories;

namespace iscaslune.Api.Infrastructure.Cached;

public class TamanhoCached
    : GenericRepository<Tamanho>, ITamanhoRepository
{
    private readonly TamanhoRepository _tamanhoRepository;
    private readonly ICachedService<Tamanho> _cachedService;
    private const string _keyList = "tamanhos";

    public TamanhoCached(IscasLuneContext context, TamanhoRepository tamanhoRepository, ICachedService<Tamanho> cachedService) : base(context)
    {
        _tamanhoRepository = tamanhoRepository;
        _cachedService = cachedService;
    }

    public async Task<Tamanho?> GetTamanhoByIdAsync(Guid id)
    {
        var key = id.ToString();
        var tamanho = await _cachedService.GetItemAsync(key);

        if (tamanho == null)
        {
            tamanho = await _tamanhoRepository.GetTamanhoByIdAsync(id);
            if (tamanho != null) await _cachedService.SetItemAsync(key, tamanho);
        }

        return tamanho;
    }

    public async Task<List<Tamanho>?> GetTamanhosAsync(PaginacaoTamanhoDto paginacaoTamanhoDto)
    {
        if (!string.IsNullOrWhiteSpace(paginacaoTamanhoDto.Descricao)
            || paginacaoTamanhoDto.Asc)
            return await _tamanhoRepository.GetTamanhosAsync(paginacaoTamanhoDto);

        var tamanhos = await _cachedService.GetListItemAsync(_keyList);

        if(tamanhos == null)
        {
            tamanhos = await _tamanhoRepository.GetTamanhosAsync(paginacaoTamanhoDto);
            if(tamanhos != null) await _cachedService.SetListItemAsync(_keyList, tamanhos);
        }

        return tamanhos;    
    }
}
