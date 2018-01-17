using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using holidays;

namespace WebApplication4.menager
{
    public partial class menager_holidays : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string text_url = Request.RawUrl;
                //Session["LastHolidayList"] = text_url;
                //if (Request.QueryString["teamid"] != null)
                //{
                    string menager_id = Context.User.Identity.Name;
                    string teamid = Request.QueryString["teamid"];
                    //PolaczenieSQL.list_of_users_names(PersonDropDown);
                    //PolaczenieSQL.list_of_teams_names(TeamDropDown, teamid);
                    PolaczenieSQL.fill_holidays_by_menager_status(GridViewAppMen, 1, menager_id);
                    PolaczenieSQL.fill_holidays_by_menager_status(GridViewAppHR, 2, menager_id);
                    PolaczenieSQL.fill_holidays_by_menager_status(GridViewZat, 3, menager_id);
                    PolaczenieSQL.fill_holidays_by_menager_status(GridViewRejec, 4, menager_id);
                    PolaczenieSQL.fill_holidays_by_menager_status(GridViewOK, 6, menager_id);
                    PolaczenieSQL.fill_holidays_by_menager_status(GridViewDelete, 7, menager_id);
                //}
                //else if (Request.QueryString["userid"] != null)
                //{
                //    string userid = Request.QueryString["userid"];
                //    PolaczenieSQL.list_of_users_names(PersonDropDown, userid);
                //    PolaczenieSQL.list_of_teams_names(TeamDropDown);
                //    PolaczenieSQL.fill_holidays_by_status(GridViewAppMen, 1, user_id: userid);
                //    PolaczenieSQL.fill_holidays_by_status(GridViewAppHR, 2, user_id: userid);
                //    PolaczenieSQL.fill_holidays_by_status(GridViewZat, 3, user_id: userid);
                //    PolaczenieSQL.fill_holidays_by_status(GridViewRejec, 4, user_id: userid);
                //    PolaczenieSQL.fill_holidays_by_status(GridViewOK, 6, user_id: userid);
                //    PolaczenieSQL.fill_holidays_by_status(GridViewDelete, 7, user_id: userid);
                //}
                //else
                //{
                //    PolaczenieSQL.list_of_users_names(PersonDropDown);
                //    PolaczenieSQL.list_of_teams_names(TeamDropDown);
                //    PolaczenieSQL.fill_holidays_by_status(GridViewAppMen, 1);
                //    PolaczenieSQL.fill_holidays_by_status(GridViewAppHR, 2);
                //    PolaczenieSQL.fill_holidays_by_status(GridViewZat, 3);
                //    PolaczenieSQL.fill_holidays_by_status(GridViewRejec, 4);
                //    PolaczenieSQL.fill_holidays_by_status(GridViewOK, 6);
                //    PolaczenieSQL.fill_holidays_by_status(GridViewDelete, 7);
                //}

                h_AppMen.InnerHtml = "Do zatwierdzenia przez menagera (" + GridViewAppMen.Rows.Count + ")";
                h_AppHR.InnerHtml = "Do zatwierdzenia przez dział HR (" + GridViewAppHR.Rows.Count + ")";
                h_Zat.InnerHtml = "Urlopy zatwierdzone (" + GridViewZat.Rows.Count + ")";
                h_OK.InnerHtml = "Urlopy wykorzystane (" + GridViewOK.Rows.Count + ")";
                h_Rejec.InnerHtml = "Urlopy odrzucone (" + GridViewRejec.Rows.Count + ")";
                h_Delete.InnerHtml = "Urlopy usunięte (" + GridViewDelete.Rows.Count + ")";

                DateTime date0 = new DateTime(DateTime.Today.Year, 1, 1);
                DateTime date2 = new DateTime(DateTime.Today.Year + 1, 3, 1);

                PageMetods.month_calendar_by_holidayid(bigDiv, date0, date2, Context.User.Identity.Name,true,"0",true);

                Session.Contents.RemoveAll();
                GC.SuppressFinalize(this);
                foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
                {
                    HttpContext.Current.Cache.Remove((string)entry.Key);
                }

            }
            else
            {
                //string team1 = Request.Form[TeamDropDown.UniqueID];
                //TeamDropDown.Text = team1;
                //string user1 = Request.Form[PersonDropDown.UniqueID];
                //PersonDropDown.Text = user1;
            }
        }
    }
}