using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using holidays;

namespace WebApplication4.hr
{
    public partial class deleteteam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                // If querystring value is missing, send the user to ManageUsers.aspx
                string teamID = Request.QueryString["deleteteamid"];
                if (string.IsNullOrEmpty(teamID))
                    Response.Redirect("menageteams.aspx");

                int recordcount;
                int userscount;

                string teamName = Request.QueryString["deleteteamName"];
                if (string.IsNullOrEmpty(teamName))
                    Response.Redirect("menageteams.aspx");

                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT COUNT(Id) From Teams Where Id = @id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = teamID;
                    cmd.CommandType = CommandType.Text;
                    recordcount = (int)cmd.ExecuteScalar();
                    sql = "SELECT COUNT(UserName) From aspnet_Users Where Team = @id";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = teamID;
                    userscount = (int)cmd.ExecuteScalar();
                    cmd.Cancel();
                    conn.Close();
                }

                if (recordcount != 1) { Response.Redirect("menageteams.aspx?nodeleteteamid=" + teamID + "&nodeleteteamName=" + teamName); }
                if (userscount != 0) { Response.Redirect("menageteams.aspx?nodeleteteamid=" + teamID + "&nodeleteteamName=" + teamName + "&userscount=1"); }

                user_name_label.InnerText = "Delete Team: " + teamName + " (id: " + teamID + ") ?";
                label_text2.Text = "Czy na pewno chcesz usunąć team: <b>" + teamName + "</b> (id: " + teamID + ") ?<br />Wszystkie jego dane zostana bezpowrotnie usunięte!";
            }

        }

        protected void anuluj_btn_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string url_text = "menageteams.aspx";
                Response.Redirect(url_text);
            }
        }

        protected void delete_btn_Click(object sender, EventArgs e)
        {
            string teamID = Request.QueryString["deleteteamid"];
            if (string.IsNullOrEmpty(teamID))
                Response.Redirect("menageteams.aspx");

            string teamName = Request.QueryString["deleteteamName"];
            if (string.IsNullOrEmpty(teamName))
                Response.Redirect("menageteams.aspx");

            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Teams Where Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = teamID;
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Cancel();
            }

            List<string> userlist = new List<string>();
            userlist.Add(teamName);
            PolaczenieSQL.addHRhistory(userlist, 0, "Team '" + teamName + "' was removed", Context.User.Identity.Name, "Teams actions");

            string url_text = "menageteams.aspx?deleteteamid=" + teamID + "&deleteteamName=" + teamName;
            Response.Redirect(url_text);
        }

    }


}