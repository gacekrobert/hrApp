using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using holidays;

namespace WebApplication4.hr
{
    public partial class printpdf : System.Web.UI.Page
    {
        void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["pathid"] != null)
            {
            // Create new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "PDFsharp Clock Demo";
            document.Info.Author = "Stefan Lange";
            document.Info.Subject = "Server time: ";

            // Create new page
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 12, XFontStyle.Regular);

            double x = 100, y = 100;
            double ls = font.GetHeight(gfx);

            string[] strinData = PolaczenieSQL.print_history_pdf(Request.QueryString["pathid"]);

            // Draw the text

            gfx.DrawString("Action: " + strinData[6], font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString("Action Id: " + strinData[0], font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString("Employe: " + strinData[1], font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString("HR employee: " + strinData[2], font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString("Days count: " + strinData[3], font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString("Reason: " + strinData[4], font, XBrushes.Black, x, y);
            y += ls;
            gfx.DrawString("Date: " + strinData[5], font, XBrushes.Black, x, y);

            
            // Send PDF to browser
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", stream.Length.ToString());
            Response.BinaryWrite(stream.ToArray());
            Response.Flush();
            stream.Close();
            Response.End();
            }

        }
    }
}