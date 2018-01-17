using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace WebApplication4
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if ((!Roles.IsUserInRole(Context.User.Identity.Name, "Administrators")) && (!Roles.IsUserInRole(Context.User.Identity.Name, "hr_emploee")))
            {
                MenuItem mnuItem = NavigationMenu.FindItem("Manage Users"); 
                NavigationMenu.Items.Remove(mnuItem);
                mnuItem = NavigationMenu.FindItem("Manage Teams");
                NavigationMenu.Items.Remove(mnuItem);
                mnuItem = NavigationMenu.FindItem("Ustawienia HR");
                NavigationMenu.Items.Remove(mnuItem);
                mnuItem = NavigationMenu.FindItem("HR Manage Holidays");
                NavigationMenu.Items.Remove(mnuItem);
            }

            if ((!Roles.IsUserInRole(Context.User.Identity.Name, "menager")))
            {
                MenuItem mnuItem = NavigationMenu.FindItem("My Team Holidays");
                NavigationMenu.Items.Remove(mnuItem);
            }

            if ((!Roles.IsUserInRole(Context.User.Identity.Name, "emploee")))
            {
                MenuItem mnuItem = NavigationMenu.FindItem("My Holidays");
                NavigationMenu.Items.Remove(mnuItem);
            }

       }
    }

}
