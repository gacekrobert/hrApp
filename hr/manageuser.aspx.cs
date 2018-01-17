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


namespace WebApplication4.hr
{
    public partial class manageuser : System.Web.UI.Page                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            info_label.Visible = false;

            if (!Page.IsPostBack)
            {
                // If querystring value is missing, send the user to ManageUsers.aspx
                string userName = Request.QueryString["user"];
                if (string.IsNullOrEmpty(userName))
                    Response.Redirect("manageusers.aspx");


                // Get information about this user
                MembershipUser usr = Membership.GetUser(userName);

                if (usr == null)
                    Response.Redirect("manageusers.aspx");

                

                user_name_label.InnerText = "Manage User: " + usr.UserName;
                TextBox_email.Text = usr.Email;
                HiddenFielduserid.Value = usr.UserName;

                if (Roles.IsUserInRole(usr.UserName, "emploee")) { Pracownik.Checked = true; }
                if (Roles.IsUserInRole(usr.UserName, "hr_emploee")) { PracownikHR.Checked = true; }
                if (Roles.IsUserInRole(usr.UserName, "Administrators")) { PracownikAdmin.Checked = true; }
                if (Roles.IsUserInRole(usr.UserName, "menager")) { PracownikMenager.Checked = true; }

                if (usr.IsLockedOut) 
                { 
                     CheckBoxZablokowany.Checked = true;
                     ButtonResetPassword.Visible = false;
                } else 
                { 
                    CheckBoxZablokowany.Checked = false;
                    ButtonResetPassword.Visible = true;
                }

                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);

                string sqlquery = "SELECT UserName, Imie, Nazwisko, pesel, data_urodz, data_zatrudnienia, team, dniurlopowe, kiedy26, dni_p_rok , dni_o_rok , dni_n_rok , dni_nz, dni_ekstra, dni_wyk FROM aspnet_Users Where UserName = @user";
                conn.Open();
                SqlCommand command = new SqlCommand(sqlquery, conn);
                command.Parameters.Add("@user", SqlDbType.VarChar, 50).Value = usr.UserName;
                SqlDataReader sdr = command.ExecuteReader();

                SqlTeams.DataBind();

                DropDownList1.DataBind();


                while (sdr.Read())
                {
                    ImieTextBox.Text = sdr["Imie"].ToString();
                    NazwiskoTextBox.Text = sdr["Nazwisko"].ToString();
                    PeselTextBox.Text = sdr["pesel"].ToString();
                    string data_urodz = sdr["data_urodz"].ToString();
                    string data_zatrudnienia = sdr["data_zatrudnienia"].ToString();
                    string kiedy26 = sdr["kiedy26"].ToString();

                    string[] days_of_holidays = holidays.PolaczenieSQL.find_holiday_days_byuser(usr.UserName);
                    Labelpr.Text = days_of_holidays[0];
                    Labelor.Text = days_of_holidays[8];
                    Labelnr.Text = days_of_holidays[9];
                    Labeldd.Text = days_of_holidays[5];
                    Labelnz.Text = days_of_holidays[6];
                    Labelsum.Text = days_of_holidays[10];
                    Labelwyk.Text = days_of_holidays[3];

                    if (Convert.ToInt16(Labelsum.Text) > 0) { suncell.BackColor = Color.LightGreen; }
                    if (Convert.ToInt16(Labelsum.Text) <= 0) { suncell.BackColor = Color.LightPink; }

                    DateTime myTime;

                    if (data_urodz != string.Empty)
                    {
                        myTime = DateTime.Parse(data_urodz);
                        Dataurodzenia.Text = myTime.ToString("dd-MM-yyyy");
                    }


                    if (data_zatrudnienia != string.Empty)
                    {
                        myTime = DateTime.Parse(data_zatrudnienia);
                        DataZatrudnienia.Text = myTime.ToString("dd-MM-yyyy");
                    }
                    else
                    {
                        SaveUserButton.Text = "Dodaj nowego pracownika";
                    }


                    if (sdr["team"].ToString() != string.Empty)
                    {
                        string item_i = sdr["team"].ToString();
                        for (int i = 0; i <= DropDownList1.Items.Count; i++)
                        {
                            if (DropDownList1.Items[i].Value == item_i)
                            {
                                DropDownList1.Items[i].Selected = true;
                                HiddenFieldTeam.Value = item_i;
                                HiddenTeamName.Value = DropDownList1.Items[i].Text;
                                break;
                            }
                        }
                    }
                    switch (sdr["dniurlopowe"].ToString())
                    {
                        case "1":
                            DropDownList2.Items[1].Selected = true;
                            HiddenField20t26.Value = "1";
                            break;
                        case "2":
                            DropDownList2.Items[2].Selected = true;
                            HiddenField20t26.Value = "2";
                            break;
                        default:
                            DropDownList2.Items[0].Selected = true;
                            break;
                    }
                    if (sdr["dniurlopowe"].ToString() == "1")
                    {
                        uzyska26label.Visible = true;
                        uzyska26TextBox.Visible = true;
                        if (kiedy26 != string.Empty)
                        {
                            myTime = DateTime.Parse(kiedy26);
                            uzyska26TextBox.Text = myTime.ToString("dd-MM-yyyy");
                        }
                    }
                }
                conn.Close();

                if (Request.QueryString["deleteMenager"] != null)
                {
                    string nomenagerdelete = Request.QueryString["deleteMenager"];
                    info_label.Text = "Użytkownik <b> " + nomenagerdelete + "</b> nie może stracić funkcji menagera. Jest on wciąż menagerem jednego z teamów.";
                    info_label.Visible = true;
                }

            }
            else
            {
                info_label.Visible = false;
                string date1 = Request.Form[DataZatrudnienia.UniqueID];
                DataZatrudnienia.Text = date1;
                string date2 = Request.Form[Dataurodzenia.UniqueID];
                Dataurodzenia.Text = date2;
                string date3 = Request.Form[uzyska26TextBox.UniqueID];
                uzyska26TextBox.Text = date3;
            }
        }

        protected void edytuj_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ImieTextBox.ReadOnly = false;
                ImieTextBox.CssClass = "textEntry";

                NazwiskoTextBox.ReadOnly = false;
                NazwiskoTextBox.CssClass = "textEntry";

                TextBox_email.ReadOnly = false;
                TextBox_email.CssClass = "textEntry";

                Pracownik.Enabled = true;
                PracownikHR.Enabled = true;
                PracownikMenager.Enabled = true;
                
                if (CheckBoxZablokowany.Checked == true)
                {
                    ButtonOdblokuj.Visible = true;
                }

                if (Roles.IsUserInRole(Context.User.Identity.Name, "Administrators"))
                {
                    PracownikAdmin.Enabled = true;
                }

                PeselTextBox.ReadOnly = false;
                PeselTextBox.CssClass = "textEntry";

                //Dataurodzenia.ReadOnly = false;
                Dataurodzenia.CssClass = "textEntry";
                ctrlCalendar.Enabled = true;


                //DataZatrudnienia.ReadOnly = false;
                DataZatrudnienia.CssClass = "textEntry";
                CalendarExtender1.Enabled = true;

                //uzyska26TextBox.ReadOnly = false;
                uzyska26TextBox.CssClass = "textEntry";
                CalendarExtender2.Enabled = true;
                
                DropDownList1.Enabled = true;
                DropDownList2.Enabled = true;

                SaveUserButton.Visible = true;
                CancelUserButton.Visible = true;

                imgCal.Visible = true;
                imgCal2.Visible = true;

                if (Equals(DropDownList2.SelectedItem.Value, "1"))
                {
                    uzyska26label.Visible = true;
                    uzyska26TextBox.Visible = true;
                    imgCal3.Visible = true;
                }
                else
                {
                    uzyska26TextBox.Visible = false;
                    imgCal3.Visible = false;
                    uzyska26label.Visible = false;
                }
                

            }

        }

        protected void reset_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["user"] != null)
            {
                string userid = Request.QueryString["user"];
                string url_text;
                if (!String.Equals(userid, ""))
                {
                    url_text = "resetpassword.aspx" + "?userid=" + userid;
                    Response.Redirect(url_text);
                }
            }
        }

        protected void goBack_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string url_text;
                if (Session["LastUsers"] != null)
                {
                    string users_name = Session["LastUsers"].ToString();
                    if (users_name.Contains("team"))
                    {
                        string team_number = users_name.Substring(5);
                        url_text = "manageteam.aspx?teamid=" + team_number;
                    }
                    else { url_text = "manageusers.aspx?users=" + users_name; }
                }
                else 
                {
                    url_text = "manageusers.aspx?users=emploee";
                }
                
                Response.Redirect(url_text);
            }

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Equals(DropDownList2.SelectedItem.Value, "1"))
            {
                uzyska26label.Visible = true;
                uzyska26TextBox.Visible = true;
                imgCal3.Visible = true;
            }
            else
            {
                uzyska26TextBox.Visible = false;
                imgCal3.Visible = false;
                uzyska26label.Visible = false;
            }
        }

        protected void Button1_Click(Object sender, EventArgs e)
        {
            Page.Validate();

            string userName = Request.QueryString["user"];
            if (string.IsNullOrEmpty(userName))
                Response.Redirect("manageusers.aspx");

            List<string> userlist = new List<string>();
            userlist.Add(HiddenFielduserid.Value);


            // Get information about this user
            MembershipUser usr = Membership.GetUser(userName);

            if (usr == null)
                Response.Redirect("manageusers.aspx");

            bool menager_yes = false;
            
            if ((Roles.IsUserInRole(usr.UserName, "menager")) && PracownikMenager.Checked == false)
            {
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
                {
                    conn.Open();
                    string sql = "Select Menager FROM Teams";
                    SqlCommand command = new SqlCommand(sql, conn);
                    SqlDataReader sdr2;

                    sdr2 = command.ExecuteReader();
                    if (sdr2.HasRows == true)
                    {
                        while (sdr2.Read())
                        {
                            if (sdr2[0].ToString() == usr.UserName)
                            {
                                menager_yes = true;
                                break;
                            }
                        }
                    }
                    sdr2.Close();
                    command.Cancel();
                    conn.Close();
                }

                if (menager_yes) 
                {
                    Response.Redirect("manageuser.aspx?user=" + usr.UserName + "&deleteMenager=" + usr.UserName);
                    return;
                }

                Roles.RemoveUserFromRole(usr.UserName, "menager");
                PolaczenieSQL.addHRhistory(userlist, 0, "Employee loses function 'Menager'", Context.User.Identity.Name, "Function change");
            }
            else if (!(Roles.IsUserInRole(usr.UserName, "menager")) && PracownikMenager.Checked == true)
            {
                Roles.AddUserToRole(usr.UserName, "menager");
                PolaczenieSQL.addHRhistory(userlist, 0, "Employee gain function 'Menager'", Context.User.Identity.Name, "Function change");
            }


            if ((Roles.IsUserInRole(usr.UserName, "emploee")) && Pracownik.Checked == false)
            {
                Roles.RemoveUserFromRole(usr.UserName, "emploee");
                PolaczenieSQL.addHRhistory(userlist, 0, "Employee loses function 'Emploee'", Context.User.Identity.Name, "Function change");
            }
            else if (!(Roles.IsUserInRole(usr.UserName, "emploee")) && Pracownik.Checked == true)
            {
                Roles.AddUserToRole(usr.UserName, "emploee");
                PolaczenieSQL.addHRhistory(userlist, 0, "Employee gain function 'Emploee'", Context.User.Identity.Name, "Function change");
            }

            if ((Roles.IsUserInRole(usr.UserName, "hr_emploee")) && PracownikHR.Checked == false)
            {
                Roles.RemoveUserFromRole(usr.UserName, "hr_emploee");
                PolaczenieSQL.addHRhistory(userlist, 0, "Employee loses function 'HR emploee'", Context.User.Identity.Name, "Function change");
            }
            else if (!(Roles.IsUserInRole(usr.UserName, "hr_emploee")) && PracownikHR.Checked == true)
            { 
                Roles.AddUserToRole(usr.UserName, "hr_emploee");
                PolaczenieSQL.addHRhistory(userlist, 0, "Employee gain function 'HR emploee'", Context.User.Identity.Name, "Function change");
            }

            if ((Roles.IsUserInRole(usr.UserName, "Administrators")) && PracownikAdmin.Checked == false)
            {
                Roles.RemoveUserFromRole(usr.UserName, "Administrators");
                PolaczenieSQL.addHRhistory(userlist, 0, "Employee loses function 'Administrator'", Context.User.Identity.Name, "Function change");
            }
            else if (!(Roles.IsUserInRole(usr.UserName, "Administrators")) && PracownikAdmin.Checked == true)
            { 
                Roles.AddUserToRole(usr.UserName, "Administrators");
                PolaczenieSQL.addHRhistory(userlist, 0, "Employee gain function 'Administrator'", Context.User.Identity.Name, "Function change");
            }


        using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString))
        {
        conn.Open();

        string sql = "";

        if (SaveUserButton.Text != "Dodaj nowego pracownika")
        {
            sql = "UPDATE aspnet_Users SET Imie = @imie, Nazwisko = @nazwisko, pesel = @pesel, data_urodz = @urodz, data_zatrudnienia = @zatrudnienia, team = @team, dniurlopowe = @dniurlopowe, kiedy26 = @kiedy26 Where UserName = @user";
        }
        else if (SaveUserButton.Text == "Dodaj nowego pracownika") 
        {
            sql = "UPDATE aspnet_Users SET Imie = @imie, Nazwisko = @nazwisko, pesel = @pesel, data_urodz = @urodz, data_zatrudnienia = @zatrudnienia, team = @team, dniurlopowe = @dniurlopowe, kiedy26 = @kiedy26, dni_p_rok = @dni_p_rok, dni_o_rok = @dni_o_rok, dni_n_rok = @dni_n_rok, dni_nz = @dni_nz  Where UserName = @user";

        }
        SqlCommand cmd = new SqlCommand(sql,conn);
        cmd.Parameters.Add("@imie", SqlDbType.VarChar, 50).Value = ImieTextBox.Text;
        cmd.Parameters.Add("@nazwisko", SqlDbType.VarChar, 50).Value = NazwiskoTextBox.Text;
        cmd.Parameters.Add("@pesel", SqlDbType.Float, 11).Value = PeselTextBox.Text;
        if (Dataurodzenia.Text != string.Empty)
        {
            string dateUr = Dataurodzenia.Text;
            DateTime datetimeUr = DateTime.ParseExact(dateUr, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.Add("@urodz", SqlDbType.Date, 50).Value = datetimeUr;
        }
        else
        {
            cmd.Parameters.Add("@urodz", SqlDbType.Date, 50).Value = DBNull.Value;
        }

            int dni_o_rok;

        if (DataZatrudnienia.Text != string.Empty)
        {
            string dateZa = DataZatrudnienia.Text;
            DateTime datetimeZa = DateTime.ParseExact(dateZa, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.Add("@zatrudnienia", SqlDbType.Date, 50).Value = datetimeZa;

            if (SaveUserButton.Text == "Dodaj nowego pracownika") 
            {
                double ma26;
                int ma26bis;
                if(String.Equals(DropDownList2.Text,"2"))
                {
                    ma26 = 2.16;
                    ma26bis = 26;
                }
                else
                {
                    ma26 = 1.66;
                    ma26bis = 20;
                }

                dni_o_rok = PageMetods.licz_dni_pierwszego_roku(datetimeZa,ma26);
                cmd.Parameters.Add("@dni_p_rok", SqlDbType.Int, 2).Value = 0;
                cmd.Parameters.Add("@dni_o_rok", SqlDbType.Int, 2).Value = dni_o_rok;
                cmd.Parameters.Add("@dni_n_rok", SqlDbType.Int, 2).Value = ma26bis;
                if (dni_o_rok >= 4)
                {
                    cmd.Parameters.Add("@dni_nz", SqlDbType.Int, 2).Value = 4;
                }
                else 
                {
                    cmd.Parameters.Add("@dni_nz", SqlDbType.Int, 2).Value = dni_o_rok;
                }
            }

        }else
        {
            cmd.Parameters.Add("@zatrudnienia", SqlDbType.Date, 50).Value = DBNull.Value;
        }

        //if (SaveUserButton.Text != "Dodaj nowego pracownika")
        //{
        //    cmd.Parameters.Add("@dni_p_rok", SqlDbType.Int, 2).Value = Convert.ToInt16(Labelpr.Text);
        //    cmd.Parameters.Add("@dni_o_rok", SqlDbType.Int, 2).Value = Convert.ToInt16(Labelor.Text);
        //    cmd.Parameters.Add("@dni_n_rok", SqlDbType.Int, 2).Value = Convert.ToInt16(Labelnr.Text);
        //    cmd.Parameters.Add("@dni_nz", SqlDbType.Int, 2).Value = Convert.ToInt16(Labelnz.Text);
        //}

        cmd.Parameters.Add("@team", SqlDbType.Int, 3).Value = DropDownList1.SelectedValue;
        cmd.Parameters.Add("@dniurlopowe", SqlDbType.Int, 2).Value = DropDownList2.Text;
        if (uzyska26TextBox.Text != string.Empty)
        {
            string date26 = uzyska26TextBox.Text;
            DateTime datetime26 = DateTime.ParseExact(date26, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.Add("@kiedy26", SqlDbType.Date, 50).Value = datetime26;
        }else
        {
            cmd.Parameters.Add("@kiedy26", SqlDbType.Date, 50).Value = DBNull.Value;
        }

        cmd.Parameters.Add("@user", SqlDbType.VarChar, 50).Value = usr.UserName;
        cmd.CommandType = CommandType.Text;
        cmd.ExecuteNonQuery();

        if (SaveUserButton.Text == "Dodaj nowego pracownika")
        {
            string newPassword;
            MembershipUser u;

            u = Membership.GetUser(userName, false);

            try
            {
                newPassword = u.ResetPassword();
            }
            catch (Exception e2)
            {
                return;
            }

            if (newPassword != null)
            {
                mailSender.SendMailNewAccount(Server.HtmlEncode(newPassword), u.Email, u.ToString());
                PolaczenieSQL.updatepasstemp("yes", u.UserName);
            }
        }

        if (HiddenField20t26.Value != DropDownList2.SelectedValue)
        {
            PolaczenieSQL.update20to26(userlist, Context.User.Identity.Name, DropDownList2.SelectedValue);
            if (DropDownList2.SelectedValue == "2") 
            {
                PolaczenieSQL.addHRhistory(userlist, 0, "Employee reach proper expierience", Context.User.Identity.Name, "Go from 20 to 26 holidays days");
            }
            
        }

        if ( HiddenFieldTeam.Value != DropDownList1.SelectedValue)
        {
            if (String.IsNullOrEmpty(HiddenFieldTeam.Value))
            {
                PolaczenieSQL.addHRhistory(userlist, 0, "New user account has been created: " + userName, Context.User.Identity.Name, "New user");

            }
            else
            {
                PolaczenieSQL.addHRhistory(userlist, 0, "Employee change team from " + HiddenTeamName.Value + " to " + DropDownList1.SelectedItem.Text, Context.User.Identity.Name, "Team change");
            }
        
        }



        usr.Email = TextBox_email.Text;
        Membership.UpdateUser(usr);
            }

            Response.Redirect(Request.RawUrl);
        }

        protected void anuluj_Click(object sender, EventArgs e)
        {
            string userName = Request.QueryString["user"];
            Response.Redirect("manageuser.aspx?user=" + userName);
            //Response.Redirect(Request.RawUrl);
        }

        protected void show_holidays_click(object sender, EventArgs e)
        {
            if (Request.QueryString["user"] != null) 
            {
                string userid = Request.QueryString["user"];
                string url_text;
                if (!String.Equals(userid, ""))
                {
                    url_text = "manageholidays.aspx" + "?userid=" + userid;
                    Response.Redirect(url_text);
                }
            }
           
        }

        protected void ButtonOdblokuj_Click(object sender, EventArgs e)
        {
            string texttosplit = user_name_label.InnerText;
            MembershipUser usr = Membership.GetUser(texttosplit.Replace("Manage User: ", ""));
            usr.UnlockUser();
            Response.Redirect(Request.RawUrl);

        }
    }
}
