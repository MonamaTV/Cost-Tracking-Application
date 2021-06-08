using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using School_Website.SchoolService;

namespace School_Website
{
    public partial class Entry : System.Web.UI.Page
    {
        readonly NSNPServiceClient service = new NSNPServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            //Comeback here to upload a file with new name, EMIS_MONTH_YEAR_DAY.pdf
            string fileName = "";
            string dataType = "";
            double size = 0;
        //    fileName = reportsFiles.PostedFile.FileName.Replace(reportsFiles.PostedFile.FileName, "");
            if(reportsFiles.HasFile)
            {
                if ((reportsFiles.PostedFile != null) && (reportsFiles.PostedFile.ContentLength > 0))
                {
                    fileName = Path.GetFileName(reportsFiles.PostedFile.FileName);
                    size = reportsFiles.PostedFile.ContentLength;
                    dataType = reportsFiles.PostedFile.ContentType;

                    
                    //File.Copy(Path.GetFileName(reportsFiles.PostedFile.FileName), path, true);
                    string fn = System.IO.Path.GetFileName(reportsFiles.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("data") + "\\" + fn;
                    try
                    {
                        reportsFiles.PostedFile.SaveAs(SaveLocation);
                     
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.StackTrace);
                        Response.Redirect("Entry.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Entry.aspx");
                }
            }

            if (VerifyInput())
            {
                
                try
                {
                    double balance = Convert.ToDouble(allocated.Value) - Convert.ToDouble(total.Value);
                    FileInfos fileInfo = new FileInfos
                    {
                        ContentType = dataType,
                        ContentLength = size,
                        Name = fileName
                    };
                    SchoolReport report = new SchoolReport
                    {
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
                        AvgLearnerFed = Convert.ToInt32(avgLearners.Value),
                        AvgBudget = Convert.ToDouble(avgBudget.Value),
                        ClosingBalance = Convert.ToDouble(closingBalance.Value),
                        
                    };
                    if (service.AddSchoolReport(report, service.GetEMIS(Session["Email"].ToString(), Session["Password"].ToString()), fileInfo))
                    {
                        ErrorMessage("Successfully added your report");
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        ErrorMessage("Could not added your report. Try again");
                    }
                }
                catch (Exception i)
                {
                    Console.Clear();
                    Console.WriteLine(i);
                }
            }
            else
            {
                Button button = new Button();
                button.OnClientClick = "";

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
            string filePath = "data//Melvin.pdf";
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
            Response.TransmitFile(Server.MapPath(filePath));
            Response.End();
            Response.Redirect("Home.aspx");
        }
        private void ErrorMessage(string message)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "MessageBox", "<script language='javascript'>alert('" + message + "');</script>");
        }

       
    }
}