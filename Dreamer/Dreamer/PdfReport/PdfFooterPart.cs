using iTextSharp.text.pdf;
using iTextSharp.text;
using System;

namespace Dreamer.PdfReport
{
    public class PdfFooterPart : PdfPageEventHelper
    {
        private readonly Font _pageNumberFont = new Font(Font.NORMAL, 8f, Font.NORMAL, BaseColor.Black);
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            this.AddPageNumber(writer, document);
        }

        public void AddPageNumber(PdfWriter writer, Document document)
        {
            var numberTable = new PdfPTable(1);
            string text = "Page No:" + writer.PageNumber.ToString("00"),
            text1 = "Generated Time : " + DateTime.Now.ToString("dd-MMM-YYYY HH:mm:ss");

            var pdfCell = new PdfPCell(new Phrase(text, _pageNumberFont));
            pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfCell.Border = 0;
            pdfCell.BackgroundColor = BaseColor.White;
            numberTable.AddCell(pdfCell);

            pdfCell = new PdfPCell(new Phrase(text, _pageNumberFont));
            pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfCell.Border = 0;
            pdfCell.BackgroundColor = BaseColor.White;
            numberTable.AddCell(pdfCell);

            numberTable.TotalWidth = 450;
            numberTable.WriteSelectedRows(0, -1, document.Left + 80, document.Bottom + 10, writer.DirectContent);
        }
}
}
