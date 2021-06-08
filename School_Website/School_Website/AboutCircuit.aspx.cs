using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;

namespace School_Website
{
    public partial class AboutCircuit : System.Web.UI.Page
    {
        readonly NSNPServiceClient service = new NSNPServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckLoggedIn())
                Response.Redirect("Home.aspx");

            if(Request.QueryString["ANO"] == null)
                Response.Redirect("Home.aspx");


            string areaNumber = Request.QueryString["ANO"].ToString();
            DisplayAreaInformation(areaNumber);
            DisplaySchoolsInformation(areaNumber);

        }
        private void DisplayAreaInformation(string areaNumber)
        {
            var office = service.GetArea(Session["Email"].ToString(), Session["Password"].ToString());
            string display = "<tr>";
            display += $"<td>{office.A_No}</td>";
            display += $"<td>{office.A_Name}</td>";
            display += $"<td>{office.A_Manager}</td>";
            display += $"<td>{office.A_Cellphone}</td>";
            display += $"<td>{office.A_Tellphone}</td>";
            display += $"<td>{office.A_NumSchools}</td>";
            display += $"</tr>";
            circuit.InnerHtml += display;
        }
        private void DisplaySchoolsInformation(string areaNumber)
        {
            HttpBrowserCapabilities browse = new HttpBrowserCapabilities();
            string browserName = browse.Browser;
            string browserVersion = browse.Version;
            bool mobileType = browse.IsMobileDevice;
            string mobileName = browse.MobileDeviceManufacturer;

            
            
        }
        
        private bool CheckLoggedIn()
        {
            if (Session["Email"] != null && Session["Password"] != null && Session["Level"] != null)
                return true;
            else return false;
        }
    }
}