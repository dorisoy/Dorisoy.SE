using DCMS.SE.Data.Setting;
using DCMS.SE.Data.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;

namespace DCMS.SE.PdfReport
{
    public class PdfSalesReceive
    {
        #region Declararion
        readonly int _maxColumn = 8;
        Document _document;
        readonly PdfPTable _pdfTable = new (8);
        PdfPCell _pdfCell;
        Font _fontStyle;
        readonly MemoryStream _memoryStream = new ();
   List<PaymentReceiveView> _listReport = new ();
        #endregion

      public byte[] Report(List<PaymentReceiveView> listReport)
        {
            _listReport = listReport;
            _document = new Document(PageSize.A4, 10f, 10f, 20f, 30f);
            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();

            float[] sizes = new float[_maxColumn];
            for(var i = 0; i< _maxColumn; i++)
            {
                if (i == 0) sizes[i] = 50;
                else sizes[i] = 100;
            }
            _pdfTable.SetWidths(sizes);
            this.ReportHeader();
            this.ReportBody();



            _pdfTable.HeaderRows = 2;
            _document.Add(_pdfTable);
            _document.Close();
            return _memoryStream.ToArray();
        }
        private void ReportHeader()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 18f, 1);
            _pdfCell = new(new Phrase("Sales Receive", _fontStyle))
            {
                Colspan = _maxColumn,
                HorizontalAlignment = Element.ALIGN_CENTER,
                ExtraParagraphSpace = 0
            };
            _pdfTable.AddCell(_pdfCell);
            _pdfTable.CompleteRow();
        }
        private void ReportBody()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 9f, 1);
            var fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);

            #region Table Header
            _pdfCell = new(new Phrase("S.No", _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.Gray
            };
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new(new Phrase("Date.", _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.Gray
            };
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("客户名称", _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.Gray
            };
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("VoucherNo", _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.Gray
            };
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Purchase", _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.Gray
            };
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new(new Phrase("VoucherType", _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.Gray
            };
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new(new Phrase("Paid By", _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.Gray
            };
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new(new Phrase("Amount", _fontStyle))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.Gray
            };
            _pdfTable.AddCell(_pdfCell);


            _pdfTable.CompleteRow();

            #endregion



            #region Table Body

            int sno = 1;
           foreach(var ostudent in _listReport)
            {
                _pdfCell = new(new Phrase(sno++.ToString(), _fontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.White
                };
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(ostudent.Date.ToString(), _fontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.White
                };
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new (new Phrase(ostudent.TerminalName, _fontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.White
                };
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new (new Phrase(ostudent.VoucherNo, _fontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.White
                };
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new (new Phrase(ostudent.PurchaseVoucherNo, _fontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.White
                };
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new(new Phrase(ostudent.VoucherTypeName, _fontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.White
                };
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new (new Phrase(ostudent.PaymentType, _fontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.White
                };
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(ostudent.Amount.ToString(), _fontStyle))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    BackgroundColor = BaseColor.White
                };
                _pdfTable.AddCell(_pdfCell);


                _pdfTable.CompleteRow();
            }
            #endregion
        }
    }
}
