using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Website
{
    public partial class Signout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Password"] = null;
            Session["Email"] = null;
            Session["Level"] = null;
            Session["Code"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}