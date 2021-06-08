using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;
namespace School_Website
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        NSNPServiceClient service = new NSNPServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            HttpCookie userInfo = new HttpCookie("userInfo");
            string Password = RandomString();
            userInfo["tempPassword"] = Password;
            userInfo["email"] = username.Value;
            
            userInfo.Expires.Add(new TimeSpan(0, 1, 0));
            Response.Cookies.Add(userInfo);

            if (!username.Value.Equals("") && true)
            {
                Session["RequestPassword"] = true;
                string subject = "Reset Password Request";
                string message = "Hey Admin, \nPlease make use of this password within an hour \nPassword: " + Password;
                SendEmail query = new SendEmail(username.Value, subject, message);
                query.sendToClient();
                Response.Redirect("ResetPassword.aspx");
            }
            Console.Write(Password);
        }
        private string RandomString()
        {
            StringBuilder password = new StringBuilder();
            Random random = new Random();
            char temp;
            for (int i = 0; i < 15; i++)
            {
                temp = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                password.Append(temp);
            }
            return password.ToString();
        }
    }
}