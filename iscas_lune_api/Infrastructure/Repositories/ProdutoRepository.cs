﻿using iscas_lune_api.Infrastructure.Filtros.Filtros;
using iscas_lune_api.Infrastructure.Interfaces;
using iscaslune.Api.Domain.Context;
using iscaslune.Api.Domain.Entities;
using iscaslune.Api.Dtos.Produtos;
using iscaslune.Api.Infrastructure.Filtros.Filtros;
using iscaslune.Api.Infrastructure.Interfaces;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Linq;

namespace iscaslune.Api.Infrastructure.Repositories;

public class ProdutoRepository
    : GenericRepository<Produto>, IProdutoRepository
{
    private readonly IscasLuneContext _context;
    private readonly ITamanhoProdutoRepository _tamanhoRepository;
    private static readonly int _take = 5;


    public ProdutoRepository(IscasLuneContext context, ITamanhoProdutoRepository tamanhoRepository) : base(context)
    {
        _context = context;
        _tamanhoRepository = tamanhoRepository;
    }

    public async Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        var produto = await _context
            .Produtos
            .Include(x => x.Categoria)
            .Include(x => x.Pesos)
            .Include(x => x.Tamanhos)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (produto != null)
        {
            produto.Categoria.Produtos = new();
            produto.Pesos.ForEach(peso =>
            {
                peso.Produtos = new();
            });
            produto.Tamanhos.ForEach(tamanho =>
            {
                tamanho.Produtos = new();
            });
        }

        return produto;
    }

    public async Task<(List<Produto>? produtos, int totalPage)> GetProdutosAsync(int page)
    {
        var newPage = page == 0 ? page : page - 1;

        var totalPages = await _context
            .Produtos
            .AsQueryable()
            .TotalPage(_take);

        var produtos = await _context
            .Produtos
            .AsNoTracking()
            .AsQueryable()
            .OrderBy(x => x.Numero)
            .Include(x => x.Categoria)
            .Skip(newPage * _take)
            .Take(_take)
            .ToListAsync();

        var tamanhos = await _tamanhoRepository.GetTamanhosProdutosAsync() ?? new();

        if (produtos.Count > 0)
        {
            produtos.ForEach(produto =>
            {
                produto.Categoria.Produtos = new();
                produto.Tamanhos = tamanhos
                    .Where(x => x.ProdutoId == produto.Id)
                    .Select(tm => new Tamanho(tm.Tamanho.Id, tm.Tamanho.DataCriacao, tm.Tamanho.DataAtualizacao, tm.Tamanho.Numero, tm.Tamanho.Descricao))
                    .ToList();
            });
        }

        return (produtos, totalPages);
    }

    public async Task<List<Produto>> GetProdutosByCarrinhoAsync(List<Guid> produtosIds)
    {
        var produtos = await _context
            .Produtos
            .AsQueryable()
            .Include(x => x.Categoria)
            .Include(x => x.Pesos)
            .Include(x => x.Tamanhos)
            .Where(x => produtosIds.Contains(x.Id))
            .AsNoTracking()
            .ToListAsync();

        produtos.ForEach(produto =>
        {
            produto.Categoria.Produtos = new();
            produto.Pesos.ForEach(peso =>
            {
                peso.Produtos = new();
            });
            produto.Tamanhos.ForEach(tamanho =>
            {
                tamanho.Produtos = new();
            });
        });

        return produtos;
    }

    public async Task<List<Produto>?> GetProdutosByCategoriaAsync(Guid categoriaId)
    {
        var produtos = await _context
            .Produtos
            .AsNoTracking()
            .AsQueryable()
            .OrderBy(x => x.Numero)
            .Include(x => x.Categoria)
            .Where(x => x.CategoriaId == categoriaId)
            .ToListAsync();

        var tamanhos = await _tamanhoRepository.GetTamanhosProdutosAsync() ?? new();

        if (produtos.Count > 0)
        {
            produtos.ForEach(produto =>
            {
                produto.Categoria.Produtos = new();
                produto.Tamanhos = tamanhos
                    .Where(x => x.ProdutoId == produto.Id)
                    .Select(tm =>
                        new Tamanho(tm.Tamanho.Id, tm.Tamanho.DataCriacao, tm.Tamanho.DataAtualizacao, tm.Tamanho.Numero, tm.Tamanho.Descricao)
                     )
                    .ToList();
            });
        }

        return produtos;
    }

    public async Task<int> GetTotalPageProdutosAsync()
    {
        return await _context
            .Produtos
            .AsQueryable()
            .TotalPage(_take);
    }
}
