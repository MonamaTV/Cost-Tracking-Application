using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;
namespace School_Website.Admin
{
    public partial class Details : System.Web.UI.Page
    {
        private readonly NSNPServiceClient service = new NSNPServiceClient();
        private dynamic reports = null;
        private string provinceNo = "";
        private bool CheckLoggedIn()
        {
            if (Session["Email"] != null && Session["Password"] != null && Session["Level"] != null)
                return true;
            else return false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckLoggedIn())
                Response.Redirect("../Login.aspx");

            if (!Session["Level"].ToString().Equals("5"))
                Response.Redirect("../Home.aspx");

            if (Request.QueryString["province"] == null)
                Response.Redirect("../Home.aspx");

            provinceNo = Request.QueryString["province"].ToString();
            string month = Convert.ToString(DateTime.Now.Month);
            
            int year = DateTime.Now.Year;
            if (Request.QueryString["month"] != null || Request.QueryString["year"] != null)
            {
                month = Convert.ToString(Request.QueryString["month"].ToString());
                year = Convert.ToInt32(Request.QueryString["year"].ToString());
            }
            displayMonth.InnerHtml = GetMonth(month);
            if (!IsPostBack)
            {
                reports = service.ExtendedDistrictReport(provinceNo, "0", "0", "0", year, month);
                ListDistricts();
                DisplayData();
            }
            else
            {
                string value = specify_districts.SelectedValue;
                string value1 = specify_circuits.SelectedValue;
                string value2 = specify_schools.SelectedValue;
                if (value1.Equals("0"))
                    value2 = "0";
                reports = service.ExtendedDistrictReport(provinceNo, value, value1, value2, year, month);
                ListDistricts();
                DisplayData();
            }
        }
        private void DisplayData()
        {
            ReportsSummary.InnerHtml = "";
            string display = "";

            double TempNumLearners = 0, TempAvgBudget = 0, TempAvgLearners = 0, TempExpenses = 0, TempAllocated = 0, TempBudget = 0;
            if (reports == null) return;
            foreach (TempReport tempReport in reports)
            {
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


        private void ListDistricts()
        {
            string districts = specify_districts.SelectedValue;
            string circuits = specify_circuits.SelectedValue;
            string schools = specify_schools.SelectedValue;
            if (reports == null) return;
            Circuits(districts);
            dynamic dbDistricts = service.GetDistrictsByProvince(provinceNo);
            specify_districts.Items.Clear();
            specify_districts.Items.Add(new ListItem("All Districts", "0"));
            foreach (var d in dbDistricts)
            {
                specify_districts.Items.Add(new ListItem(d.D_Name, d.D_No));
            }
            specify_districts.Text = districts ?? "0";

        }
        private void Schools(string area)
        {
            if (area.Equals("0"))
            {
                specify_schools.Items.Clear();
                specify_schools.Items.Add(new ListItem("All Schools", "0"));
                return;
            }
            dynamic dbSchools = service.GetSchoolsByCircuit(area);
            if (dbSchools == null) return;
            string schools = specify_schools.SelectedValue;
            specify_schools.Items.Clear();

            specify_schools.Items.Add(new ListItem("All Schools", "0"));
            foreach (var d in dbSchools)
            {
                specify_schools.Items.Add(new ListItem(d.S_Name, d.EMIS));
            }
            if (schools.Equals(""))
                specify_schools.Text = "0";
            else specify_schools.Text = schools;

        }
        private void Circuits(string district)
        {
            if (district.Equals("0"))
            {
                specify_schools.Items.Clear();
                specify_schools.Items.Add(new ListItem("All Schools", "0"));
                specify_circuits.Items.Clear();
                specify_circuits.Items.Add(new ListItem("All Circuits", "0"));
                return;
            }
            dynamic dbCircuits = service.GetCircuitsByDistricts(district) ?? null;
            if (dbCircuits == null) return;
            string circuits = specify_circuits.SelectedValue;
            Schools(circuits);
            specify_circuits.Items.Clear();
            specify_circuits.Items.Add(new ListItem("All Circuits", "0"));
            foreach (var d in dbCircuits)
            {
                specify_circuits.Items.Add(new ListItem(d.A_Name, d.A_No));
            }
            if (specify_circuits.Items.Count == 1)
            {
                specify_circuits.Text = "0";
                return;
            }

            if (circuits.Equals(""))
                specify_circuits.Text = "0";
            else specify_circuits.Text = circuits;

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

        protected void specify_districts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void specify_circuits_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void specify_schools_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}