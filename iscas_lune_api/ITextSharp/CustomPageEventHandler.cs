using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iscas_lune_api.ITextSharp;

public class CustomPageEventHandler : PdfPageEventHelper
{
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        try
        {
            base.OnEndPage(writer, document);

            var font = FontFactory.GetFont(FontFactory.HELVETICA, 6);

            var footer = new PdfPTable(2);
            footer.DefaultCell.FixedHeight = 20;
            footer.WidthPercentage = 100;
            footer.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

            var cellFooterData = new PdfPCell(new Phrase("Data : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), font));
            cellFooterData.BorderWidth = 0;
            cellFooterData.BorderWidthTop = 1;
            cellFooterData.HorizontalAlignment = Element.ALIGN_LEFT;
            footer.AddCell(cellFooterData);

            var cellFooterPag = new PdfPCell(new Phrase("Pag. " + writer.PageNumber, font));
            cellFooterPag.BorderWidth = 0;
            cellFooterPag.BorderWidthTop = 1;
            cellFooterPag.HorizontalAlignment = Element.ALIGN_RIGHT;
            footer.AddCell(cellFooterPag);

            footer.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
