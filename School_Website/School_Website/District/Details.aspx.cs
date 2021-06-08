using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;
namespace School_Website.District
{
    public partial class Details : System.Web.UI.Page
    {
        private readonly NSNPServiceClient service = new NSNPServiceClient();
        private dynamic reports = null;
      
        private bool CheckLoggedIn()
        {
            if (Session["Email"] != null && Session["Password"] != null && Session["Level"] != null)
                return true;
            else return false;
        }
        private void listSchools(string email)
        {
            
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckLoggedIn())
                Response.Redirect("../Login.aspx");

            if (!Session["Level"].ToString().Equals("3"))
                Response.Redirect("../Home.aspx");

            string month = Convert.ToString(DateTime.Now.Month);           
            int year = DateTime.Now.Year;
            if (Request.QueryString["month"] != null || Request.QueryString["year"] != null)
            {
                month = Convert.ToString(Request.QueryString["month"].ToString());
                year = Convert.ToInt32(Request.QueryString["year"].ToString());
            }
            displayMonth.InnerText = GetMonth(month);
            reports = service.GenerateDistrictReport(Session["Email"].ToString(), Session["Password"].ToString(), month, year) ?? null;
            if(reports == null)
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
            double TempNumLearners = 0, TempAvgBudget = 0, TempAvgLearners = 0, TempExpenses = 0, TempAllocated = 0, TempBudget = 0;

            foreach (TempReport tempReport in reports)
            {
                if (!areaEmail.Equals("") && !anum.Equals(tempReport.TempAreaNumber))
                    continue;
                //Used for filtering the schools by circuits... areas
                
                display += $"<tr id={tempReport.TempAreaNumber}>";
                display += $"<td title={tempReport.TempSchoolEMIS}><a href='../Aboutschool.aspx?EMIS={tempReport.TempSchoolEMIS}'>{tempReport.TempSchoolName}</a></td>";
                display += $"<td>{tempReport.TempNumLearners}</td>";
                display += $"<td>{tempReport.TempAvgBudget}</td>";
                display += $"<td>{tempReport.TempAvgLearners}</td>";
                display += $"<td>{tempReport.TempExpenses}</td>";
                display += $"<td>{tempReport.TempAllocated}</td>";
                display += $"<td>{tempReport.TempBudget}</td>";
                display += $"<td>{Math.Round(tempReport.TempMonthlySpent, 2) + "%"}</td>";
                display += $"<td>{Convert.ToDateTime(tempReport.TempDate).ToShortDateString()}</td>";
                display += $"<td>{tempReport.TempClosingBalance}</td>";
                display += $"<td>{Math.Round(tempReport.BalanceAMB, 2) + "%"}</td>";
                display += "</tr>";
               
                TempNumLearners += Convert.ToDouble(tempReport.TempNumLearners);
                TempAvgBudget += tempReport.TempAvgBudget;
                TempAvgLearners += tempReport.TempAvgLearners;
                TempExpenses += tempReport.TempExpenses;
                TempAllocated += tempReport.TempAllocated;
                TempBudget += tempReport.TempBudget;
                ReportsSummary.InnerHtml = display;
            }
            display += $"<tr>";
            display += $"<td class='font-weight-bold'>Total:</td>";
            display += $"<td class='font-weight-bold'>{TempNumLearners}</td>";
            display += $"<td class='font-weight-bold'>{TempAvgBudget}</td>";
            display += $"<td class='font-weight-bold'>{TempAvgLearners}</td>";
            display += $"<td class='font-weight-bold'>{TempExpenses}</td>";
            display += $"<td class='font-weight-bold'>{TempAllocated}</td>";
            display += $"<td class='font-weight-bold'>{TempBudget}</td>";
            display += $"<td></td>";
            display += $"<td></td>";
            display += $"<td></td>";
            display += $"<td></td>";
            display += "</tr>";
            ReportsSummary.InnerHtml = display;

        }

        protected void specify_area_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Displays the schools from the selected circuit
            listSchools(specify_area.SelectedValue);
            ReportsSummary.InnerHtml = "";
            DisplayData(specify_area.SelectedValue);
        }

        protected void specify_school_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private string GetMonth(string month)
        {
            if (month.Equals("1"))
                return "January";
            if (month.Equals("2"))
                return "February";
            if (month.Equals("3"))
                return "March";
            if (month.Equals("4"))
                return "April";
            if (month.Equals("5"))
                return "May";
            if (month.Equals("6"))
                return "June";
            if (month.Equals("7"))
                return "July";
            if (month.Equals("8"))
                return "August";
            if (month.Equals("9"))
                return "September";
            if (month.Equals("10"))
                return "October";
            if (month.Equals("11"))
                return "November";
            if (month.Equals("12"))
                return "December";
            return "";
        }
    }
}