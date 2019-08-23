using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace BLL
{
    public abstract class PDFExportador
    {
        protected iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        public void ExportarPDFARuta(String title, List<String> columnHeaderList, List<Object> objectLists, String rutaDestino)
        {
            Document doc = new Document(PageSize.A4);
            //rutaDestino = rutaDestino.Replace(".pdf", "(" + DateTime.Now.ToString("yyyy-MM-dd hh mm ss") + ").pdf");
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(rutaDestino, FileMode.Create));
            writer.PageEvent = new PageEventHandler(columnHeaderList);
            doc.AddTitle("Reportes Editorial");
            doc.AddCreator("Editorial");

            doc.Open();

            doc.Add(new Paragraph(title));
            doc.Add(Chunk.NEWLINE);

            PdfPTable tblPrueba = new PdfPTable(columnHeaderList.Count);
            tblPrueba.WidthPercentage = 100;

            columnHeaderList.ForEach(cabecara =>
            {
                PdfPCell clNombre = new PdfPCell(new Phrase(cabecara, _standardFont));
                clNombre.BorderWidth = 0;
                clNombre.BorderWidthBottom = 0.75f;
                tblPrueba.AddCell(clNombre);
            });

            CompletarDocumento(tblPrueba, objectLists);
            doc.Add(tblPrueba);

            doc.Close();
            writer.Close();
        }




        protected abstract void CompletarDocumento(PdfPTable tabla, List<Object> objectLists);
    }
    public class PageEventHandler : PdfPageEventHelper
    {
        protected iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
        private List<string> columnHeaderList;
        //private PdfContentByte cb;
        private List<PdfTemplate> templates;

        public PageEventHandler(List<String> columnHeaderList)
        {
            this.columnHeaderList = columnHeaderList;
            this.templates = new List<PdfTemplate>();
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            if (writer.PageNumber > 1)
            {
                document.Add(Chunk.NEWLINE);

                PdfPTable tblPrueba = new PdfPTable(columnHeaderList.Count);
                tblPrueba.WidthPercentage = 100;

                columnHeaderList.ForEach(cabecara =>
                {
                    PdfPCell clNombre = new PdfPCell(new Phrase(cabecara, _standardFont));
                    clNombre.BorderWidth = 0;
                    clNombre.BorderWidthBottom = 0.75f;
                    tblPrueba.AddCell(clNombre);
                });
                document.Add(tblPrueba);
            }

            base.OnStartPage(writer, document);

            var cb = writer.DirectContentUnder;
            PdfTemplate templateM = cb.CreateTemplate(50, 50);
            templates.Add(templateM);

            int pageN = writer.CurrentPageNumber;
            String pageText = "Pagina " + pageN.ToString() + " de ";
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            float len = bf.GetWidthPoint(pageText, 10);
            cb.BeginText();
            cb.SetFontAndSize(bf, 10);
            cb.SetTextMatrix(document.LeftMargin, document.PageSize.GetTop(document.TopMargin));
            cb.ShowText(pageText);
            cb.EndText();
            cb.AddTemplate(templateM, document.LeftMargin + len, document.PageSize.GetTop(document.TopMargin));
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            foreach (PdfTemplate item in templates)
            {
                item.BeginText();
                item.SetFontAndSize(bf, 10);
                item.SetTextMatrix(0, 0);
                item.ShowText("" + (writer.PageNumber));
                item.EndText();
            }

        }

        /* public override void OnStartPage(PdfWriter writer, Document document)
         {
             if (writer.PageNumber > 1)
             {
                 document.Add(Chunk.NEWLINE);

                 PdfPTable tblPrueba = new PdfPTable(columnHeaderList.Count);
                 tblPrueba.WidthPercentage = 100;

                 columnHeaderList.ForEach(cabecara =>
                 {
                     PdfPCell clNombre = new PdfPCell(new Phrase(cabecara, _standardFont));
                     clNombre.BorderWidth = 0;
                     clNombre.BorderWidthBottom = 0.75f;
                     tblPrueba.AddCell(clNombre);
                 });
                 document.Add(tblPrueba);
             }
             base.OnStartPage(writer, document);
         }*/
    }
}
