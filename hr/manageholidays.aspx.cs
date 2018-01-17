using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using holidays;
using System.Data;

namespace WebApplication4.hr
{
    public partial class manageholidays : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
          if (!Page.IsPostBack)
            {
                string text_url = Request.RawUrl;
                //Session["LastHolidayList"] = text_url;
                if (Request.QueryString["teamid"] != null)
                {
                    string teamid = Request.QueryString["teamid"];
                    PolaczenieSQL.list_of_users_names(PersonDropDown);
                    PolaczenieSQL.list_of_teams_names(TeamDropDown, teamid);
                    PolaczenieSQL.fill_holidays_by_status(GridViewAppMen, 1, team_id: teamid);
                    PolaczenieSQL.fill_holidays_by_status(GridViewAppHR, 2, team_id: teamid);
                    PolaczenieSQL.fill_holidays_by_status(GridViewZat, 3, team_id: teamid);
                    PolaczenieSQL.fill_holidays_by_status(GridViewRejec, 4, team_id: teamid);
                    PolaczenieSQL.fill_holidays_by_status(GridViewOK, 6, team_id: teamid);
                    PolaczenieSQL.fill_holidays_by_status(GridViewDelete, 7, team_id: teamid);
                }
                else if (Request.QueryString["userid"] != null) 
                {
                    string userid = Request.QueryString["userid"];
                    PolaczenieSQL.list_of_users_names(PersonDropDown, userid);
                    PolaczenieSQL.list_of_teams_names(TeamDropDown);
                    PolaczenieSQL.fill_holidays_by_status(GridViewAppMen, 1, user_id: userid);
                    PolaczenieSQL.fill_holidays_by_status(GridViewAppHR, 2, user_id: userid);
                    PolaczenieSQL.fill_holidays_by_status(GridViewZat, 3, user_id: userid);
                    PolaczenieSQL.fill_holidays_by_status(GridViewRejec, 4, user_id: userid);
                    PolaczenieSQL.fill_holidays_by_status(GridViewOK, 6, user_id: userid);
                    PolaczenieSQL.fill_holidays_by_status(GridViewDelete, 7, user_id: userid);
                }else
                {
                    PolaczenieSQL.list_of_users_names(PersonDropDown);
                    PolaczenieSQL.list_of_teams_names(TeamDropDown);
                    PolaczenieSQL.fill_holidays_by_status(GridViewAppMen, 1);
                    PolaczenieSQL.fill_holidays_by_status(GridViewAppHR, 2);
                    PolaczenieSQL.fill_holidays_by_status(GridViewZat, 3);
                    PolaczenieSQL.fill_holidays_by_status(GridViewRejec, 4);
                    PolaczenieSQL.fill_holidays_by_status(GridViewOK, 6);
                    PolaczenieSQL.fill_holidays_by_status(GridViewDelete, 7);
                }

                h_AppMen.InnerHtml = "Do zatwierdzenia przez menagera (" + GridViewAppMen.Rows.Count + ")";
                h_AppHR.InnerHtml = "Do zatwierdzenia przez dział HR (" + GridViewAppHR.Rows.Count + ")";
                h_Zat.InnerHtml = "Urlopy zatwierdzone (" + GridViewZat.Rows.Count + ")";
                h_OK.InnerHtml = "Urlopy wykorzystane (" + GridViewOK.Rows.Count + ")";
                h_Rejec.InnerHtml = "Urlopy odrzucone (" + GridViewRejec.Rows.Count + ")";
                h_Delete.InnerHtml = "Urlopy usunięte (" + GridViewDelete.Rows.Count + ")";

                Session.Contents.RemoveAll();
                GC.SuppressFinalize(this);
                foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
                {
                    HttpContext.Current.Cache.Remove((string)entry.Key);
                }

            }
                else
            {
                string team1 = Request.Form[TeamDropDown.UniqueID];
                TeamDropDown.Text = team1;
                string user1 = Request.Form[PersonDropDown.UniqueID];
                PersonDropDown.Text = user1;
            }

    }

        protected void TeamDropDown_Change(object sender, EventArgs e) 
        {
            string taemid = TeamDropDown.SelectedValue;
            string url_text;
            if (String.Equals(taemid, "-1"))
            {
                url_text = "manageholidays.aspx"; 
            }
            else
            {
                url_text = "manageholidays.aspx" + "?teamid=" + taemid;
            }
            Response.Redirect(url_text);
        }

        protected void PersonDropDown_Change(object sender, EventArgs e)
        {
            string userid = PersonDropDown.SelectedValue;
            string url_text;
            if (String.Equals(userid, "-1"))
            {
                url_text = "manageholidays.aspx";
            }
            else
            {
                url_text = "manageholidays.aspx" + "?userid=" + userid;
            }
            Response.Redirect(url_text);
        }

    }
}