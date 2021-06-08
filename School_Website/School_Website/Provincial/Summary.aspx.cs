using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;

namespace School_Website.Provincial
{
    public partial class Summary : System.Web.UI.Page
    {
        private readonly NSNPServiceClient service = new NSNPServiceClient();
        private dynamic reports = null;
        private string ProvinceNo = "";
        private bool CheckLoggedIn()
        {
            return (Session["Email"] != null && Session["Password"] != null && Session["Level"] != null);              
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckLoggedIn())
                Response.Redirect("../Login.aspx");

            if (!Session["Level"].ToString().Equals("4"))
                Response.Redirect("../Home.aspx");

            int quarter = 1;
            int year = DateTime.Now.Year;
            if (Request.QueryString["quarter"] != null && Request.QueryString["year"] != null)
            {
                quarter = Convert.ToInt32(Request.QueryString["quarter"].ToString());
                year = Convert.ToInt32(Request.QueryString["year"].ToString());
            }
            ProvinceNo = service.GetProvince(Session["Email"].ToString(), Session["Password"].ToString()).P_No;
            displayMonth.InnerText = Convert.ToString(quarter);
            if (!IsPostBack)
            {
                if (!Session["Level"].ToString().Equals("4"))
                    Response.Redirect("../Home.aspx");
                //Sending zero values at the start only...
                reports = service.ExtendedQuarterReport(ProvinceNo, "0", "0", "0", quarter, year);
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
                reports = service.ExtendedQuarterReport(ProvinceNo, value, value1, value2, quarter, year);
                ListDistricts();
                DisplayData();
            }
        }
        public void ListDistricts()
        {
            string districts = specify_districts.SelectedValue;
            string circuits = specify_circuits.SelectedValue;
            string schools = specify_schools.SelectedValue;

            Circuits(districts);
            dynamic dbDistricts = service.GetDistrictsByProvince(ProvinceNo);
            specify_districts.Items.Clear();
            specify_districts.Items.Add(new ListItem("All Districts", "0"));
            foreach (var d in dbDistricts)
            {
                specify_districts.Items.Add(new ListItem(d.D_Name, d.D_No));
            }
            specify_districts.Text = districts ?? "0";

        }
        private void DisplayData()
        { 
            string display = "";
            ReportsSummary.InnerHtml = "";
            if (reports == null) return;
            double tempTotalLearners = 0, tempTotalAvgLearners = 0, tempTotalAllocated = 0, tempTotalExpenses = 0, tempTotalBudget = 0;
            foreach (TempReport tempReport in reports)
            {       
                display += $"<tr id={tempReport.TempAreaNumber}>";
                display += $"<td>{tempReport.TempSchoolEMIS}</td>";
                display += $"<td title={tempReport.TempSchoolEMIS}>{tempReport.TempSchoolName}</td>";
                display += $"<td>{tempReport.TempNumLearners}</td>";
                display += $"<td>{Math.Round(tempReport.TempAvgLearners, 2)}</td>";
                display += $"<td>{tempReport.TempAllocated}</td>";
                display += $"<td>{tempReport.TempExpenses}</td>";
                display += $"<td>{tempReport.TempBudget}</td>";
                display += "</tr>";
                tempTotalLearners += Convert.ToDouble(tempReport.TempNumLearners);
                tempTotalAvgLearners += Convert.ToDouble(tempReport.TempAvgLearners);
                tempTotalAllocated += Convert.ToDouble(tempReport.TempAllocated);
                tempTotalExpenses += Convert.ToDouble(tempReport.TempExpenses);
                tempTotalBudget += Convert.ToDouble(tempReport.TempBudget);
                ReportsSummary.InnerHtml = display;
            }
            display += $"<tr>";
            display += $"<td class='font-weight-bold'>Total:</td>";
            display += $"<td></td>";
            display += $"<td class='font-weight-bold'>{tempTotalLearners}</td>";
            display += $"<td class='font-weight-bold'>{tempTotalAvgLearners}</td>";
            display += $"<td class='font-weight-bold'>{tempTotalAllocated}</td>";
            display += $"<td class='font-weight-bold'>{tempTotalExpenses}</td>";
            display += $"<td class='font-weight-bold'>{tempTotalBudget}</td>";
            display += "</tr>";
            ReportsSummary.InnerHtml = display;
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