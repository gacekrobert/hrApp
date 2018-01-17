using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using holidays;

namespace WebApplication4.hr
{
    public partial class menageteams : System.Web.UI.Page
    {

        string[] menagers;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                //var functions = teamName;

                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
                conn.Open();
                string sql = "SELECT Id, Menager, Name, Imie, Nazwisko FROM Teams LEFT JOIN aspnet_Users ON aspnet_Users.UserName=Teams.Menager";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader sdr;
                DataTable td = new DataTable();
                td.Columns.Add("TeamName");
                td.Columns.Add("Menager");
                td.Columns.Add("Count");
                td.Columns.Add("Id");
                DataRow dr = null;
                
                sdr = cmd.ExecuteReader();

                string teamid;

                while (sdr.Read())
                {
                    dr = td.NewRow();
                    dr[0] = sdr["Name"].ToString();
                    dr[1] = sdr["Imie"].ToString() + " " + sdr["Nazwisko"].ToString();
                    teamid = sdr["Id"].ToString();
                    dr[3] = teamid;
                    //dr[2] = recordcount2;
                    td.Rows.Add(dr);
                }
                cmd.Cancel();
                sdr.Close();

                int recordcount2;

                string sql2 = "SELECT COUNT(UserName) From aspnet_Users Where Team = @id";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.Parameters.Add("@id", SqlDbType.VarChar, 50);
                cmd2.CommandType = CommandType.Text;

                for (int i = 0; i<td.Rows.Count; i++) 
                {
                    cmd2.Parameters["@id"].Value = td.Rows[i][3];
                    recordcount2 = (int)cmd2.ExecuteScalar();
                    td.Rows[i][2] = recordcount2;
                }
                
                DropDownMenager.Items.Clear();
                ListItem firstitem = new ListItem();
                firstitem.Value = "-1";
                firstitem.Text = "Wybierz menagera";
                DropDownMenager.Items.Add(firstitem);

                string menagername = "";

                menagers = Roles.GetUsersInRole("menager");
                string sqlquery = "SELECT UserName, Imie, Nazwisko, Name FROM aspnet_Users LEFT JOIN Teams ON aspnet_Users.Team=Teams.Id Where UserName = @user";

                SqlCommand command = new SqlCommand(sqlquery, conn);
                SqlDataReader sdr2;
                //ListItem nextitem;
                command.Parameters.Add("@user", SqlDbType.VarChar, 50);
                foreach (string menager in menagers)
                {
                    ListItem nextitem = new ListItem();
                    command.Parameters["@user"].Value = menager;
                    sdr2 = command.ExecuteReader();

                    while (sdr2.Read())
                    {
                        menagername = sdr2["Imie"].ToString() + " " + sdr2["Nazwisko"].ToString() + " (" + sdr2["Name"].ToString() + ")";
                    }
                    nextitem.Value = menager;
                    nextitem.Text = menagername;
                    DropDownMenager.Items.Add(nextitem);
                    sdr2.Close();
                    nextitem = null;
                }

                conn.Close();

                if (Request.QueryString["teamadd"] != null)
                {
                    string teamadd = Request.QueryString["teamadd"];
                    info_label.Text = "Team <b> " + teamadd + "</b> został dodant z bazy danych";
                    info_label.CssClass ="goodNotification";
                    info_label.Visible = true;
                }
                else { info_label.Text = ""; }

                if (Request.QueryString["nodeleteteamid"] != null && Request.QueryString["nodeleteteamName"] != null && Request.QueryString["userscount"] == null)
                {
                    string noteamdeletename = Request.QueryString["nodeleteteamName"];
                    string noteamdeleteid = Request.QueryString["nodeleteteamid"];
                    info_label.Text = "Nie można usunąc tamu <b>id: " + noteamdeleteid + "</b>. Taki team nie istnieje w bazie danych";
                    info_label.CssClass = "failureNotification";
                    info_label.Visible = true;
                }

                if (Request.QueryString["deleteteamid"] != null && Request.QueryString["deleteteamName"] != null)
                {
                    string teamdeleteid = Request.QueryString["deleteteamid"];
                    string teamdeletename = Request.QueryString["deleteteamName"];
                    info_label.Text = "Team <b> " + teamdeletename + "</b> (id: " + teamdeleteid + ") został usunięty z bazy danych";
                    info_label.CssClass = "failureNotification";
                    info_label.Visible = true;
                }

                if (String.Equals(Request.QueryString["userscount"],"1"))
                {
                    string teamdeleteid = Request.QueryString["nodeleteteamid"];
                    string teamdeletename = Request.QueryString["nodeleteteamName"];
                    info_label.Text = "Team <b> " + teamdeletename + "</b> (id: " + teamdeleteid + ") nie może zostac usunięty, gdyż są do niego wciąż podłączeni pracownicy<br />By usunąś team odłącz od niego wszystkich przcowników. <a href='/hr/manageteam.aspx?teamid=" + teamdeleteid + "'>Zarządzaj Teamem " + teamdeletename + "</a>";
                    info_label.CssClass = "failureNotification";
                    info_label.Visible = true;
                }

                GridView1.DataSource = td;
                GridView1.DataBind();
                DropDownMenager.DataBind();
            }
            else 
            {
                string menager1 = Request.Form[DropDownMenager.UniqueID];
                DropDownMenager.Text = menager1;
                info_label.Text = "";
            }
          }

        protected void addButton_Click(object sender, EventArgs e)
        {

            hiddendiv.Attributes["style"] = "float:left;width:250px;";
            addTeamNameLab.Visible= true;
            addTeamNameTextBox.Visible = true;
            DropDownMenagerLab.Visible = true;
            DropDownMenager.Visible = true;
            ZapiszButton.Visible = true;
            AnulujButton.Visible = true;
            DropDownMenager.DataBind();

        }

        protected void AnulujButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("menageteams.aspx");
        }

        protected void ZapiszButton_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid) 
            {
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO Teams (Name, Menager) VALUES (@name, @menager)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = addTeamNameTextBox.Text;
                    cmd.Parameters.Add("@menager", SqlDbType.VarChar, 50).Value = DropDownMenager.SelectedValue;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    cmd.Cancel();

                }

                List<string> userlist = new List<string>();
                userlist.Add(addTeamNameTextBox.Text);
                PolaczenieSQL.addHRhistory(userlist, 0, "Team '" + addTeamNameTextBox.Text + "' was created", Context.User.Identity.Name, "Teams actions");
                user u = PolaczenieSQL.find_user(DropDownMenager.SelectedValue);
                PolaczenieSQL.addHRhistory(userlist, 0, "Team '" + addTeamNameTextBox.Text + "' has new meanger " + u.ToString(), Context.User.Identity.Name, "Teams actions");

                string team_name = addTeamNameTextBox.Text;
                string url_text = "menageteams.aspx?teamadd=" + team_name;
                Response.Redirect(url_text);
            }
        }

        
    }
}