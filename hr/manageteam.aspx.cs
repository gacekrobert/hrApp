using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using holidays;

namespace WebApplication4.hr
{
    public partial class manageteam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                string[] menagers;
                string teamID = Request.QueryString["teamid"];
                if (string.IsNullOrEmpty(teamID))
                    Response.Redirect("menageteams.aspx");

                int recordcount;

                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);

                conn.Open();
                string sql = "SELECT COUNT(Id) From Teams Where Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = teamID;
                cmd.CommandType = CommandType.Text;
                recordcount = (int)cmd.ExecuteScalar();
                cmd.Cancel();

                if (!int.Equals(recordcount, 1))
                    Response.Redirect("menageteams.aspx");

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

                sqlquery = "SELECT Id, Name, Menager FROM Teams Where Id = @id";

                command = new SqlCommand(sqlquery, conn);
                SqlDataReader sdr;
                //ListItem nextitem;
                command.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = teamID;

                sdr = command.ExecuteReader();

                while (sdr.Read())
                {

                    addTeamNameTextBox.Text = sdr["Name"].ToString();
                    HiddenFieldTeamName.Value = sdr["Name"].ToString();

                    if (sdr["Menager"].ToString() != string.Empty)
                    {
                        string item_i = sdr["Menager"].ToString();
                        for (int i = 0; i <= DropDownMenager.Items.Count; i++)
                        {
                            if (DropDownMenager.Items[i].Value == item_i)
                            {
                                DropDownMenager.Items[i].Selected = true;
                                HiddenFieldMenager.Value = DropDownMenager.SelectedItem.Text;
                                break;
                            }
                        }
                    }
                }
                sdr.Close();

                string sql3 = "SELECT UserName, Imie, Nazwisko FROM aspnet_Users Where Team = @team";
                SqlCommand cmd2 = new SqlCommand(sql3, conn);
                cmd2.Parameters.Add("@team", SqlDbType.VarChar, 50).Value = teamID;

                SqlDataReader sdr3;

                DataTable td = new DataTable();
                td.Columns.Add("UserName");
                td.Columns.Add("Email");
                td.Columns.Add("Imie i nazwisko");
                td.Columns.Add("Funkcja");
                DataRow dr = null;

                sdr3 = cmd2.ExecuteReader();
                while (sdr3.Read())
                {
                    dr = td.NewRow();
                    string aaa = sdr3["Imie"].ToString();
                    string bbb = sdr3["Nazwisko"].ToString();
                    string user = sdr3["Username"].ToString();
                    dr[0] = Membership.GetUser(user).UserName.ToString();
                    dr[1] = Membership.GetUser(user).Email.ToString();
                    dr[2] = aaa + " " + bbb;
                    if (Roles.IsUserInRole(user, "Menager"))
                    {
                        dr[3] = "Menager";
                    }
                    td.Rows.Add(dr);
                }
                sdr3.Close();

                conn.Close();

                Session["LastUsers"] = "team#" + teamID;
                GridView1.DataSource = td;
                GridView1.DataBind();
            }
            else
            {

            }


        }

        protected void goBack_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string url_text = "menageteams.aspx";
                Response.Redirect(url_text);
            }

        }

        protected void AnulujButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void ZapiszButton_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid) 
            {
                string teamID = Request.QueryString["teamid"];
                if (string.IsNullOrEmpty(teamID))
                    Response.Redirect("menageteams.aspx");

                string teame_name = addTeamNameTextBox.Text;
                string selected_menager = DropDownMenager.SelectedValue;

                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                {
                    conn.Open();
                    string sql = "UPDATE Teams SET Name = @name, Menager = @menager Where Id = @id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = teamID;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = teame_name;
                    cmd.Parameters.Add("@menager", SqlDbType.VarChar, 50).Value = selected_menager;
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                    conn.Close();
                }

                if (addTeamNameTextBox.Text != HiddenFieldTeamName.Value)
                {
                    List<string> userlist = new List<string>();
                    userlist.Add(HiddenFieldTeamName.Value);
                    PolaczenieSQL.addHRhistory(userlist, 0, "Team '" + HiddenFieldTeamName.Value + "' change name to '" + addTeamNameTextBox.Text + "'", Context.User.Identity.Name, "Teams actions");
                }

                if (DropDownMenager.SelectedItem.Text != HiddenFieldMenager.Value)
                {
                    List<string> menagerlist = new List<string>();
                    menagerlist.Add(addTeamNameTextBox.Text);
                    PolaczenieSQL.addHRhistory(menagerlist, 0, "Team '" + HiddenFieldTeamName.Value + "' change menager to " + DropDownMenager.SelectedItem.Text, Context.User.Identity.Name, "Teams actions");
                }

            }

            Response.Redirect(Request.RawUrl);
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            DropDownMenager.Enabled = true;
            ZapiszButton.Visible = true;
            AnulujButton.Visible = true;
            addTeamNameTextBox.ReadOnly = false;
            addTeamNameTextBox.CssClass = "textEntry2";
        }
    }
}