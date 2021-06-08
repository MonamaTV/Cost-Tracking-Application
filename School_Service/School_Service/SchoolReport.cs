using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Service
{
    public class SchoolReport : S_Report
    {
        
        public SchoolReport()
        {
            Greenies = 0;
            Honorarium = 0;
            Food = 0;
            Gas_Wood = 0;
            AvgLearnerFed = 0;
            BankCharges = 0;
            Allocation = 0;
            Expenditure = 0;
            Balance = 0;
            NumDays = 0;
            Submission_Date = DateTime.Now;
            ClosingBalance = 0;
            AvgBudget = 0;
            Code = "";
        }
    }
}