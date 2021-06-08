using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;
namespace School_Website.Admin
{
    public partial class Provincial : System.Web.UI.Page
    {
        readonly NSNPServiceClient service = new NSNPServiceClient();
        private bool CheckLoggedIn()
        {
            if (Session["Email"] != null && Session["Password"] != null && Session["Level"] != null)
                return true;
            else return false;
        }
        
        private void DisplayProvincialReports(int Sort = 0)
        {
            if (Request.QueryString["province"] == null) return;

            string display = "";
            string[] months = new string[12];

            var prov =  service.GetProvinceByID(Request.QueryString["province"].ToString());

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
                display += $"<a href = 'details.aspx?month={mon}&year={SetYear(Sort)}&province={prov.P_No}' class='cardLink' title='Click here for {Months(i - 1)} reports info'>";
                display += "<div class='our-card'>";
                display += "<div class='our-card-date'>";
                display += $"<h3> {SetYear(Sort)}</h3>";
                display += $"<h3>{Months(i - 1)}</h3>";
                display += "</div>";
                display += "<div class='our-card-info'>";
                display += $"<h4>Province: {prov.P_Name}</h4>";
                display += "</div>";
                display += "</div>";
                display += "</a>";
                display += "</div>";
                if (i == 3 || i == 6 || i == 9 || i == 12)
                {
                    display += "<div class='col-md-3'>";
                    display += $"<a href = 'summary.aspx?quarter={i / 3}&year={SetYear(Sort)}&province={prov.P_No}' class='cardLink' title='Click here for {Months(i - 1)} reports info'>";
                    display += "<div class='our-card'>";
                    display += "<div class='our-card-date'>";
                    display += $"<h3> {SetYear(Sort)}</h3>";
                    display += $"<h3>{"Quarter" + i / 3}</h3>";
                    display += "</div>";
                    display += "<div class='our-card-info'>";
                    display += $"<h4>Province: {prov.P_Name}</h4>";
                    display += "</div>";
                    display += "</div>";
                    display += "</a>";
                    display += "</div>";
                }
            }
            cardsData.InnerHtml = display;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckLoggedIn())
                Response.Redirect("../Login.aspx");

            

            if (!Session["Level"].ToString().Equals("5")) Response.Redirect("../Home.aspx"); ;
            if(!IsPostBack)
            {
                ItemsForArea();
                DisplayProvincialReports();
            }

        }
        private void ItemsForArea()
        {
            int CurrentYear = DateTime.Now.Year;
            sortby.Items.Clear();
            ListItem Year1 = new ListItem(Convert.ToString(CurrentYear), "1");
            ListItem Year2 = new ListItem(Convert.ToString(CurrentYear - 1), "2");
            ListItem Year3 = new ListItem(Convert.ToString(CurrentYear - 2), "3");
            sortby.Items.Add(Year1);
            sortby.Items.Add(Year2);
            sortby.Items.Add(Year3);
        }

        private string SetYear(int value) => sortby.Items[value].Text;
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

            for (int i = 0; i <= 8; i++)
            {
                months[i] = GetMonth(Convert.ToString(4 + i));
            }
            months[9] = "January";
            months[10] = "February";
            months[11] = "March";
            return months[index];
        }

        protected void sortby_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayProvincialReports(sortby.SelectedIndex);
        }
    }
}