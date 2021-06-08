using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Website
{
    public partial class MainLayout : System.Web.UI.MasterPage  
    {
        protected void Page_Load(object sender, EventArgs e)
        {         
            if (Session["Password"] != null && Session["Email"] != null)
            {
                if (Session["Level"].ToString().Equals("1"))
                {
                    school.Visible = false;
                    drop_menu.Visible = false;
                    drop_icon.Visible = false;
                }
                else if(Session["Level"].ToString().Equals("2"))
                {
                    school.Visible = true;
                    entry.Visible = false;
                }
                else if(Session["Level"].ToString().Equals("3"))
                {
                    entry.Visible = false;
                    school.Visible = true;
                    drop_menu.Visible = false;
                    nameEnt.InnerText = "Circuits";
                }  
                else if(Session["Level"].ToString().Equals("4"))
                {
                    entry.Visible = false;
                    school.Visible = false;
                    drop_menu.Visible = false;

                }
                else if (Session["Level"].ToString().Equals("5"))
                {
                    profile.Visible = false;
                    entry.Visible = false;
                    school.Visible = false;
                    drop_menu.Visible = false;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        public void Reports()
        {
           profile.Attributes["Class"] = "active";
        }
       
    }
}