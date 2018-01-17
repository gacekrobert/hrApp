using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

using holidays;
using System.Drawing;

namespace WebApplication4.all
{
    public partial class myholidays : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                string[] days_of_holidays = holidays.PolaczenieSQL.find_holiday_days_byuser(Context.User.Identity.Name);
                Labelpr.Text = days_of_holidays[0];
                Labelor.Text = days_of_holidays[8];
                Labelnr.Text = days_of_holidays[9];
                Labeldd.Text = days_of_holidays[5];
                Labelnz.Text = days_of_holidays[6];
                Labelsum.Text = days_of_holidays[10];
                Labelwyk.Text = days_of_holidays[3];

                if (Convert.ToInt16(Labelsum.Text) > 0) { suncell.BackColor = Color.LightGreen; }
                if (Convert.ToInt16(Labelsum.Text) <= 0) { suncell.BackColor = Color.LightPink; }
                
                List<string> holiday_list = holidays.PolaczenieSQL.find_holiday_byuser(Context.User.Identity.Name);
                
                DataTable td = new DataTable();
                td.Columns.Add("From");
                td.Columns.Add("To");
                td.Columns.Add("Rodzaj");
                td.Columns.Add("ManagerId");
                td.Columns.Add("Status");
                td.Columns.Add("Id");
                td.Columns.Add("Ilosc_dni");
                DataRow dr = null;

                DateTime myTime;

                if (holiday_list != null)
                {
                    foreach (string holidayID in holiday_list)
                    {
                        holidays.holiday h = holidays.PolaczenieSQL.find_holiday(holidayID);

                        dr = td.NewRow();
                        if (h.date_od != null)
                        {
                            myTime = DateTime.Parse(h.date_od.ToString());
                            dr[0] = myTime.ToString("yyyy-MM-dd");
                            if (h.statusid == "3" && (myTime <= DateTime.Today)) 
                            {
                                h.wykorzystany();
                                h.status = "Wykonany";
                            }
                        }
                        if (h.date_od != null)
                        {
                            myTime = DateTime.Parse(h.date_do.ToString());
                            dr[1] = myTime.ToString("yyyy-MM-dd");
                        }
                        dr[2] = h.rodzaj.ToString();
                        holidays.user m = holidays.PolaczenieSQL.find_user(h.menager.ToString());
                        if (m != null)
                        {
                            dr[3] = m.ToString();
                        }
                        else 
                        {
                            dr[3] = "User deleted";
                        }
                        
                        dr[4] = h.status.ToString();
                        dr[5] = h.holidayid.ToString();
                        dr[6] = h.holiday_dyas_all.ToString();
                        td.Rows.Add(dr);
                    }
                }

                GridView1.DataSource = td;
                GridView1.DataBind();
                holidays.PageMetods.holiday_status_color(GridView1);                
                DropDownMenager.DataBind();

                //if (Request.QueryString["teamadd"] != null)
                //{
                //    string teamadd = Request.QueryString["teamadd"];
                //    info_label.Text = "Team <b> " + teamadd + "</b> został dodant z bazy danych";
                //    info_label.CssClass = "goodNotification";
                //    info_label.Visible = true;
                //}
                //else { info_label.Text = ""; }

                //if (Request.QueryString["nodeleteteamid"] != null && Request.QueryString["nodeleteteamName"] != null && Request.QueryString["userscount"] == null)
                //{
                //    string noteamdeletename = Request.QueryString["nodeleteteamName"];
                //    string noteamdeleteid = Request.QueryString["nodeleteteamid"];
                //    info_label.Text = "Nie można usunąc tamu <b>id: " + noteamdeleteid + "</b>. Taki team nie istnieje w bazie danych";
                //    info_label.CssClass = "failureNotification";
                //    info_label.Visible = true;
                //}

                //if (Request.QueryString["deleteteamid"] != null && Request.QueryString["deleteteamName"] != null)
                //{
                //    string teamdeleteid = Request.QueryString["deleteteamid"];
                //    string teamdeletename = Request.QueryString["deleteteamName"];
                //    info_label.Text = "Team <b> " + teamdeletename + "</b> (id: " + teamdeleteid + ") został usunięty z bazy danych";
                //    info_label.CssClass = "failureNotification";
                //    info_label.Visible = true;
                //}

                //if (String.Equals(Request.QueryString["userscount"], "1"))
                //{
                //    string teamdeleteid = Request.QueryString["nodeleteteamid"];
                //    string teamdeletename = Request.QueryString["nodeleteteamName"];
                //    info_label.Text = "Team <b> " + teamdeletename + "</b> (id: " + teamdeleteid + ") nie może zostac usunięty, gdyż są do niego wciąż podłączeni pracownicy<br />By usunąś team odłącz od niego wszystkich przcowników. <a href='/hr/manageteam.aspx?teamid=" + teamdeleteid + "'>Zarządzaj Teamem " + teamdeletename + "</a>";
                //    info_label.CssClass = "failureNotification";
                //    info_label.Visible = true;
                //}

            }
            else
            {
                string menager1 = Request.Form[DropDownMenager.UniqueID];
                DropDownMenager.Text = menager1;
                string rodzaj1 = Request.Form[DropDownHoliday.UniqueID];
                DropDownHoliday.Text = rodzaj1;
                string date1 = Request.Form[OdTextBox.UniqueID];
                OdTextBox.Text = date1;
                string date3 = Request.Form[DoTextBox.UniqueID];
                DoTextBox.Text = date3;
                info_label.Text = "";
            }

            DateTime date0 = new DateTime(DateTime.Today.Year, 1, 1);
            DateTime date2 = new DateTime(DateTime.Today.Year + 1, 3, 1);

            PageMetods.month_calendar_by_holidayid(bigDiv, date0, date2, Context.User.Identity.Name);
            Session.Contents.RemoveAll();
            GC.SuppressFinalize(this);
            foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
            {
                HttpContext.Current.Cache.Remove((string)entry.Key);
            }
            
        }

        protected void addButton_Click(object sender, EventArgs e)
        {

            //Dropdown dla rodzaju urlopów
            holidays.PolaczenieSQL.list_of_holiday_names(DropDownHoliday);

            //Dropdown dla menagerów
            holidays.PolaczenieSQL.list_of_menagers_names(DropDownMenager);

            string menager_id = holidays.PolaczenieSQL.find_menager_by_user(Context.User.Identity.Name);

            DropDownMenager.SelectedValue = menager_id;

            hiddendiv.Attributes["style"] = "float:left;width:120px;";
            hiddendiv2.Attributes["style"] = "float:left;width:120px;";
            OdLab.Visible = true;
            OdTextBox.Visible = true;
            imgCal.Visible = true;
            DoLab.Visible = true;
            DoTextBox.Visible = true;
            imgCal2.Visible = true;
            DropDownMenagerLab.Visible = true;
            DropDownMenager.Visible = true;
            DropDownHoliday.Visible = true;
            DropDownHolidayLab.Visible = true;
            ZapiszButton.Visible = true;
            AnulujButton.Visible = true;
            DropDownMenager.DataBind();

        }

        protected void AnulujButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("myholidays.aspx");
        }

        protected void ZapiszButton_Click(object sender, EventArgs e)
        {

            //Page.Validate();
            if (Page.IsValid)
            {
                holiday h = new holiday(OdTextBox.Text, DoTextBox.Text, DropDownMenager.SelectedValue, "1","1", DropDownHoliday.SelectedValue, Context.User.Identity.Name);
                user u = PolaczenieSQL.find_user(Context.User.Identity.Name,false,true);
                PolaczenieSQL.insertholiday(h,u);
                string url_text = "myholidays.aspx";
                Response.Redirect(url_text);
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

            }else
            args.IsValid = true;
        }


    }
}