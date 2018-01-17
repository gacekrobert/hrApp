using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using holidays;
using System.Drawing;

namespace WebApplication4.menager
{
    public partial class menager_holiday : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["holidayid"] != null)
                {
                    string holidayid = Request.QueryString["holidayid"];
                    holiday new_h = PolaczenieSQL.find_holiday(holidayid);

                    if (new_h.menager != Context.User.Identity.Name && new_h.othermenager != Context.User.Identity.Name) { Response.Redirect("menager_holidays.aspx"); }

                    string user_name_string = PolaczenieSQL.find_user(new_h.userid).ToString();

                    switch (new_h.statusid)
                    {
                        case "1":
                            butt_delete.Visible = false;
                            butt_app_men.Visible = true;
                            butt_app_hr.Visible = false;
                            butt_rej_men.Visible = true;
                            butt_rej_hr.Visible = false;
                            label_status.BackColor = Color.LightYellow;
                            break;
                        case "2":
                            butt_delete.Visible = false;
                            butt_app_men.Visible = false;
                            butt_app_hr.Visible = false;
                            butt_rej_men.Visible = true;
                            butt_rej_hr.Visible = false;
                            label_status.BackColor = Color.LightYellow;
                            break;
                        case "3":
                            butt_delete.Visible = false;
                            butt_app_men.Visible = false;
                            butt_app_hr.Visible = false;
                            butt_rej_men.Visible = true;
                            butt_rej_hr.Visible = false;
                            label_status.BackColor = Color.LightGreen;
                            break;
                        case "4":
                            butt_delete.Visible = false;
                            butt_app_men.Visible = false;
                            butt_app_hr.Visible = false;
                            butt_rej_men.Visible = false;
                            butt_rej_hr.Visible = false;
                            label_status.BackColor = Color.LightPink;
                            break;
                        case "5":
                            butt_delete.Visible = false;
                            butt_app_men.Visible = false;
                            butt_app_hr.Visible = false;
                            butt_rej_men.Visible = false;
                            butt_rej_hr.Visible = false;
                            label_status.BackColor = Color.LightPink;
                            break;
                        case "6":
                            butt_delete.Visible = false;
                            butt_app_men.Visible = false;
                            butt_app_hr.Visible = false;
                            butt_rej_men.Visible = false;
                            butt_rej_hr.Visible = false;
                            label_status.BackColor = Color.DarkSeaGreen;
                            break;
                        case "7":
                            butt_delete.Visible = false;
                            butt_app_men.Visible = false;
                            butt_app_hr.Visible = false;
                            butt_rej_men.Visible = false;
                            butt_rej_hr.Visible = false;
                            label_status.BackColor = Color.LightSlateGray;
                            break;

                    }

                    l_id.Text = new_h.holidayid;
                    l_name.Text = user_name_string;
                    l_od.Text = new_h.date_od.ToShortDateString();
                    l_do.Text = new_h.date_do.ToShortDateString();
                    l_menager.Text = PolaczenieSQL.find_user(new_h.menager).ToString();
                    l_ilość_dni.Text = new_h.holiday_dyas_all.ToString();
                    l_losc_dni_all.Text = new_h.holiday_dyas_ciag.ToString();

                    string[] days_of_holidays = PolaczenieSQL.find_holiday_days_byuser(new_h.userid);
                    Labelpr.Text = days_of_holidays[0];
                    Labelor.Text = days_of_holidays[8];
                    Labelnr.Text = days_of_holidays[9];
                    Labeldd.Text = days_of_holidays[5];
                    Labelnz.Text = days_of_holidays[6];
                    Labelsum.Text = days_of_holidays[10];
                    Labelwyk.Text = days_of_holidays[3];

                    if (Convert.ToInt16(Labelsum.Text) > 0) { suncell.BackColor = Color.LightGreen; }
                    if (Convert.ToInt16(Labelsum.Text) <= 0) { suncell.BackColor = Color.LightPink; }

                    h_details.InnerHtml = "Szczegóły urlopu [id:" + holidayid + "]";
                    h_userdays.InnerHtml = "Dni urlopowe pracownika [" + user_name_string + "]";

                    PolaczenieSQL.fill_holidays_by_user(GridView1, new_h.userid, 10);
                    PolaczenieSQL.fill_holiday_history(GridView2, holidayid);

                    label_status.Text = "STATUS: " + new_h.status;

                    DateTime date1 = new DateTime(new_h.date_od.Year, new_h.date_od.Month, 1);
                    DateTime date0 = date1.AddMonths(-1);
                    DateTime date2 = date1.AddMonths(1);

                    PageMetods.month_calendar_by_holidayid(bigDiv, date0, date2, new_h.userid, true, new_h.holidayid);
                    Session.Contents.RemoveAll();
                    GC.SuppressFinalize(this);
                    foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
                    {
                        HttpContext.Current.Cache.Remove((string)entry.Key);
                    }

                }
                else
                {
                    Response.Redirect("menager_holidays.aspx");
                }
            }

        }

        protected void goBack_Click(object sender, EventArgs e)
        {

            string url_text = "menager_holidays.aspx";

            //if (Session["LastHolidayList"] != null)
            //{
            //    url_text = Session["LastHolidayList"].ToString();
            //}

            Response.Redirect(url_text);

        }

        protected void rejectHR_Click(object sender, EventArgs e)
        {
            holiday h = PolaczenieSQL.find_holiday(this.l_id.Text);
            h.rejectHR(Context.User.Identity.Name);
            string url_text = "menager_holiday.aspx?holidayid=" + this.l_id.Text;
            Response.Redirect(url_text);

        }

        protected void rejectMenager_Click(object sender, EventArgs e)
        {
            holiday h = PolaczenieSQL.find_holiday(this.l_id.Text);
            h.rejectMenager(Context.User.Identity.Name);
            string url_text = "menager_holiday.aspx?holidayid=" + this.l_id.Text;
            Response.Redirect(url_text);

        }

        protected void apprMenager_Click(object sender, EventArgs e)
        {
            holiday h = PolaczenieSQL.find_holiday(this.l_id.Text);
            h.appruveMenager(Context.User.Identity.Name);
            string url_text = "menager_holiday.aspx?holidayid=" + this.l_id.Text;
            Response.Redirect(url_text);

        }

        protected void apprHR_Click(object sender, EventArgs e)
        {
            holiday h = PolaczenieSQL.find_holiday(this.l_id.Text);
            h.appruveHR(Context.User.Identity.Name);
            string url_text = "menager_holiday.aspx?holidayid=" + this.l_id.Text;
            Response.Redirect(url_text);

        }

        protected void delete_Click(object sender, EventArgs e)
        {
            holiday h = PolaczenieSQL.find_holiday(this.l_id.Text);
            h.deleted(Context.User.Identity.Name);
            string url_text = "menager_holiday.aspx?holidayid=" + this.l_id.Text;
            Response.Redirect(url_text);

        }
    }
}