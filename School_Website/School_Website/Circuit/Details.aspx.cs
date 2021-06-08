using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;

namespace School_Website.views
{
    public partial class Details : System.Web.UI.Page
    {
        private string Month = "";
        private string Year = "";
        private int tempAreaNo;
        readonly NSNPServiceClient service = new NSNPServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Accesible to logged in Level entities
            if (!CheckLoggedIn())
                Response.Redirect("../Login.aspx");
            if (!Session["Level"].ToString().Equals("2"))
                Response.Redirect("../Home.aspx");

            if (Request.QueryString["month"] != null && Request.QueryString["year"] != null)
            {
                Year = Request.QueryString["year"].ToString();
                Month = Request.QueryString["month"].ToString();
            }
            else
            {
                Month = Convert.ToString(DateTime.Now.Month);
                Year = Convert.ToString(DateTime.Now.Year);
            }
            //Calls the function for summary reports
            DisplaySummary();
            if (!IsPostBack)
            {
                var areaReport = service.GetAreaReport(Session["Email"].ToString(), Session["Password"].ToString(), Month, Convert.ToInt32(Year));
                if (areaReport != null)
                {
                    tempAreaNo = areaReport.R_ID;
                    allocated.Value = Convert.ToString(areaReport.Allocation);
                    expenditure.Value = Convert.ToString(areaReport.Expenditure);
                    balance.Value = Convert.ToString(areaReport.Balance);
                    areacharges.Value = Convert.ToString(areaReport.BankCharges);
                    //submission.Value = Convert.ToString(Convert.ToDateTime(areaReport.Submission_Date).Date);
                }           
            }    
        }
        private void DisplaySummary(int sort = 4)
        {
            var ReportInfo = service.GenerateAreaReport(Session["Email"].ToString(), Session["Password"].ToString(), Month, Convert.ToInt32(Year), sort);
            food.InnerText = "R" + Convert.ToString(ReportInfo.Food);
            veges.InnerText = "R" + Convert.ToString(ReportInfo.Greenies);
            wood.InnerText = "R" + Convert.ToString(ReportInfo.Gas_Wood);
            honorarium.InnerText = "R" + Convert.ToString(ReportInfo.Honorarium);
            charges.InnerText = "R" + Convert.ToString(ReportInfo.BankCharges);
            avgLearners.InnerText = Convert.ToString(ReportInfo.AvgLearnerFed);
            days.InnerText = Convert.ToString(ReportInfo.NumDays);
            Areaallocated.InnerText = "R" + Convert.ToString(ReportInfo.Allocation);
            Areabalance.InnerText = "R" + Convert.ToString(ReportInfo.Balance);
            total.InnerText = "R" + Convert.ToString(ReportInfo.Expenditure);
            areacharges.Value = Convert.ToString(ReportInfo.BankCharges);
            allocated.Value = Convert.ToString(ReportInfo.Allocation);
            expenditure.Value = Convert.ToString(ReportInfo.Expenditure);
            balance.Value = Convert.ToString(ReportInfo.Balance);

        }

        protected void Submit_Click(object sender, EventArgs e)
        {    

        try
        {
            AreaReport areaReport = new AreaReport()
            {
                Allocation = Convert.ToDouble(allocated.Value),
                Expenditure = Convert.ToDouble(expenditure.Value),
                Balance = Convert.ToDouble(balance.Value),
                BankCharges = Convert.ToDouble(areacharges.Value),
                Month = Month,
                //Submission_Date = Convert.ToDateTime(submission.Value),
                R_ID = tempAreaNo
            };
            if (service.AddAreaReport(areaReport, Session["Email"].ToString(), Session["Password"].ToString()))
            {
                Alert.Attributes.Add("class", "alert alert-success profile-alert show-alert");
            }
            else
            {
                ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
            }
        }
        catch(Exception u)
        {
            ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert");
            Console.Write(u.StackTrace);
        }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Home.aspx");
        }
        private bool CheckLoggedIn()
        {
            if (Session["Email"] != null && Session["Password"] != null && Session["Level"] != null)
                return true;
            else return false;
        }

        protected void schooltypes_SelectedIndexChanged(object sender, EventArgs e)
        {

            DisplaySummary(Convert.ToInt32(schooltypes.SelectedValue));
        }
    }
}