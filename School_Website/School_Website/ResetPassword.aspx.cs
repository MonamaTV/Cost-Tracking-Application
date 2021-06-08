using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Website
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RequestPassword"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            HttpCookie RequestCookies = Request.Cookies["userInfo"];
            string tempPassword = "";
            string tempEmail = "";
            if (RequestCookies != null)
            {
                tempPassword = RequestCookies["tempPassword"].ToString();
                tempEmail = RequestCookies["email"].ToString();
                if (tempPassword.Equals(password.Value) && tempEmail.Equals(username.Value))
                {
                    Response.Redirect("ChangePassword.aspx");
                }
                else
                {
                    password.Value = "";
                    username.Value = "";
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


    }
}