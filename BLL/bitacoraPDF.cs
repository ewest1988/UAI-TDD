using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class BitacoraPDF : PDFExportador
    {
        protected override void CompletarDocumento(PdfPTable tabla, List<Object> objectLists)
        {
            objectLists.ForEach(obj =>
            {
                DataGridViewRow row = (DataGridViewRow)obj;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    PdfPCell rowCell = new PdfPCell(new Phrase(cell.Value?.ToString(), _standardFont));
                    rowCell.BorderWidth = 0;
                    tabla.AddCell(rowCell);
                }
            });
        }
    }
}
