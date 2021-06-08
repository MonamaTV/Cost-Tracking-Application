using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;

namespace School_Website
{
    public partial class Schools : System.Web.UI.Page
    {
        readonly NSNPServiceClient service = new NSNPServiceClient();
        private bool CheckLoggedIn()
        {
            if (Session["Email"] != null && Session["Password"] != null && Session["Level"] != null)
                return true;
            else return false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckLoggedIn())
                Response.Redirect("Login.aspx");

            
            if (Request.QueryString["schoolID"] != null)
            {
                string EMIS = Request.QueryString["schoolID"].ToString();
                if (service.DisconnectSchool(Session["Email"].ToString(), Session["Password"].ToString(), EMIS))
                {
                    Response.Redirect("Schools.aspx");
                }
                else
                {
                    Response.Redirect("Schools.aspx");
                }
            }
            if(Request.QueryString["circuitID"] != null)
            {
                string ID = Request.QueryString["circuitID"].ToString();
                if (service.DisconnectArea(Session["Email"].ToString(), Session["Password"].ToString(), ID))
                {
                    Response.Redirect("Schools.aspx");
                }
                else
                {
                    Response.Redirect("Schools.aspx");
                }
            }



            if(!IsPostBack)
            {
                if (Session["Level"].ToString().Equals("2"))
                {
                    DisplaySchools();
                }
                else if(Session["Level"].ToString().Equals("3"))
                {
                    DisplayAreas();
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }
        private void DisplayAreas()
        {
            dynamic areas = service.GetAreas(Session["Email"].ToString(), Session["Password"].ToString());
            string display = "";
            if(areas != null)
            {
                manager.InnerText = "Manager Name";
                enitityNo.InnerText = "Circuit No.";
                organization.InnerText = "Circuits";
                numLearners.Visible = false;
                int count = 1;
                foreach(AreaOffice area in areas)
                {
                    display += "<tr>";
                    display += $"<td>{count}</td>";
                    display += $"<td>{area.A_No}</td>";
                    display += $"<td>{area.A_Name}</td>";
                    display += $"<td>{area.A_Manager}</td>";
                    display += $"<td>{area.A_Tellphone}</td>";
                    display += $"<td>{area.A_Cellphone}</td>";
                    display += $"<td id='{area.A_Name}'>";

                    display += $"<a href='Schools.aspx?circuitID={area.A_No}' class='show disconnect' onclick='disconnectFunction({area.A_No}, {area.A_Name});'> Disconnect</a>";
                    display += "</td>";
                    display += "</tr>";
                    count++;
                }
                schoolsInfo.InnerHtml = display;
            }
            else schoolsInfo.InnerHtml = "<h2>No Circuits Under This Organization</h2>";
        }
        
        private void DisplaySchools()
        {
            string email = Session["Email"].ToString();
            dynamic schools = service.GetSchools(email);
            String display = "";
            if(schools != null)
            {
                int count = 1;
                foreach(School sc in schools)
                {
                    display += "<tr>";
                    display += $"<td>{count}</td>";
                    display += $"<td>{sc.EMIS}</td>";
                    display += $"<td>{sc.S_Name}</td>";
                    display += $"<td>{sc.S_PrincipalName}</td>";
                    display += $"<td>{sc.S_TellNumber}</td>";
                    display += $"<td>{sc.S_CellPhone}</td>";
                    display += $"<td>{sc.S_No_learners}</td>";
                    display += $"<td>";
                    display += $"<a href='AboutSchool.aspx?EMIS={sc.EMIS}' class='show'>Show</a>";
                    display += $"<a class='show disconnect' href='Schools.aspx?schoolID={sc.EMIS}'> Disconnect</a>";
                    display += "</td>";
                    display += "</tr>";
                    count++;
                }
                schoolsInfo.InnerHtml = display;
            }
            else schoolsInfo.InnerHtml = "<h2>No Schools Under This Organization</h2>";

        }
    }
}