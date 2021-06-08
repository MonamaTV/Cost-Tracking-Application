using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using School_Website.SchoolService;

namespace School_Website
{
    public partial class Details : System.Web.UI.Page
    {
        readonly NSNPServiceClient service = new NSNPServiceClient();
        protected string Code = "";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!IsPostBack)
            {
                if (!CheckLoggedIn())
                    Response.Redirect("Login.aspx");

                if (!Session["Level"].ToString().Equals("1") && !Session["Level"].ToString().Equals("3"))
                    Response.Redirect("Home.aspx");


                if(Session["Level"].ToString().Equals("3"))
                {
                    Submit.Enabled = false;
                    Cancel.Enabled = false;
                    Submit.Visible = false;
                    Cancel.Visible = false;
                    downloadReport.Visible = true;
                }
                string reportID = Request.QueryString["ReportID"];
                if (reportID != null && !reportID.Equals(""))
                {
                    if (!service.VerifyReportAuth(Convert.ToInt32(reportID), Session["Email"].ToString(), Session["Password"].ToString(), Session["Level"].ToString()))
                        Response.Redirect("ErrorPage.aspx");
                    //Display the information of the report
                    SchoolReport report = service.GetSchoolReport(Convert.ToInt32(reportID));
                    titleRegister.InnerText = GetMonth(report.Month);
                    days.Value = Convert.ToString(report.NumDays);
                    allocated.Value = Convert.ToString(report.Allocation);
                    balance.Value = Convert.ToString(report.Balance);
                    food.Value = Convert.ToString(report.Food);
                    gas.Value = Convert.ToString(report.Gas_Wood);
                    total.Value = Convert.ToString(report.Expenditure);
                    charges.Value = Convert.ToString(report.BankCharges);
                    honorarium.Value = Convert.ToString(report.Honorarium);
                    veges.Value = Convert.ToString(report.Greenies);
                    submission.Value = Convert.ToString(Convert.ToDateTime(report.Submission_Date).ToShortDateString());
                    months.SelectedValue = report.Month;
                    avgLearners.Value = Convert.ToString(report.AvgLearnerFed);
                    closingBalance.Value = Convert.ToString(report.ClosingBalance);
                    avgBudget.Value = Convert.ToString(report.AvgBudget);
                    Session["Code"] = report.Code;
                    if(Session["Code"] != null && !Session["Code"].ToString().Equals(""))
                    {
                        try
                        {

                            FileInfos file = service.GetFileInformation(report.EMIS, Session["Code"].ToString());
                            downloadReport.Text = file.Name ?? "";
                        }
                        catch (Exception exce)
                        {
                            Console.Write(exce);
                            downloadReport.Text = "";
                        }
                       
                    }

                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {       
            if (VerifyInput())
            {
                
                try
                {
                    double balance = Convert.ToDouble(allocated.Value) - Convert.ToDouble(total.Value);
                    string rEMIS = service.GetSchoolReport(Convert.ToInt32(Request.QueryString["ReportID"].ToString())).EMIS;
                    SchoolReport report = new SchoolReport
                    {
                        AvgLearnerFed = Convert.ToInt32(avgLearners.Value),
                        Allocation = Convert.ToDouble(allocated.Value),
                        Balance = balance,
                        BankCharges = Convert.ToDouble(charges.Value),
                        Food = Convert.ToDouble(food.Value),
                        Gas_Wood = Convert.ToDouble(gas.Value),
                        Submission_Date = Convert.ToDateTime(submission.Value).Date,
                        Greenies = Convert.ToDouble(veges.Value),
                        Honorarium = Convert.ToDouble(honorarium.Value),
                        Month = months.SelectedValue,
                        Expenditure = Convert.ToDouble(total.Value),
                        NumDays = Convert.ToInt32(days.Value),
                        EMIS = rEMIS,
                        R_ID = Convert.ToInt32(Request.QueryString["ReportID"].ToString()),
                        AvgBudget = Convert.ToDouble(avgBudget.Value),
                        ClosingBalance = Convert.ToDouble(closingBalance.Value),
                        Code = Session["Code"].ToString() ?? ""
                    };
                    //Come here...
                    if (service.EditScholReport(report))
                    {
                        
                        Alert.Attributes.Add("class", "alert alert-success profile-alert show-alert");
                    }
                    else
                    {
                        
                        ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
                    }
                }
                catch (Exception i)
                {
                    Console.WriteLine(i);
                    Alert.InnerHtml = "";
                    ErrorAlert.InnerHtml = "";
                    ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
                }
                
            }
            else
            {
                ErrorMessage("Fill in every neccesary input, try again!");
            }
        }
        private bool VerifyInput()
        {
            if (allocated.Value.Equals(""))
                return false;
            if (charges.Value.Equals(""))
                return false;
            if (food.Value.Equals(""))
                return false;
            if (gas.Value.Equals(""))
                return false;
            if (submission.Value.Equals(""))
                return false;
            if (veges.Value.Equals(""))
                return false;
            if (honorarium.Value.Equals(""))
                return false;
            if (total.Value.Equals(""))
                return false;
            if (days.Value.Equals(""))
                return false;
            return true;
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
        private void ErrorMessage(string message)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox", "<script language='javascript'>alert('" + message + "');</script>");
        }

        private bool CheckLoggedIn()
        {
            if(Session["Email"] != null && Session["Password"] != null && Session["Level"] != null)
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
            return "";
        }

        protected void downloadReport_Click(object sender, EventArgs e)
        {
            if(Session["Level"].ToString().Equals("1") || Session["Level"].ToString().Equals("3"))
            {
                
                if (Session["Code"] == null || Session["Code"].ToString().Equals(""))
                    return;

                string rEMIS = service.GetSchoolReport(Convert.ToInt32(Request.QueryString["ReportID"].ToString())).EMIS;
                FileInfos file = service.GetFileInformation(rEMIS, Session["Code"].ToString());
                try
                {
                    if (file != null)
                    {
                        string filePath = $"data//{file.Name}";
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                        Response.TransmitFile(Server.MapPath(filePath));
                        Response.End();
                    }
                }
                catch(Exception ee)
                {
                    //Do what you do best... nothing
                    Console.Write(ee.Message);
                }
            }
        }

        protected void deleteReport_Click(object sender, EventArgs e)
        {
            
        }
    }
}