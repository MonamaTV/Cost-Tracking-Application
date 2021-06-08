using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;

namespace School_Website
{
    public partial class Home : System.Web.UI.Page
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

            if (Request.QueryString["Delete"] != null && Request.QueryString["deleteID"] != null)
            {
                try
                {
                    int ID = Convert.ToInt32(Request.QueryString["deleteID"].ToString());
                    if (service.VerifyReportAuth(ID, Session["Email"].ToString(), Session["Password"].ToString(), Session["Level"].ToString()))
                    {
                        string name = service.DeleteSchoolReport(Session["Email"].ToString(), Session["Password"].ToString(), ID);
                        if(name.Length > 0)
                        {
                            string dir = Server.MapPath("~/data/");
                            string path = dir + name;
                            FileInfo file = new FileInfo(path);
                            if(file.Exists)
                            {
                                file.Delete();
                            }
                        }
                    }
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee);
                    Response.Redirect("Home.aspx");
                }            
            }
            if (!IsPostBack)
            {
                if (Session["Email"] != null && Session["Password"] != null)
                {
                    if (service.LoginEntity(Session["Email"].ToString(), Session["Password"].ToString()))
                    {
                        if (Session["Level"].ToString().Equals("1"))
                        {
                            DisplayReports(1);
                        }
                        else if (Session["Level"].ToString().Equals("2"))
                        {
                            ItemsForArea();
                            DisplayAreaReports();
                        }
                        else if (Session["Level"].ToString().Equals("3"))
                        {
                            ItemsForArea();
                            DisplayDistrictReports();
                        }
                        else if (Session["Level"].ToString().Equals("4"))
                        {
                            ItemsForArea();
                            DisplayProvincialReports();
                        }
                        else if(Session["Level"].ToString().Equals("5"))
                        {
                            sortby.Visible = false;
                            DisplayAllProvinces();
                        }
                    }
                }
                
            }
        }
        private void DisplayAllProvinces()
        {
            dynamic provinces = service.GetAllProvinces();//Get all the provinces
            if (provinces == null) return;

            string display = "";
            foreach(var prov in provinces)
            {
                display += "<div class='col-md-4'>";
                display += $"<a href = 'admin/provincial.aspx?province={prov.P_No}' class='cardLink' title='Click here for reports info'>";
                display += "<div class='our-card'>";
                display += "<div class='our-card-date'>";
                display += $"<h3 class='mt-2'>Province: {prov.P_Name} </h3>";
                display += "</div>";
                display += "<div class='our-card-info'>";
                display += $"<h4 class='mt-2'>Manager: { prov.p_Manageer}</h4>";
                display += "</div>";
                display += "</div>";
                display += "</a>";
                display += "</div>";
            }
            cardsData.InnerHtml = display;

        }
        private void DisplayProvincialReports(int Sort = 0)
        {
            string display = "";
            string[] months = new string[12];
            string name = service.GetProvince(Session["Email"].ToString(), Session["Password"].ToString()).P_Name;
            int mon = 1;
            for (int i = 1; i <= 12; i++)
            {
                if (i <= 9) 
                {
                    mon = i + 3;
                }
                if (i > 9)
                {
                    mon = i - 9;
                }
                display += "<div class='col-md-3'>";
                display += $"<a href = 'provincial/details.aspx?month={mon}&year={SetYear(Sort)}' class='cardLink' title='Click here for {Months(i - 1)} reports info'>";
                display += "<div class='our-card'>";
                display += "<div class='our-card-date'>";
                display += $"<h3> {SetYear(Sort)}</h3>";
                display += $"<h3>{Months(i - 1)}</h3>";
                display += "</div>";
                display += "<div class='our-card-info'>";
                display += $"<h4>Province: {name}</h4>";
                display += "</div>";
                display += "</div>";
                display += "</a>";
                display += "</div>";
                if (i == 3 || i == 6 || i == 9 || i == 12)
                {
                    display += "<div class='col-md-3'>";
                    display += $"<a href = 'provincial/summary.aspx?quarter={i / 3}&year={SetYear(Sort)}' class='cardLink' title='Click here for {Months(i - 1)} reports info'>";
                    display += "<div class='our-card'>";
                    display += "<div class='our-card-date'>";
                    display += $"<h3> {SetYear(Sort)}</h3>";
                    display += $"<h3>{"Quarter" + i / 3}</h3>";
                    display += "</div>";
                    display += "<div class='our-card-info'>";
                    display += $"<h4>Province: {name}</h4>";
                    display += "</div>";
                    display += "</div>";
                    display += "</a>";
                    display += "</div>";
                }
            }
            cardsData.InnerHtml = display;
        }
        private void DisplayDistrictReports(int Sort = 0)
        {
            string display = "";
            string[] months = new string[12];

            string DistrictName = service.DisplayDistrictName(Session["Email"].ToString(), Session["Password"].ToString());
            int mon = 1;
            for (int i = 1; i <= 12; i++)
            {  
                if(i <= 9)
                {
                    mon = i + 3;
                }
                if(i > 9)
                {
                    mon = i - 9;
                }
                display += "<div class='col-md-3'>";
                display += $"<a href = 'district/details.aspx?month={mon}&year={SetYear(Sort)}' class='cardLink' title='Click here for {Months(i-1)} reports info'>";
                display += "<div class='our-card'>";
                display += "<div class='our-card-date'>";
                display += $"<h3> {SetYear(Sort)}</h3>";
                display += $"<h3>{Months(i - 1)}</h3>";
                display += "</div>";
                display += "<div class='our-card-info'>";
                display += $"<h4>Distict: {DistrictName}</h4>";
                display += "</div>";
                display += "</div>";
                display += "</a>";
                display += "</div>";
                if (i == 3 || i == 6 || i == 9 || i == 12)
                {
                    display += "<div class='col-md-3'>";
                    display += $"<a href = 'district/summary.aspx?quarter={i/3}&year={SetYear(Sort)}' class='cardLink' title='Click here for {Months(i - 1)} reports info'>";
                    display += "<div class='our-card'>";
                    display += "<div class='our-card-date'>";
                    display += $"<h3> {SetYear(Sort)}</h3>";
                    display += $"<h3>{"Quarter" + i/3}</h3>";
                    display += "</div>";
                    display += "<div class='our-card-info'>";
                    display += $"<h4>Distict: {DistrictName}</h4>";                   
                    display += "</div>";
                    display += "</div>";
                    display += "</a>";
                    display += "</div>";
                }
            }
            cardsData.InnerHtml = display;
        }
        private void ItemsForArea()
        {
            int CurrentYear = DateTime.Now.Year;
            sortby.Items.Clear();
            ListItem Year1 = new ListItem(Convert.ToString(CurrentYear), "1");
            ListItem Year2 = new ListItem(Convert.ToString(CurrentYear-1), "2");
            ListItem Year3 = new ListItem(Convert.ToString(CurrentYear-2), "3");
            sortby.Items.Add(Year1);
            sortby.Items.Add(Year2);
            sortby.Items.Add(Year3);
        }
        private string SetYear(int value) => sortby.Items[value].Text;
        
        // Level 2
        private void DisplayAreaReports(int Sort = 0)
        {          
            string display = "";
            string DistrictName = service.GetDistrictName(Session["Email"].ToString(), Session["Password"].ToString());
            string AreaName = service.GetArea(Session["Email"].ToString(), Session["Password"].ToString()).A_Name;
            for (int i = 1; i <= 12; i++)
            {
                display += "<div class='col-md-3'>";
                display += $"<a href = 'circuit/details.aspx?month={i}&year={SetYear(Sort)}' class='cardLink' title='Click here for {GetMonth(Convert.ToString(i))} reports info'>";
                display += "<div class='our-card'>";
                display += "<div class='our-card-date'>";
                display += $"<h3> {SetYear(Sort)}</h3>";
                display += $"<h3>{GetMonth(Convert.ToString(i))}</h3>";
                display += "</div>";
                display += "<div class='our-card-info'>";
                display += $"<h4>Distict: {DistrictName}</h4>";
                display += $"<h4>SD: {AreaName}</h4>";
                display += "</div>";
                display += "</div>";
                display += "</a>";
                display += "</div>";
            }
            cardsData.InnerHtml = display;
        }
        private void DisplayReports(int sortby)
        {       
            dynamic reports = service.GetSchoolReports(service.GetEMIS(Session["Email"].ToString(), Session["Password"].ToString()), sortby) ?? null;
            string display = "";
            if(reports != null)
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
        private string Months(int index = 0)
        {
            string[] months = new string[12];

            for(int i = 0; i <= 8; i++)
            {
                months[i] = GetMonth(Convert.ToString(4+i));
            }
            months[9] = "January";
            months[10] = "February";
            months[11] = "March";
            return months[index];
        }
        protected void sortby_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Session["Level"].ToString().Equals("1"))
            {
                DisplayReports(sortby.SelectedIndex);
            }
            if(Session["Level"].ToString().Equals("2"))
            {
                DisplayAreaReports(sortby.SelectedIndex);
            }
            if (Session["Level"].ToString().Equals("3"))
            {
                DisplayDistrictReports(sortby.SelectedIndex);
            }
            if (Session["Level"].ToString().Equals("4"))
            {
                DisplayProvincialReports(sortby.SelectedIndex);
            }

        }
        
    }
    
}