using Dreamer.Data.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Dreamer.PdfReport
{
    public class rptPurchaseInvoice : PdfFooterPart
    {
        #region Declaration
        PdfWriter _oPdfWriter;
        int _maxColumn = 8;
        Document _document;
        PdfPTable _pdfPTable = new PdfPTable(8);
        PdfPCell _pdfPCell;
        Font _fontStyle;
        MemoryStream _memoryStream = new MemoryStream();
        ProductView _student = new ProductView();

        #endregion



        public byte[] Report(ProductView student)
        {
            _student = student;

            _document = new Document(PageSize.A4, 10f, 10f, 20f, 30f);
            _pdfPTable.WidthPercentage = 100;
            _pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);

            _oPdfWriter = PdfWriter.GetInstance(_document, _memoryStream);
            _oPdfWriter.PageEvent = new PdfFooterPart();

            _document.Open();

            float[] sizes = new float[_maxColumn];
            for (int i = 0; i < _maxColumn; i++)
            {
                if (i == 0) sizes[i] = 50;
                else sizes[i] = 100;
            }
            _pdfPTable.SetWidths(sizes);

            this.ReportHeader();
            this.ReportBody();
            _pdfPTable.HeaderRows = 2;
            _document.Add(_pdfPTable);

            this.OnEndPage(_oPdfWriter, _document);

            _document.Close();
            return _memoryStream.ToArray();
        }
        private void ReportHeader()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 18f, 1);

            _pdfPCell = new PdfPCell(new Phrase("Student Information", _fontStyle));
            _pdfPCell.Colspan = _maxColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();

        }
        private void ReportBody()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 14f, 1);
            var fontStyle = FontFactory.GetFont("Tahoma", 14f, 0);

            #region Student Basic Info (1st Row)
            _pdfPCell = new PdfPCell(new Phrase("Name:", _fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPCell.Border = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_student.ProductName, _fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPCell.Border = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Name:", _fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPCell.Border = 0;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_student.ProductCode, _fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPCell.Border = 0;
            _pdfPTable.AddCell(_pdfPCell);



            #endregion
        }
    }
}
