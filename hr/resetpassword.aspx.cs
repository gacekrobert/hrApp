using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using holidays;
using System.Drawing;

namespace WebApplication4.hr
{
    public partial class resetpassword : System.Web.UI.Page
    {
        MembershipUser u;

        public void Page_Load(object sender, EventArgs args)
        {
            if (!Membership.EnablePasswordReset)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            Msg.Text = "";

            if (!IsPostBack)
            {
                Msg.ForeColor = Color.Maroon;
                Msg.Text = "Please supply a username.";

                if (Request.QueryString["userid"] != null)
                {
                    string userid = Request.QueryString["userid"];
                    PolaczenieSQL.list_of_usersid(PersonDropDown, userid);
                    VerifyUsername();
                }
                else
                {
                    PolaczenieSQL.list_of_usersid(PersonDropDown);
                }
            }
            else
            {
                VerifyUsername();
            }
        }


        public void VerifyUsername()
        {
            u = Membership.GetUser(PersonDropDown.Text, false);

            if (u == null)
            {
                Msg.ForeColor = Color.Maroon;
                Msg.Text = "Username " + Server.HtmlEncode(PersonDropDown.Text) + " not found. Please check the value and re-enter.";
                Msg2.Text = "";
                
                ResetPasswordButton.Enabled = false;
            }
            else
            {
                ResetPasswordButton.Enabled = true;
                Msg2.Text = "";
                Msg.Text = "";
            }
        }

        public void ResetPassword_OnClick(object sender, EventArgs args)
        {
            string newPassword;

            u = Membership.GetUser(PersonDropDown.Text, false);

            if (u == null)
            {
                Msg.ForeColor = Color.Maroon;
                Msg.Text = "Username " + Server.HtmlEncode(PersonDropDown.Text) + " not found. Please check the value and re-enter.";
                return;
            }

            try
            {
                newPassword = u.ResetPassword();
            }
            catch (MembershipPasswordException e)
            {
                Msg.ForeColor = Color.Maroon;
                Msg.Text = "Invalid password answer. Please re-enter and try again.";
                return;
            }
            catch (Exception e)
            {
                Msg.Text = e.Message;
                return;
            }

            if (newPassword != null)
            {
                Msg.ForeColor = Color.DarkGreen;
                Msg.Text = "Password reset. Your new password is: ";
                Msg2.Text = Server.HtmlEncode(newPassword);
                mailSender.SendMailPassRestart(Server.HtmlEncode(newPassword), u.Email, u.ToString());
                PolaczenieSQL.updatepasstemp("yes", u.UserName);
            }
            else
            {
                Msg.Text = "Password reset failed. Please re-enter your values and try again.";
            }
        }
    }
}