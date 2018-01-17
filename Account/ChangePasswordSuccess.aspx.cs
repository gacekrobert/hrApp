using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using holidays;

namespace WebApplication4.Account
{
    public partial class ChangePasswordSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PolaczenieSQL.updatepasstemp("no", Context.User.Identity.Name);
        }
    }
}
