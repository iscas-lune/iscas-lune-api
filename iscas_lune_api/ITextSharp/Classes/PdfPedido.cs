using iscas_lune_api.Application.Interfaces;
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
            var fontCabecalho = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            float border = 0.5f;

            var document = new Document(PageSize.A4);
            document.SetMargins(15f, 15f, 15f, 20f);

            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

            var table = new PdfPTable(1)
            {
                PaddingTop = 0
            };
            table.DefaultCell.FixedHeight = 100;
            table.WidthPercentage = 100;

            var cellEmpresa = new PdfPCell(
                new Phrase($" \n Iscas Lune \n\n\n\n\n Pedido - {pedido.Numero} \n "
                , fontCabecalho))
            {
                HorizontalAlignment = Element.ALIGN_LEFT
            };
            table.AddCell(cellEmpresa);

            var tableContato = new PdfPTable(1);
            tableContato.DefaultCell.FixedHeight = 20;
            tableContato.WidthPercentage = 100;

            var cellCliente = new PdfPCell(new Phrase($" Cliente : {pedido.Usuario.Nome}", fontCabecalho));
            tableContato.AddCell(cellCliente);

            var tableCabecalhoProdutos = new PdfPTable(_celulas.Count);
            tableCabecalhoProdutos.DefaultCell.FixedHeight = 15;
            tableCabecalhoProdutos.WidthPercentage = 100;

            float[] columnWidths = new float[_celulas.Count];

            for (int i = 0; i < _celulas.Count; i++)
            {
                var newCell = new PdfPCell(new Phrase(_celulas.ElementAt(i), font));

                newCell.HorizontalAlignment = 0;
                newCell.VerticalAlignment = 1;
                newCell.BorderWidth = 0;
                newCell.BorderWidthBottom = border;

                if (_celulas[i] == "Descrição")
                {
                    columnWidths[i] = 20f;
                }
                else
                {
                    columnWidths[i] = 12f;
                }

                tableCabecalhoProdutos.AddCell(newCell);
            }

            tableCabecalhoProdutos.SetWidths(columnWidths);

            var tableDadosProdutos = new PdfPTable(_celulas.Count);
            tableDadosProdutos.DefaultCell.FixedHeight = 15;
            tableDadosProdutos.WidthPercentage = 100;

            foreach (var item in pedido.ItensPedido)
            {
                var descricao = new PdfPCell(new Phrase(item.Produto.Descricao, font));
                var tamanhoPeso = new PdfPCell(
                    new Phrase(item.Tamanho != null ? 
                    item.Tamanho.Descricao : 
                    item.Peso?.Descricao ?? "", 
                    font));
                var quantidade = new PdfPCell(new Phrase(item.Quantidade.ToString(), font));
                var valorUnitario = new PdfPCell(new Phrase(item.ValorUnitario.ToString("N", formato), font));
                var total = new PdfPCell(new Phrase(item.ValorTotal.ToString("N", formato), font));

                descricao.BorderWidth = 0;
                tamanhoPeso.BorderWidth = 0;
                quantidade.BorderWidth = 0;
                valorUnitario.BorderWidth = 0;
                total.BorderWidth = 0;

                tableDadosProdutos.AddCell(descricao);
                tableDadosProdutos.AddCell(tamanhoPeso);
                tableDadosProdutos.AddCell(quantidade);
                tableDadosProdutos.AddCell(valorUnitario);
                tableDadosProdutos.AddCell(total);
            }

            tableDadosProdutos.SetWidths(columnWidths);

            var eventHelper = new CustomPageEventHandler();
            writer.PageEvent = eventHelper;

            var tableTotalizacao = new PdfPTable(1);
            tableTotalizacao.DefaultCell.FixedHeight = 15;
            tableTotalizacao.WidthPercentage = 100;
            tableTotalizacao.HorizontalAlignment = Element.ALIGN_RIGHT;

            var cellQtdTotal = new PdfPCell(new Phrase($" Total :  {pedido.ValorTotal.ToString("N", formato)}", fontCabecalho))
            {
                BorderWidth = 0,
                PaddingRight = 70,
                HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            };

            tableTotalizacao.AddCell(cellQtdTotal);

            document.Open();

            document.Add(table);
            document.Add(tableContato);
            document.Add(tableCabecalhoProdutos);
            document.Add(tableDadosProdutos);
            document.Add(tableTotalizacao);

            document.Close();

            var emailPedido = EnvironmentVariable.GetVariable("EMAIL_PEDIDO");
            var message = "Que ótima noticia, mais um pedido!";
            var assunto = "Novo pedido";
            return _emailService.SendEmail(emailPedido, message, assunto, memoryStream.ToArray(), $"pedido-{pedido.Numero}", "application/pdf");
        }
        catch (Exception)
        {
            return false;
        }
    }

}
