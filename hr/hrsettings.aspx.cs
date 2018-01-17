using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using holidays;
using System.Drawing;
using System.Globalization;

namespace WebApplication4.hr
{
    public partial class hrsettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
            int todayy = DateTime.Today.Year;
            LabelYear.Text = "Holidays " + todayy.ToString();
            List<int> years = new List<int>(new int[] { todayy - 1, todayy, todayy + 1 });
            RepeaterYears.DataSource = years;
            RepeaterYears.DataBind();
            DataTable dt = PolaczenieSQL.find_swieta(todayy);
            GridViewSwieta.Columns[0].Visible = true;
            GridViewSwieta.Columns[1].Visible = true;
            GridViewSwieta.Columns[2].Visible = true;
            GridViewSwieta.Columns[3].Visible = true;
            GridViewSwieta.DataSource = dt;
            GridViewSwieta.DataBind();
            GridViewSwieta.Columns[3].Visible = false;

            LabelOperation.Text = "PERSONAL OPERATIONS";

            string to = DateTime.Today.ToShortDateString();
            string from = DateTime.Today.AddDays(-30).ToShortDateString();

            OdTextBox.Text = from;
            DoTextBox.Text = to;

            DataTable dt2 = PolaczenieSQL.find_HistoryHR(from, to, "personal");
            GridViewHistoryHR.DataSource = dt2;
            GridViewHistoryHR.DataBind();


            foreach (GridViewRow row in GridViewHistoryHR.Rows)
            {
                row.Height = 40;
            }

            GridViewHistoryHR.Columns[0].Visible = true;
            GridViewHistoryHR.Columns[1].Visible = true;
            GridViewHistoryHR.Columns[2].Visible = true;
            GridViewHistoryHR.Columns[3].Visible = true;
            GridViewHistoryHR.Columns[4].Visible = true;
            GridViewHistoryHR.Columns[5].Visible = true;
            GridViewHistoryHR.Columns[6].Visible = true;
            GridViewHistoryHR.Columns[7].Visible = true;

            string calculation_date = PolaczenieSQL.calculationdate();
            DateTime myTime1 = DateTime.Today;
            DateTime myTime2 = DateTime.Today.AddDays(-1);
            if (calculation_date != string.Empty)
            {
                myTime2 = DateTime.Parse(calculation_date);
                //this.date_od = DateTime.ParseExact(odtext, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            TimeSpan span = myTime1 - myTime2;
            int totalnumber = Convert.ToInt16(span.TotalDays);
            string calculation_string = "";

            if (totalnumber < 0)
            {
                calculation_string = "No action occure in past";
                LabelLastCalculation.Text = calculation_string;
                LabelLastCalculation.ForeColor = Color.Gray;
            }
            else if (totalnumber >= 0 && totalnumber < 365)
            {
                calculation_string = "Last action: " + myTime2.ToShortDateString() + "; " + totalnumber + " days ago";
                LabelLastCalculation.Text = calculation_string;
                LabelLastCalculation.ForeColor = Color.DarkGreen;
            }
            else 
            {
                calculation_string = "Last action: " + myTime2.ToShortDateString() + "; " + totalnumber + " days ago";
                LabelLastCalculation.Text = calculation_string;
                LabelLastCalculation.ForeColor = Color.Crimson;
            }
           

            }
            else
            {
                string menager1 = Request.Form[DateTextBox.UniqueID];
                DateTextBox.Text = menager1;
                string date1 = Request.Form[NameTextBox.UniqueID];
                NameTextBox.Text = date1;
                string dateod = Request.Form[OdTextBox.UniqueID];
                OdTextBox.Text = dateod;
                string datedo = Request.Form[DoTextBox.UniqueID];
                DoTextBox.Text = datedo;
            }

            if (Request.QueryString["holidaydeleted"] != null)
            {
                string usersdelete = Request.QueryString["holidaydeleted"];
                info_label.Text = "Święto z dnia " + usersdelete + " zostało usunięte z listy";
                info_label.Visible = true;
            }
        }

        public void GridViewSwieta_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "usun")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewSwieta.Rows[index];
                string idvalue = row.Cells[3].Text as string;
                string name_sw = row.Cells[1].Text as string;
                PolaczenieSQL.usunswieto(idvalue);
                string url_text = "hrsettings.aspx?holidaydeleted=" + name_sw;
                Response.Redirect(url_text);
            }
        } 

        protected void DodajButton_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                PolaczenieSQL.insertswieto(DateTextBox.Text, NameTextBox.Text);
                string url_text = "hrsettings.aspx";
                Response.Redirect(url_text);
            }
        }

        protected void NowyRokButton_Click(object sender, EventArgs e)
        {

                PolaczenieSQL.updateNewyear();

                List<string> userlist = new List<string>();
                userlist.Add("All employee");
                PolaczenieSQL.addHRhistory(userlist, 0, "New Year calculations", Context.User.Identity.Name, "New Year calculations");
                
            string url_text = "hrsettings.aspx";
                Response.Redirect(url_text);
        }

        protected void RepeaterYears_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
           int year = Convert.ToInt16(e.CommandArgument);
           LabelYear.Text = "Holidays " + year.ToString();
           DataTable dt = PolaczenieSQL.find_swieta(year);
           GridViewSwieta.Columns[0].Visible = true;
           GridViewSwieta.Columns[1].Visible = true;
           GridViewSwieta.Columns[2].Visible = true;
           GridViewSwieta.Columns[3].Visible = true;
           GridViewSwieta.DataSource = dt;
           GridViewSwieta.DataBind();
           GridViewSwieta.Columns[3].Visible = false;
        }

        protected void hrhistory_Click(object sender, CommandEventArgs e)
        {
            //Page.Validate();
            if (Page.IsValid)
            {
                string from = OdTextBox.Text;
                string to = DoTextBox.Text;

                DataTable dt2 = PolaczenieSQL.find_HistoryHR(from, to, e.CommandArgument.ToString());
                GridViewHistoryHR.DataSource = dt2;
                GridViewHistoryHR.DataBind();

                if (e.CommandArgument.ToString() == "team" || e.CommandArgument.ToString() == "newyear")
                {
                    GridViewHistoryHR.Columns[0].Visible = true;
                    GridViewHistoryHR.Columns[1].Visible = true;
                    GridViewHistoryHR.Columns[2].Visible = true;
                    GridViewHistoryHR.Columns[3].Visible = true;
                    GridViewHistoryHR.Columns[4].Visible = true;
                    GridViewHistoryHR.Columns[5].Visible = true;
                    GridViewHistoryHR.Columns[6].Visible = true;
                    GridViewHistoryHR.Columns[7].Visible = false;
                }
                else 
                {
                    GridViewHistoryHR.Columns[0].Visible = true;
                    GridViewHistoryHR.Columns[1].Visible = true;
                    GridViewHistoryHR.Columns[2].Visible = true;
                    GridViewHistoryHR.Columns[3].Visible = true;
                    GridViewHistoryHR.Columns[4].Visible = true;
                    GridViewHistoryHR.Columns[5].Visible = true;
                    GridViewHistoryHR.Columns[6].Visible = true;
                    GridViewHistoryHR.Columns[7].Visible = true;
                }

                foreach (GridViewRow row in GridViewHistoryHR.Rows)
                {
                    row.Height = 40;
                }

                switch (e.CommandArgument.ToString())
                {
                    case "team":
                        LabelOperation.Text = "TEAMS OPERATIONS";
                        break;
                    case "functions":
                        LabelOperation.Text = "PERSON FUNCTION OPERATIONS";
                        break;
                    case "newyear":
                        LabelOperation.Text = "NEW YEAR OPERATIONS";
                        break;
                    default:
                        LabelOperation.Text = "PERSONAL OPERATIONS";
                        break;
                }
            }

        }

        protected void DateValidate(object source, ServerValidateEventArgs args)
        {
            if (!String.IsNullOrEmpty(OdTextBox.Text))
            {
                DateTime Date_od = new DateTime();
                DateTime Date_do = new DateTime();

                if (OdTextBox.Text != string.Empty)
                {
                    Date_od = DateTime.ParseExact(OdTextBox.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                if (args.Value != string.Empty)
                {
                    Date_do = DateTime.ParseExact(args.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }

                if (Date_do.Subtract(Date_od).TotalDays < 0)
                {
                    args.IsValid = false;
                }
                else args.IsValid = true;

            }
            else
                args.IsValid = true;
        }

        public void GridViewHistoryHR_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "print_pdf")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewHistoryHR.Rows[index];
                string idvalue = row.Cells[0].Text as string;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('printpdf.aspx?pathid=" + idvalue + "');", true);
            }
        } 

        
    }
}