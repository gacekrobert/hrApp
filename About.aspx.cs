using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace WebApplication4
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int year = DateTime.Today.Year;
            string[] miesiace = new string[] { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"};
            for(int i=1;i<16;i++)
            {
                int days_count;
                int im = i;
                if (i == 13) { year = year + 1;}
                if (i > 12) { im = i - 12; }
                days_count = DateTime.DaysInMonth(year, im);
                Table tb = new Table();
                TableRow tr = new TableRow();
                tb.Rows.Add(tr);
                for (int ii = 1; ii <= days_count; ii++)
                {
                    TableCell tCell = new TableCell();
                    tr.Cells.Add(tCell);
                    tCell.Text = ii.ToString();
                    tCell.CssClass = "cellnormal";
                    DateTime thisday = new DateTime(year,im,ii);
                    if (thisday.DayOfWeek == DayOfWeek.Sunday || thisday.DayOfWeek == DayOfWeek.Saturday)
                    {
                        tCell.BackColor = Color.FromArgb(255, 180, 180);
                    }   
                }
                string str_id = "m" + i.ToString();
                string str_id2 = "l" + i.ToString();
                HtmlControl divm = (HtmlControl) bigDiv.FindControl(str_id);
                HtmlControl divl = (HtmlControl)bigDiv.FindControl(str_id2);
                divl.Controls.Add(new LiteralControl(miesiace[im-1] + " " + year.ToString()));
                divm.Controls.Add(tb);
            }
        }
    }
}
