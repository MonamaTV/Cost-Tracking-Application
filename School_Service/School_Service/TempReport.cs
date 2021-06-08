using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Service
{

    public class TempReport
    {
        public double TempFood { set; get; }
        public double TempCharges { set; get; }
        public double TempWood { set; get; }
        public double TempAvgLearners { set; get; }
        public double TempAllocated { set; get; }
        public double TempBalance { set; get; }
        public double TempExpenses { set; get; }
        public double TempVeges { set; get; }
        public double TempHonori { set; get; }
        public double TempDays { set; get; }
        public DateTime TempDate { set; get; }

        public double TempBudget { set; get; }

        public double TempBankBalance { set; get; }
        public double TempAvgBudget { set; get; }
        public double TempClosingBalance { set; get; }
        //School Information
        public string TempSchoolEMIS { set; get; }
        public string TempSchoolName { set; get; }
        public string TempSchoolTelephone { set; get; }
        public string TempSchoolCellphone { set; get; }
        public int TempSchoolType { set; get; }
        public string TempNumLearners { set; get; }

        public string TempAreaNumber { set; get; }

        public double TempMonthlySpent { set { TempMonthlySpent = value; } get { return CalculateMonthlyBudgetSpent(); } }
        public double BalanceAMB { set { BalanceAMB = value; } get { return CalculateBankBalanceAMB(); } }

        
        public TempReport()
        {
            TempAvgBudget = 0;
            TempNumLearners = "0";
            TempAreaNumber = "";
            TempSchoolType = 0;
            TempSchoolTelephone = "";
            TempSchoolName = "";
            TempSchoolEMIS = "";
            TempSchoolCellphone = "";
            TempClosingBalance = 0;
            TempCharges = 0;
            TempBalance = 0;
            TempAvgLearners = 0;
            TempAllocated = 0;
            TempDays = 0;
            TempExpenses = 0;
            TempFood = 0;
            TempHonori = 0;
            TempVeges = 0;
            TempWood = 0;
            TempDate = DateTime.Now;
            TempBudget = 0;
            
            TempBankBalance = 0;
        }
        public double CalculateMonthlyBudgetSpent()
        {
            return (TempExpenses / TempBudget) * 100;
        }
        private double CalculateBankBalanceAMB()
        {
            return (TempClosingBalance / TempAvgBudget) * 100;
        }

    }
}