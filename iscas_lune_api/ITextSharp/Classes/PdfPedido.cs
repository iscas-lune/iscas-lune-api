﻿using iscas_lune_api.Application.Interfaces;
using iscas_lune_api.Infrastructure.Interfaces;
using iscas_lune_api.ITextSharp.Interfaces;
using iscaslune.Api;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;

namespace iscas_lune_api.ITextSharp.Classes;

public class PdfPedido : IPdfPedido
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IList<string> _celulas;
    private readonly IEmailService _emailService;

    public PdfPedido(IPedidoRepository pedidoRepository, IEmailService emailService)
    {
        _celulas = new List<string>()
        {
            "REF",
            "Descrição",
            "Tamanho/Peso",
            "Quantidade",
            "Valor unitário",
            "Total"
        };
        _pedidoRepository = pedidoRepository;
        _emailService = emailService;
    }

    public async Task<bool> GeneratePdfAsync(Guid pedidoId)
    {
        try
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(pedidoId);
            if (pedido == null) return false;

            using var memoryStream = new MemoryStream();

            var cultureInfo = new CultureInfo("pt-BR");
            var formato = new NumberFormatInfo
            {
                NumberDecimalSeparator = ",",
                NumberGroupSeparator = ".",
                NumberDecimalDigits = 2
            };

            var font = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fontCabecalho = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            float border = 0.5f;

            var document = new Document(PageSize.A4);
            document.SetMargins(15f, 15f, 15f, 20f);

            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

            var tableEmpresa = new PdfPTable(1)
            {
                PaddingTop = 0,
            };
            tableEmpresa.DefaultCell.FixedHeight = 100;
            tableEmpresa.WidthPercentage = 100;

            var cellEmpresa = new PdfPCell(new Phrase($" \n Iscas Lune \n\n\n\n Pedido - {pedido.Numero} \n ", fontCabecalho))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                BorderWidth = 0,
                BorderWidthBottom = border
            };

            var cellFake = new PdfPCell()
            {
                BorderWidth = 0,
            };

            tableEmpresa.AddCell(cellEmpresa);

            var tableContato = new PdfPTable(1);
            tableContato.DefaultCell.FixedHeight = 20;
            tableContato.WidthPercentage = 100;

            var dadosUsuario = $"\n Cliente : {pedido.Usuario.Nome} \n E-mail : {pedido.Usuario.Email}\n Telefone : {pedido.Usuario.Telefone ?? "N/A"}\n CNPJ : {pedido.Usuario.Cnpj ?? "Sem CNPJ"}\n\n";
            var cellCliente = new PdfPCell(new Phrase(dadosUsuario, fontCabecalho))
            {
                BorderWidth = 0,
                BorderWidthBottom = border
            };
            tableContato.AddCell(cellCliente);

            var tableCabecalhoProdutos = new PdfPTable(_celulas.Count);
            tableCabecalhoProdutos.DefaultCell.FixedHeight = 15;
            tableCabecalhoProdutos.WidthPercentage = 100;

            float[] columnWidths = new float[_celulas.Count];

            for (int i = 0; i < _celulas.Count; i++)
            {
                var newCell = new PdfPCell(new Phrase(_celulas.ElementAt(i), font))
                {
                    HorizontalAlignment = 0,
                    VerticalAlignment = 1,
                    BorderWidth = 0,
                    BorderWidthBottom = border
                };

                columnWidths[i] = _celulas[i].Equals("Descrição") ? 20f : 12f;
                tableCabecalhoProdutos.AddCell(newCell);
            }

            tableCabecalhoProdutos.SetWidths(columnWidths);

            var tableDadosProdutos = new PdfPTable(_celulas.Count);
            tableDadosProdutos.DefaultCell.FixedHeight = 15;
            tableDadosProdutos.WidthPercentage = 100;

            pedido.ItensPedido = pedido.ItensPedido.OrderBy(x => x.Numero).ToList();
            var itemsPedidosGroup = pedido.ItensPedido.GroupBy(x => x.ProdutoId);
            const int borderWidth = 0;

            foreach (var itemGroup in itemsPedidosGroup)
            {
                foreach (var item in itemGroup)
                {
                    var tamanhoPesoAtual = item.Tamanho != null ?
                    item.Tamanho.Descricao :
                    item.Peso?.Descricao ?? "";

                    var refProduto = new PdfPCell(new Phrase($"REF: {item.Produto.Referencia}", font)) { BorderWidth = borderWidth };
                    var descricao = new PdfPCell(new Phrase(item.Produto.Descricao, font)) { BorderWidth = borderWidth };
                    var tamanhoPeso = new PdfPCell(new Phrase(tamanhoPesoAtual, font)) { BorderWidth = borderWidth };
                    var quantidade = new PdfPCell(new Phrase(item.Quantidade.ToString(), font)) { BorderWidth = borderWidth };
                    var valorUnitario = new PdfPCell(new Phrase(item.ValorUnitario.ToString("N", formato), font)) { BorderWidth = borderWidth };
                    var total = new PdfPCell(new Phrase($"R$ {item.ValorTotal.ToString("N", formato)}", font)) { BorderWidth = borderWidth };

                    tableDadosProdutos.AddCell(refProduto);
                    tableDadosProdutos.AddCell(descricao);
                    tableDadosProdutos.AddCell(tamanhoPeso);
                    tableDadosProdutos.AddCell(quantidade);
                    tableDadosProdutos.AddCell(valorUnitario);
                    tableDadosProdutos.AddCell(total);
                }
            }

            tableDadosProdutos.SetWidths(columnWidths);

            var eventHelper = new CustomPageEventHandler();
            writer.PageEvent = eventHelper;

            var tableTotalizacao = new PdfPTable(_celulas.Count);
            tableTotalizacao.DefaultCell.FixedHeight = 15;
            tableTotalizacao.WidthPercentage = 100;
            tableTotalizacao.HorizontalAlignment = Element.ALIGN_RIGHT;

            var cellQtdTotal = new PdfPCell(new Phrase($" Total :  R$ {pedido.ValorTotal.ToString("N", formato)}", fontCabecalho))
            {
                BorderWidth = 0,
                HorizontalAlignment = PdfPCell.ALIGN_LEFT,
                BorderWidthTop = border,
            };

            tableTotalizacao.AddCell(cellFake);
            tableTotalizacao.AddCell(cellFake);
            tableTotalizacao.AddCell(cellFake);
            tableTotalizacao.AddCell(cellFake);
            tableTotalizacao.AddCell(cellFake);

            tableTotalizacao.AddCell(cellQtdTotal);

            var tableTotalizacaoQuantidade = new PdfPTable(_celulas.Count);
            tableTotalizacaoQuantidade.DefaultCell.FixedHeight = 15;
            tableTotalizacaoQuantidade.WidthPercentage = 100;
            tableTotalizacaoQuantidade.HorizontalAlignment = Element.ALIGN_RIGHT;

            var tamamhosItens = pedido
                .ItensPedido
                .OrderBy(x => x.Tamanho?.Numero)
                .Select(x => x.Tamanho)
                .ToList()
                .GroupBy(x => x?.Id);

            foreach (var tamanhoGroup in tamamhosItens)
            {
                var itemPedido = pedido
                    .ItensPedido
                    .FirstOrDefault(x => x.TamanhoId == tamanhoGroup.Key);

                var totalQuantidade = pedido
                    .ItensPedido
                    .Where(x => x.TamanhoId == tamanhoGroup.Key)
                    .ToList()
                    .Sum(x => x.Quantidade);

                var cellTamanhoTotal = new PdfPCell(new Phrase($"{itemPedido?.Tamanho?.Descricao} : {totalQuantidade.ToString("N", formato)}", font))
                {
                    BorderWidth = 0,
                    HorizontalAlignment = PdfPCell.ALIGN_LEFT,
                    BorderWidthTop = border
                };

                tableTotalizacaoQuantidade.AddCell(cellFake);
                tableTotalizacaoQuantidade.AddCell(cellFake);
                tableTotalizacaoQuantidade.AddCell(cellFake);
                tableTotalizacaoQuantidade.AddCell(cellFake);
                tableTotalizacaoQuantidade.AddCell(cellFake);

                tableTotalizacaoQuantidade.AddCell(cellTamanhoTotal);
            }

            document.Open();

            document.Add(tableEmpresa);
            document.Add(tableContato);
            document.Add(tableCabecalhoProdutos);
            document.Add(tableDadosProdutos);
            document.Add(tableTotalizacao);
            document.Add(tableTotalizacaoQuantidade);

            document.Close();

            var emailPedido = EnvironmentVariable.GetVariable("EMAIL_PEDIDO");
            var message = $"Que ótima noticia, mais um pedido!\nN. do pedido : {pedido.Numero}";
            var assunto = "Novo pedido";
            return _emailService.SendEmail(emailPedido, message, assunto, memoryStream.ToArray(), $"pedido-{pedido.Numero}", "application/pdf");
        }
        catch (Exception)
        {
            return false;
        }
    }

}
