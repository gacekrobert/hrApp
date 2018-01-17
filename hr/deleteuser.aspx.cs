using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication4.hr
{
    public partial class deleteuser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // If querystring value is missing, send the user to ManageUsers.aspx
                string userName = Request.QueryString["deleteuser"];
                if (string.IsNullOrEmpty(userName))
                    Response.Redirect("manageusers.aspx");


                // Get information about this user
                MembershipUser usr = Membership.GetUser(userName);

                if (usr == null)
                    Response.Redirect("manageusers.aspx");

                string[] sss = Roles.GetRolesForUser(userName);

                if (sss.Length != 0)
                {
                    Response.Redirect("manageusers.aspx?users=emploee&nouserdeleted=" + userName);
                }

                user_name_label.InnerText = "Delete User: " + usr.UserName + "?";
                label_text2.Text = "Czy na pewno chcesz usunąć użytkownika: <b>" + usr.UserName + "</b> (" + usr.Email.ToString() + ") ?<br />Wszystkie jego dane zostana bezpowrotnie usunięte!";
            }

        }

        protected void anuluj_btn_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string url_text = "manageusers.aspx?users=unregister";
                Response.Redirect(url_text);
            }
        }

        protected void delete_btn_Click(object sender, EventArgs e)
        {
            string userName = Request.QueryString["deleteuser"];
            if (string.IsNullOrEmpty(userName))
                Response.Redirect("manageusers.aspx");

            bool menager_yes = false;

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
                        if (sdr2[0].ToString() == userName) 
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


            // Get information about this user

            if (menager_yes)
            {
                Response.Redirect("manageusers.aspx?users=emploee&deleteMenager=" + userName);
            }
            else 
            {
                MembershipUser usr = Membership.GetUser(userName);

                if (usr == null)
                {
                    Response.Redirect("manageusers.aspx");
                }
                else 
                {
                    string user_name = usr.UserName;
                    Membership.DeleteUser(usr.UserName, true);
                    string url_text = "manageusers.aspx?users=unregister&userdeleted=" + user_name;
                    Response.Redirect(url_text);
                }     
            }
        }
    }
}