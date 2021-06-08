using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;
namespace School_Website
{
    public partial class AboutSchool : System.Web.UI.Page
    {
        readonly NSNPServiceClient service = new NSNPServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckLoggedIn())
                Response.Redirect("Login.aspx");
            if (!Session["Level"].ToString().Equals("2") && !Session["Level"].ToString().Equals("3"))
                Response.Redirect("Home.aspx");

            SchoolInformation(1);
        }
        void SchoolInformation(int sortby = 1)
        {
            if(Request.QueryString["EMIS"] == null)
                Response.Redirect("Home.aspx");
            

            string EMIS = Request.QueryString["EMIS"].ToString();
            dynamic reports = service.GetSchoolReportsByAD(Session["Email"].ToString(), Session["Password"].ToString(), EMIS, sortby);
            string display = "";
            if (reports != null)
            {
                foreach (SchoolReport report in reports)
                {
                    display += "<div class='col-md-3'>";
                    display += "<div class='our-card'>";
                    display += "<div class='our-card-date'>";
                    display += $"<h3>{GetMonth(Convert.ToString(report.Month))}</h3>";
                    display += $"<h5>{Convert.ToDateTime(report.Submission_Date).ToShortDateString()}</h5>";
                    display += "</div>";
                    display += "<div class='our-card-info'>";
                    display += $"<h4>Balance: R{report.Balance}</h4>";
                    display += $"<h4> Expenditure: R{report.Expenditure}</h4>";
                    display += "</div>";
                    display += $"<a href='Details.aspx?ReportID={report.R_ID}'><i class='fas fa-share-square'></i> More Details</a>";
                    display += $"<span class='delete-record' onclick='deleteRecord({report.R_ID});'><img src='images/deleter.png'></span>";
                    display += "</div>";
                    display += "</div>";
                }
            }
            else
            {
                display += "<div class='col-md-12'>";
                display += "<br>";
                display += "<br>";
                display += "<br>";
                display += "<h2 class='text-center'>There are no reports.</h2>";
                display += "</div>";
            }
            cardsData.InnerHtml = display;
        }
        private bool CheckLoggedIn()
        {
            if (Session["Email"] != null && Session["Password"] != null && Session["Level"] != null)
                return true;
            else return false;
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
            return "Unknown";
        }

        protected void sortby_SelectedIndexChanged(object sender, EventArgs e)
        {
            SchoolInformation(Convert.ToInt32(sortby.SelectedValue));
        }
    }
}