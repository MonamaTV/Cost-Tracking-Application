using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;

namespace School_Website
{
    public partial class Register : System.Web.UI.Page
    {
        readonly NSNPServiceClient service = new NSNPServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Signup_Click(object sender, EventArgs e)
        {
            if(password.Value.Equals(password1.Value))
            {
                ourEntity entity = new ourEntity
                {
                    EMIS = entityno.Value,
                    S_Email = username.Value
                };
                if (service.RegisterEntity(entity, entityType.SelectedValue, Convert.ToString(password.Value)) == true)
                {
                    Session["Level"] = entityType.SelectedValue;
                    Session["Password"] = password.Value;
                    Session["Email"] = username.Value;
                    Response.Redirect("Profile.aspx");
                }
                else
                {
                    ErrorAlert.Attributes.Add("class", "alert alert-danger show-alert");
                }
            }
            else
            {
                Alert1.Attributes.Add("class", "alert alert-danger show-alert");
                password.Value = "";
                password1.Value = "";
            }
            
        }
        private void ErrorMessage(string message)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox", "<script language='javascript'>alert('" + message + "');</script>");
        }
    }
}