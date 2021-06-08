using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;
namespace School_Website
{
    public partial class Login : System.Web.UI.Page
    {
        readonly NSNPServiceClient service = new NSNPServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Signin_Click(object sender, EventArgs e)
        {
           if(VerifyInput())
            {
                if (service.LoginEntity(username.Value, password.Value))
                {
                    Session["Email"] = username.Value;
                    Session["Password"] = password.Value;
                    Session["Level"] = service.GetLevel(username.Value, password.Value);
                    Response.Redirect("Home.aspx");
                }
                else
                {
          
                    Alert.Attributes.Add("class", "alert alert-danger show-alert");
                    password.Value = "";
                }
            }
            else
            {
                ErrorMessage("Fill in all the fields");
                password.Value = "";
            }
        }
        private void ErrorMessage(string message)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox", "<script language='javascript'>alert('" + message + "');</script>");
        }
        private bool VerifyInput()
        {
            if (username.Value.Equals(""))
                return false;
            if (password.Value.Equals(""))
                return false;
            return true;
        }
    }
}