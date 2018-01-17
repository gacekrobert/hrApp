using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Drawing;
using holidays;

namespace WebApplication4.all
{
    public partial class myaccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // If querystring value is missing, send the user to ManageUsers.aspx
                string userName = Context.User.Identity.Name;
                //if (string.IsNullOrEmpty(userName))
                //    Response.Redirect("manageusers.aspx");

                holidays.user u = PolaczenieSQL.find_user(userName, false, true);
                
                // Get information about this user
                MembershipUser usr = Membership.GetUser(userName);

                //if (usr == null)
                //    Response.Redirect("manageusers.aspx");

                user_name_label.InnerText = "User Account: " + usr.UserName;
                TextBox_email.Text = usr.Email;
                //HiddenFielduserid.Value = usr.UserName;

                if (Roles.IsUserInRole(usr.UserName, "emploee")) { Pracownik.Checked = true; }
                if (Roles.IsUserInRole(usr.UserName, "hr_emploee")) { PracownikHR.Checked = true; }
                if (Roles.IsUserInRole(usr.UserName, "Administrators")) { PracownikAdmin.Checked = true; }
                if (Roles.IsUserInRole(usr.UserName, "menager")) { PracownikMenager.Checked = true; }

                ImieTextBox.Text = u.imie.ToString();
                NazwiskoTextBox.Text = u.nazwisko.ToString();
                PeselTextBox.Text = u.pesel.ToString();
                string data_urodz = u.data_urodzenia.ToString();
                string data_zatrudnienia = u.data_zatrudnienia.ToString();
                string kiedy26 = u.kiedy26.ToString();
                TeamTextBox.Text = u.team.ToString();

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


                    switch (u.dniurlopowe.ToString())
                    {
                        case "1":
                            DropDownList2TextBox.Text = "20";
                            break;
                        case "2":
                            DropDownList2TextBox.Text = "26";
                            break;
                        default:
                            DropDownList2TextBox.Text = "26";
                            break;
                    }

                    if (u.dniurlopowe.ToString() == "1")
                    {
                        uzyska26label.Visible = true;
                        uzyska26TextBox.Visible = true;
                        if (kiedy26 != string.Empty)
                        {
                            myTime = DateTime.Parse(kiedy26);
                            uzyska26TextBox.Text = myTime.ToString("dd-MM-yyyy");
                        }
                    }

                    if (u.passtemp.ToString() == "True")
                    {
                        info_label.Visible = true;
                        info_label.Text = "Your password is setup as tempolary. Please change your password now!";
                    }
                    else
                    {
                        info_label.Visible = false;
                    }
                }

        }

        protected void reset_Click(object sender, EventArgs e)
        {
                string url_text;
                url_text = "~/Account/ChangePassword.aspx";
                    Response.Redirect(url_text);

        }

    }
}