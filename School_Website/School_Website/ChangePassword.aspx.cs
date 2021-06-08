using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Website
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RequestPassword"] == null)
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void Change_Click(object sender, EventArgs e)
        {
            if (VerifyInput())
            {       
                if (/*service.ChangeUserPassword(email.Value, password.Value, password1.Value) > 0*/ true)
                {
                    Session["RequestPassword"] = null;
                    Response.Redirect("Home.aspx");
                }
            }
            else
            {
               
            }
        }
        private bool VerifyInput()
        {
            if (!(!password.Value.Equals("") && !password1.Value.Equals(""))) return false;
            if (!password.Value.Equals(password1.Value)) return false;
            return true;
        }
    }
}