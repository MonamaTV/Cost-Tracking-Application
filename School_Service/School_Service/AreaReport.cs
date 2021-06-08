using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Service
{
    public class AreaReport :  A_Report
    {
        public AreaReport()
        {
            Month = "0";
            Submission_Date = DateTime.Now;
            Allocation = 0;
            BankCharges = 0;
            Expenditure = 0;
            Balance = 0;
            Schools_Submitted = 0;
        }
    }
}