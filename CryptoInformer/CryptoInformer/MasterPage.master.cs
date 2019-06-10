using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String email = (String)(Session["email"]);
        if (email != null)
        {
            userLabel.Text = "User: " + email;

            registerLabel.Visible = false;
            loginLabel.Visible = false;
            logoutLabel.Visible = true;
        }
        else
        {
            userLabel.Text = "";
            registerLabel.Visible = true;
            loginLabel.Visible = true;
            logoutLabel.Visible = false;
        }
    }

    protected void LogOutEventMethod(object sender, EventArgs e)
    {
        Session["email"] = null;
        Session["userID"] = null;
        Response.Redirect("Default.aspx", false);
    }

}
