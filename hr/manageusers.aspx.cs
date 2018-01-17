using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using holidays;
using System.Web.UI.HtmlControls;


namespace WebApplication4
{
    public partial class manageusers : System.Web.UI.Page
    {

        MembershipUserCollection users;
        MembershipUserCollection usersNoRoles = new MembershipUserCollection();
        MembershipUserCollection usersinadmin = new MembershipUserCollection();
        string[] usersadministrators;
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                // If querystring value is missing, send the user to ManageUsers.aspx
                string usersName = Request.QueryString["users"];

                if (string.IsNullOrEmpty(usersName))
                    usersName = "emploee";
                //Response.Redirect("manageusers.aspx?users=emploee");
                //else
                //{
                if (usersName == "unregister")
                {
                    users = Membership.GetAllUsers();

                    foreach (MembershipUser user in users)
                    {
                        string[] roles = Roles.GetRolesForUser(user.UserName);

                        if (roles.Count() == 0)
                        {
                            usersNoRoles.Add(user);
                        }

                    }
                    users_name_label2.Text = "Niezarejestrowani";
                    Session["LastUsers"] = "unregister";
                    GridView1.Columns[0].Visible = true;
                    GridView1.Columns[5].Visible = false;
                    GridView1.Columns[6].Visible = false;
                    GridView1.Columns[7].Visible = false;
                    GridView1.Columns[8].Visible = false;
                    GridView1.DataSource = usersNoRoles;
                    GridView1.DataBind();

                    tabele1.Visible = false;
                }
                else
                {
                    var functions = usersName;
                    usersadministrators = Roles.GetUsersInRole(functions);

                    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
                    conn.Open();
                    string sql = "SELECT UserName, Imie, Nazwisko, Name, dniurlopowe, kiedy26 FROM aspnet_Users INNER JOIN Teams ON aspnet_Users.team=Teams.Id Where UserName = @user";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@user", SqlDbType.VarChar, 50);//sha
                    SqlDataReader sdr;
                    DataTable td = new DataTable();
                    td.Columns.Add("UserName");
                    td.Columns.Add("Email");
                    td.Columns.Add("Imie i nazwisko");
                    td.Columns.Add("Team");
                    td.Columns.Add("Podstawa urlopowa");
                    td.Columns.Add("Kiedy 26");
                    DataRow dr = null;
                    DateTime myTime;

                    foreach (string user in usersadministrators)
                    {
                        cmd.Parameters["@user"].Value = Membership.GetUser(user).UserName;
                        sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            dr = td.NewRow();
                            string aaa = sdr["Imie"].ToString();
                            string bbb = sdr["Nazwisko"].ToString();
                            dr[0] = Membership.GetUser(user).UserName.ToString();
                            dr[1] = Membership.GetUser(user).Email.ToString();
                            dr[2] = aaa + " " + bbb;
                            dr[3] = sdr["Name"].ToString();
                            switch (sdr["dniurlopowe"].ToString())
                            {
                                case "1":
                                    dr[4] = "20 dni";
                                    break;
                                default:
                                    dr[4] = "26 dni";
                                    break;
                            }
                            dr[5] = sdr["kiedy26"].ToString();
                            td.Rows.Add(dr);
                        }
                        sdr.Close();
                    }
                    conn.Close();

                    switch (usersName)
                    {
                        case "menager":
                            users_name_label2.Text = "Menagerowie";
                            break;
                        case "Administrators":
                            users_name_label2.Text = "Administratorzy";
                            break;
                        case "hr_emploee":
                            users_name_label2.Text = "Pracownicy kadr";
                            break;
                        default:
                            users_name_label2.Text = "Pracownicy";
                            break;
                    }

                    Session["LastUsers"] = usersName;
                    GridView1.Columns[0].Visible = false;
                    GridView1.Columns[4].Visible = true;
                    GridView1.Columns[5].Visible = true;
                    GridView1.DataSource = td;
                    GridView1.DataBind();

                    tabele1.Visible = true;
                    string date_string;

                    for (int i = 1; i < GridView1.Rows.Count; i++)
                    {   
                        date_string = GridView1.Rows[i].Cells[8].Text;

                        if (date_string.Length > 7) 
                        {
                            myTime = DateTime.Parse(date_string);
                            if (myTime < DateTime.Today) { GridView1.Rows[i].BackColor = Color.LightPink; }
                        }
                    }
                    GridView1.Columns[8].Visible = false;
                }

                //}

                if (Request.QueryString["nouserdeleted"] != null)
                {
                    string nousersdelete = Request.QueryString["nouserdeleted"];
                    info_label.Text = "Użytkownik <b> " + nousersdelete + "</b> nie może zostać usunięty. Posiada on wciąż aktywne funkcje.";
                    info_label.Visible = true;
                }

                if (Request.QueryString["deleteMenager"] != null)
                {
                    string nomenagerdelete = Request.QueryString["deleteMenager"];
                    info_label.Text = "Użytkownik <b> " + nomenagerdelete + "</b> nie może zostać usunięty. Jest on wciąż menagerem jednego z teamów.";
                    info_label.Visible = true;
                }

                if (Request.QueryString["userdeleted"] != null)
                {
                    string usersdelete = Request.QueryString["userdeleted"];
                    info_label.Text = "Użytkownik <b> " + usersdelete + "</b> został usunięty z bazy danych";
                    info_label.Visible = true;
                }

            }

        }

        protected void unregister_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // Bind users to ListBox.

                users = Membership.GetAllUsers();

                foreach (MembershipUser user in users)
                {
                    string[] roles = Roles.GetRolesForUser(user.UserName);

                    if (roles.Count() == 0)
                    {
                        usersNoRoles.Add(user);
                    }

                }
                users_name_label2.Text = "Niezarejestrowani";
                Session["LastUsers"] = "unregister";
                GridView1.Columns[0].Visible = true;
                GridView1.Columns[5].Visible = false;
                GridView1.Columns[6].Visible = false;
                GridView1.Columns[7].Visible = false;
                GridView1.DataSource = usersNoRoles;
                GridView1.DataBind();

                tabele1.Visible = false;
                info_label.Visible = false;

            }

        }

        protected void administrators_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                var functions = ((LinkButton)sender).CommandName;
                usersadministrators = Roles.GetUsersInRole(functions);

                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
                conn.Open();
                string sql = "SELECT UserName, Imie, Nazwisko, Name, dniurlopowe, kiedy26 FROM aspnet_Users INNER JOIN Teams ON aspnet_Users.team=Teams.Id Where UserName = @user";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@user", SqlDbType.VarChar, 50);
                SqlDataReader sdr;
                DataTable td = new DataTable();
                td.Columns.Add("UserName");
                td.Columns.Add("Email");
                td.Columns.Add("Imie i nazwisko");
                td.Columns.Add("Team");
                td.Columns.Add("Podstawa urlopowa");
                td.Columns.Add("Kiedy 26");
                DataRow dr = null;

                foreach (string user in usersadministrators)
                {
                    cmd.Parameters["@user"].Value = Membership.GetUser(user).UserName;
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        dr = td.NewRow();
                        string aaa = sdr["Imie"].ToString();
                        string bbb = sdr["Nazwisko"].ToString();
                        dr[0] = Membership.GetUser(user).UserName.ToString();
                        dr[1] = Membership.GetUser(user).Email.ToString();
                        dr[2] = aaa + " " + bbb;
                        dr[3] = sdr["Name"].ToString();
                        switch (sdr["dniurlopowe"].ToString())
                        {
                            case "1":
                                dr[4] = "20 dni";
                                break;
                            default:
                                dr[4] = "26 dni";
                                break;
                        }
                        dr[5] = sdr["kiedy26"].ToString();
                        td.Rows.Add(dr);
                    }
                    sdr.Close();
                }
                conn.Close();

                switch (functions)
                {
                    case "menager":
                        users_name_label2.Text = "Menagerowie";
                        break;
                    case "Administrators":
                        users_name_label2.Text = "Administratorzy";
                        break;
                    case "hr_emploee":
                        users_name_label2.Text = "Pracownicy kadr";
                        break;
                    default:
                        users_name_label2.Text = "Pracownicy";
                        break;
                }

                Session["LastUsers"] = functions;
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[5].Visible = true;
                GridView1.Columns[6].Visible = true;
                GridView1.Columns[7].Visible = true;

                //GridView1.Columns[0].Visible = false;
                //GridView1.Columns[4].Visible = true;
                //GridView1.Columns[5].Visible = true;
                //GridView1.Columns[8].Visible = true;
                GridView1.DataSource = td;
                GridView1.DataBind();
                string date_string;

                tabele1.Visible = true;

                DateTime myTime;

                for (int i = 1; i < GridView1.Rows.Count; i++)
                {
                    date_string = GridView1.Rows[i].Cells[8].Text;

                    if (date_string.Length > 7)
                    {
                        myTime = DateTime.Parse(date_string);
                        if (myTime < DateTime.Today) { GridView1.Rows[i].BackColor = Color.LightPink; }
                    }
                }
                GridView1.Columns[8].Visible = false;
                info_label.Visible = false;

            }

        }

        protected void from20t026_Click(object sender, EventArgs e)
        {
            List<string> user_name_list = new List<string>();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        user_name_list.Add(row.Cells[3].Text);
                        chkRow.Checked = false;
                    }
                }
            }
            if (user_name_list.Count > 0) 
            {
                PolaczenieSQL.update20to26(user_name_list, Context.User.Identity.Name, "2");
                PolaczenieSQL.addHRhistory(user_name_list, 0, "Employee reach proper expierience", Context.User.Identity.Name, "Go from 20 to 26 holidays days");
                Response.Redirect("manageusers.aspx");
            }
            
        }

        protected void Plus_Click(object sender, EventArgs e)
        {
            int textboxvalue = Convert.ToInt16(TextBoxAdddays.Text);
            textboxvalue = textboxvalue + 1;
            TextBoxAdddays.Text = Convert.ToString(textboxvalue);
        }

        protected void Minus_Click(object sender, EventArgs e)
        {
            int textboxvalue = Convert.ToInt16(TextBoxAdddays.Text);
            textboxvalue = textboxvalue - 1;
            TextBoxAdddays.Text = Convert.ToString(textboxvalue);
        }

        protected void addextraday_Click(object sender, EventArgs e)
        {
            List<string> user_name_list = new List<string>();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        user_name_list.Add(row.Cells[3].Text);
                        chkRow.Checked = false;
                    }
                }
            }

            int addnumber = Convert.ToInt16(TextBoxAdddays.Text);
            string reasonText = TextBoxReason.Text;
            if (user_name_list.Count > 0 && addnumber != 0 && reasonText.Length > 0) 
            {
                PolaczenieSQL.addextraday(user_name_list, addnumber);
                PolaczenieSQL.addHRhistory(user_name_list, addnumber, reasonText, Context.User.Identity.Name, "Addtional days");
                Response.Redirect("manageusers.aspx");
            }
            
        }

        protected void Invert_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        chkRow.Checked = false;
                    }
                    else 
                    {
                        chkRow.Checked = true;
                    }
                }
            }

                //Response.Redirect("manageusers.aspx");

            
        }

        protected void buttonAdduser_click(object sender, EventArgs e)
        {
            string url_text = "~/Account/Register.aspx";
            Response.Redirect(url_text);
        }

    }
}
