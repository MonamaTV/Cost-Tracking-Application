using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;
namespace School_Website.District
{
    public partial class Summary : System.Web.UI.Page
    {
        NSNPServiceClient service = new NSNPServiceClient();
        dynamic reports = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckLoggedIn())
                Response.Redirect("../Login.aspx");

            if(!Session["Level"].ToString().Equals("3"))
                Response.Redirect("../Home.aspx");


            int quarter = 1;
            int year = DateTime.Now.Year;
            if (Request.QueryString["quarter"] != null && Request.QueryString["year"] != null)
            {
                quarter = Convert.ToInt32(Request.QueryString["quarter"].ToString());
                year = Convert.ToInt32(Request.QueryString["year"].ToString());
            }
            displayMonth.InnerText = Convert.ToString(quarter);
            reports = service.GenerateDistrictQuarterReport(Session["Email"].ToString(), Session["Password"].ToString(), quarter, year) ?? null;
            if (reports == null)
            {
                noReports.InnerHtml = "There are currently no reports in your database";
                return;
            }
            DisplayData();
            listAreas();
        }
        private void DisplayData(string areaEmail = "")
        {
            string display = "";
            string anum = service.GetAreaNumber(areaEmail);

            foreach (TempReport tempReport in reports)
            {
                if (!areaEmail.Equals("") && !anum.Equals(tempReport.TempAreaNumber))
                    continue;
                //Filtering the schools by circuits
                display += $"<tr id={tempReport.TempAreaNumber}>";
                display += $"<td>{tempReport.TempSchoolEMIS}</td>";
                display += $"<td title={tempReport.TempSchoolEMIS}>{tempReport.TempSchoolName}</td>";
                display += $"<td>{tempReport.TempNumLearners}</td>";
                display += $"<td>{Math.Round(tempReport.TempAvgLearners, 2)}</td>";        
                display += $"<td>{tempReport.TempAllocated}</td>";
                display += $"<td>{tempReport.TempExpenses}</td>";
                display += $"<td>{tempReport.TempBudget}</td>";
                display += "</tr>";
                ReportsSummary.InnerHtml = display;
            }
        }
        private void listAreas()
        {
            dynamic areas = service.GetAreas(Session["Email"].ToString(), Session["Password"].ToString());
            if (areas == null) return;

            if (specify_area.Items.Count > 0) return;
            specify_area.Items.Clear();
            specify_area.Items.Add(new ListItem("All Circuits", ""));
            foreach (AreaOffice area in areas)
            {
                ListItem item = new ListItem(area.A_Name, area.A_Email);
                specify_area.Items.Add(item);
            }
        }
        private bool CheckLoggedIn()
        {
            if (Session["Email"] != null && Session["Password"] != null && Session["Level"] != null)
                return true;
            else return false;
        }

        protected void specify_area_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportsSummary.InnerHtml = "";
            DisplayData(specify_area.SelectedValue);
        }
    }
}