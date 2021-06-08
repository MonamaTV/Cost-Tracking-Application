using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using School_Website.SchoolService;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Website
{
    public partial class Annoucement : System.Web.UI.Page
    {
        readonly NSNPServiceClient service = new NSNPServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckLoggedIn())
                Response.Redirect("Login.aspx");
            


            /*If the URL parameter contains the delete query*/
            if (Request.QueryString["delete"] != null)
            {
                if(!Session["Level"].ToString().Equals("1"))
                     service.DeleteMessage(Session["Email"].ToString(), Session["Password"].ToString(), Request.QueryString["delete"].ToString());
            }

            if (service.LoginEntity(Session["Email"].ToString(), Session["Password"].ToString()))
            {
                if (Session["Level"].ToString().Equals("1"))
                {
                    btnAnnoucement.Visible = false;
                    ViewMessages();
                }
                if (Session["Level"].ToString().Equals("2"))
                {
                    if (Request.QueryString["Sent"] != null)
                    {
                        ViewMessages();
                    }
                    if (Request.QueryString["Received"] != null)
                    {
                        ViewMessages(2);
                    }
                    else ViewMessages();
                }
                if (Session["Level"].ToString().Equals("3"))
                {
                    ViewMessages();
                }
            } 
        }
        private string MessageType(int type)
        {
            if (type <= 0) return "Important";
            string[] messages = {"Important", "Urgent", "Normal"};
            return messages[type - 1];
        }
        private string ClassType(int clss)
        {
            if (clss <= 0) return "info";
            string[] type = {"info", "danger", "warning"};
            return type[clss - 1];
        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
        private bool CheckLoggedIn()
        {
            if (Session["Email"] != null && Session["Password"] != null && Session["Level"] != null)
                return true;
            else return false;
        }
        private void ViewMessages(int received = 0)
        {
            string display = "";
            dynamic messages = null;

            if (Session["Level"].ToString().Equals("1"))
            {
                messages = service.GetSchoolMessages(Session["Email"].ToString(), Session["Password"].ToString()) ?? null;
            }

            if (Session["Level"].ToString().Equals("2"))
            {
                if (received == 2)
                {
                    messages = service.GetAreaMessages(Session["Email"].ToString(), Session["Password"].ToString()) ?? null;
                }
                else messages = service.GetSchoolMessages(Session["Email"].ToString(), Session["Password"].ToString()) ?? null;
            }

            if(Session["Level"].ToString().Equals("3"))
            {
                messages = service.GetAreaMessages(Session["Email"].ToString(), Session["Password"].ToString()) ?? null; 
            }

            if (messages != null)
            {
                foreach (var message in messages)
                {
                    display += "<div class='col-md-12'>";
                    display += "<div class='messagecard'>";
                    display += $"<h5>{message.Subject} <span class='badge badge-pill badge-{ClassType(Convert.ToInt32(message.Type))}'>{MessageType(Convert.ToInt32(message.Type))}</span>";
                    if((Session["Level"].ToString().Equals("2") && received != 2) || Session["Level"].ToString().Equals("3") )
                    {
                        //Option available to the admins of either the circuit(level 2) or the district(level 3)
                        display += $"<img src='../images/deleter.png' class='delete_message' onclick='deleteFunction({message.M_ID})'>";
                    }
                    display +="</h5>";
                    display += $"<small>{Convert.ToDateTime(message.Date).ToShortDateString()}</small>";
                    display += $"<p>{message.Message}</p>";
                    display += "<br>";
                    display += "<hr>";
                    display += "</div></div>";
                }
                MessagesCard.InnerHtml = display;
            }
            else
            {
                display += "<div class='col-md-12'>";
                display += "<h2 class='text-center'>There are no messages.</h2>";
                display += "</div>";
                MessagesCard.InnerHtml = display;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!CheckLoggedIn())
                Response.Redirect("Login.aspx");

            if (VerifyInput())
            {
                if (service.LoginEntity(Session["Email"].ToString(), Session["Password"].ToString()))
                {
                    try
                    {
                        SchoolMessage Message = new SchoolMessage()
                        {
                            Message = message.Value,
                            Subject = subject.Value,
                            Date = DateTime.Now,
                            Type = Convert.ToInt32(messagetype.SelectedValue)
                        };
                        if (service.AddSchoolMessage(Session["Email"].ToString(), Session["Password"].ToString(), Message))
                        {            
                            Response.Redirect("Annoucement.aspx");
                        }
                        else Response.Redirect("Annoucement.aspx");
                    }
                    catch (Exception i)
                    {
                        //Add error message to he console
                        Console.Write(i.StackTrace);
                    }

                }
            }
            else
            {
                Response.Redirect("Annoucement.aspx");
            }
            
        }
        private bool VerifyInput()
        {
            if (subject.Value.Equals(""))
                return false;
            if (message.Value.Equals(""))
                return false;
            if (messagetype.SelectedIndex < 0 || messagetype.SelectedIndex > 3)
                return false;
            return true;
        }
       
    }

}