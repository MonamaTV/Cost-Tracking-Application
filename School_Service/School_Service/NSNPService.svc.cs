using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace School_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NSNPService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select NSNPService.svc or NSNPService.svc.cs at the Solution Explorer and start debugging.
    public class NSNPService : INSNPService
    {
        //The trick is, do NOT touch it if it works or you do not know what it does... spoiler alert, there is a lot of confusing code in here
      // NSNPDataDataContext db = new NSNPDataDataContext();
        DataConnectionDataContext db = new DataConnectionDataContext();
        public bool LoginEntity(string Email, string Password)
        {
            var user = (from u in db.Logins where 
                        u.password.Equals(Secrecy.HashPassword(Password))
                        && u.email.Equals(Email) select u).FirstOrDefault();

            if (user != null) return true;
            else return false;
        }
        public ourEntity GetSchool(string email, string password)
        {
            
            if(LoginEntity(email, password))
            {
                var entity = (from e in db.Schools where e.S_Email.Equals(email) select e).FirstOrDefault();
                if (entity != null)
                {
                    //ourEntity is a class from(inherits) School table in the database.
                    ourEntity ent = new ourEntity
                    {
                        EMIS = entity.EMIS,
                        S_Email = entity.S_Email,
                        S_Name = entity.S_Name,
                        S_Type = entity.S_Type,
                        S_No_learners = entity.S_No_learners,
                        S_PrincipalName = entity.S_PrincipalName,
                        S_CellPhone = entity.S_CellPhone,
                        S_TellNumber = entity.S_TellNumber,
                        S_Budget = entity.S_Budget,
                        A_No = entity.A_No,
                        P_No = entity.P_No
                          
                    };
                    return ent;
                }
            }
            return null;
        }
        public bool RegisterEntity(ourEntity entity, string Level, string Password)
        {
            Boolean state = false;
            Boolean traceLoginInsertion = false;
            School sc = null;
            AreaOffice areaOffice = null;
            District district = null;
            Provincial provincial = null;
                
            try
            {
                Login log = new Login
                {
                    Level_ID = Level,
                    email = entity.S_Email,
                    password = Secrecy.HashPassword(Password)
                };
                db.Logins.InsertOnSubmit(log);
                db.SubmitChanges();
                traceLoginInsertion = true;
                //If the above code crashes there is no success in the function
                if (Level.Equals("1"))
                {
                    sc = new School
                    {
                        EMIS = entity.EMIS,
                        S_Email = entity.S_Email
                    };
                    db.Schools.InsertOnSubmit(sc);
                    db.SubmitChanges();
                    state = true;
                }
                else if (Level.Equals("2"))
                {
                    areaOffice = new AreaOffice
                    {
                        A_Email = Convert.ToString(entity.S_Email),
                        A_No = entity.EMIS
                    };
                    db.AreaOffices.InsertOnSubmit(areaOffice);
                    db.SubmitChanges();
                    state = true;
                }
                else if (Level.Equals("3"))
                {
                    district = new District
                    {
                        D_Email = entity.S_Email,
                        D_No = entity.EMIS
                    };
                    db.Districts.InsertOnSubmit(district);
                    db.SubmitChanges();
                    state = true;
                }
                else if (Level.Equals("4"))
                {
                    provincial = new Provincial
                    {
                        P_Email = entity.S_Email,
                        P_No = entity.EMIS
                    };
                    db.Provincials.InsertOnSubmit(provincial);
                    db.SubmitChanges();
                    state = true;
                }
                
            }
            catch (Exception e)
            {
                //Delete the login entry made
                if(!traceLoginInsertion)
                {
                    var logins = db.Logins.SingleOrDefault(a => a.email == entity.S_Email);
                    db.Logins.DeleteOnSubmit(logins);
                    db.SubmitChanges();
                }
                PrintError(e);
                state = false;
            }
            return state;
        }
       
        public bool EditSchool(ourEntity school, string areaNumber, string provNo, string OldPassword, string NewPassword)
        {
            Boolean state = false;
            try
            {
                if(LoginEntity(school.S_Email, OldPassword))
                {
                    //Get credetials
                    var log = (from l in db.Logins
                               where l.email.Equals(school.S_Email) && l.password.Equals(Secrecy.HashPassword(OldPassword))
                               select l).FirstOrDefault();
                    //Get school to edit
                    var sc = (from s in db.Schools where s.S_Email.Equals(school.S_Email) select s).FirstOrDefault();
                    //Change the password
                    if (!log.password.Equals(Secrecy.HashPassword(NewPassword)))
                    {
                        log.password = Secrecy.HashPassword(NewPassword);
                        log.email = school.S_Email;
                        db.SubmitChanges();
                        state = true;
                    }
                    //Change School Details
                    sc.S_Email = school.S_Email;
                    sc.S_Name = school.S_Name;
                    sc.S_Type = school.S_Type;
                    sc.S_No_learners = school.S_No_learners;
                    sc.S_PrincipalName = school.S_PrincipalName;
                    sc.S_TellNumber = school.S_TellNumber;
                    sc.S_CellPhone = school.S_CellPhone;
                    sc.EMIS = school.EMIS;
                    sc.S_Budget = school.S_Budget;
                    if (areaNumber != null && !areaNumber.Equals(""))
                        sc.A_No = areaNumber;
                    else
                        sc.A_No = null;

                    if (provNo != null && !provNo.Equals(""))
                        sc.P_No = provNo;
                    else
                        sc.P_No = null;
                    db.SubmitChanges();
                    state = true;
                }
            }
            catch(Exception e)
            {
                Console.Clear();
                PrintError(e);
                state = false;
            }
            return state;
        }
        private int CountSchools(string AreaNumber)
        {
            int count = 0;
            try
            {
                count = (from s in db.Schools where s.A_No.Equals(AreaNumber) select s).Count();
            }
            catch(Exception e)
            {
                PrintError(e);
                count = -1;
            }
            return count;
        }
        private  int CountAreas(string DitrictNumber)
        {
            int count = 0;
            try
            {
                count = (from a in db.AreaOffices where a.D_No.Equals(DitrictNumber) select a).Count();
            }
            catch(Exception e)
            {
                PrintError(e);
                count = -1;
            }
            return count;
        }
        public bool EditArea(ourArea area, string DistrictNo, string newPassword, string oldPassword)
        {
            Boolean state = false;
            try
            {
                //The area exists
                if(LoginEntity(area.A_Email, oldPassword))
                {
                    var log = (from l in db.Logins
                               where l.email.Equals(area.A_Email) && l.password.Equals(Secrecy.HashPassword(oldPassword))
                               select l).FirstOrDefault();
                    //Get the area
                    var dbArea = db.AreaOffices.FirstOrDefault(a => a.A_No.Equals(area.A_No));
                    //Changed passwords?
                    if (!log.password.Equals(Secrecy.HashPassword(newPassword)))
                    {
                        log.password = Secrecy.HashPassword(newPassword);
                        log.email = area.A_Email;
                        db.SubmitChanges();
                        state = true;
                    }
                    //change the area details
                    dbArea.A_Name = area.A_Name;
                    dbArea.A_Email = area.A_Email;
                    dbArea.A_Cellphone = area.A_Cellphone;
                    dbArea.A_Manager = area.A_Manager;
                    dbArea.A_NumSchools = CountSchools(dbArea.A_No);
                    dbArea.A_Tellphone = area.A_Tellphone;
                    dbArea.A_No = area.A_No;
                    if (DistrictNo != null && !DistrictNo.Equals(""))
                        dbArea.D_No = DistrictNo;
                    db.SubmitChanges();
                    state = true;
                }
            }
            catch(Exception i)
            {
                PrintError(i);
                state = false;
            }
            return state;
        }
        public bool EditDistrict(ourDistrict district, string P_No, string newPassword, string oldPassword)
        {
            Boolean state = false;
            //checking if passwords are changed...
            try
            {
                if(LoginEntity(district.D_Email, oldPassword))
                {
                    var log = db.Logins.FirstOrDefault(a => a.email.Equals(district.D_Email) && a.password.Equals(Secrecy.HashPassword(oldPassword)));
                    //the district
                    var dbDistrict = (from dis in db.Districts where dis.D_No.Equals(district.D_No) select dis).FirstOrDefault();
                    //change the passwords
                    if (!log.password.Equals(Secrecy.HashPassword(newPassword)))
                    {
                        log.password = Secrecy.HashPassword(newPassword);
                        log.email = district.D_Email;
                        db.SubmitChanges();
                        state = true;
                    }
                    dbDistrict.D_Cellphone = district.D_Cellphone;
                    dbDistrict.D_Email = district.D_Email;
                    dbDistrict.D_Manager = district.D_Manager;
                    dbDistrict.D_Name = district.D_Name;
                    dbDistrict.D_Tellphone = district.D_Tellphone;
                    dbDistrict.D_No = district.D_No;
                    dbDistrict.D_NumAreas = CountAreas(district.D_No);
                    if (P_No != null && !P_No.Equals(""))
                        dbDistrict.P_No = P_No;
                    db.SubmitChanges();
                    state = true;
                }
            }
            catch(Exception e)
            {
                PrintError(e);
                state = false;
            }
            return state;
        }
        private void PrintError(Exception e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
        }

        public ourArea GetArea(string email, string password)
        {
            if(LoginEntity(email, password))
            {
                var dbArea = (from a in db.AreaOffices where a.A_Email.Equals(email) select a).FirstOrDefault();
                if(dbArea != null)
                {
                    ourArea area = new ourArea
                    {
                        A_Email = dbArea.A_Email,
                        A_No = dbArea.A_No,
                        A_Name = dbArea.A_Name,
                        A_Cellphone = dbArea.A_Cellphone,
                        A_Manager = dbArea.A_Manager,
                        A_NumSchools = CountSchools(dbArea.A_No),
                        A_Tellphone = dbArea.A_Tellphone,
                        D_No = dbArea.D_No                      
                    };
                    return area;
                }
            }
            return null;
        }

        public int GetLevel(string email, string password)
        {
            var log = (from u in db.Logins where u.email.Equals(email) && u.password.Equals(Secrecy.HashPassword(password)) select u).FirstOrDefault();
            if(log != null)
            {
                return Convert.ToInt32(log.Level_ID);
            }
            return -1;
        }

        public string GetEMIS(string email, string password)
        {
            if(LoginEntity(email, password))
            {
                var sc = (from s in db.Schools where s.S_Email.Equals(email) select s).FirstOrDefault();
                if(sc != null)
                {
                    return sc.EMIS;
                }
            }
            return "";
        }
        public ourProvincial GetProvince(string email, string password)
        {
            if(LoginEntity(email, password))
            {
                var dbProvince = db.Provincials.FirstOrDefault(p => p.P_Email == email);
                if (dbProvince == null) return null;

                return new ourProvincial()
                {
                    P_Email =  dbProvince.P_Email,
                    P_No = dbProvince.P_No,
                    P_Cellphone = dbProvince.P_Cellphone,
                    P_Name = dbProvince.P_Name,
                    P_Tellphone = dbProvince.P_Tellphone,
                    p_Manageer = dbProvince.p_Manageer,
                    Num_Areas = dbProvince.Num_Areas,
                    Num_Districts = dbProvince.Num_Districts
                };

            }
            return null;
        }

        public bool EditProvince(ourProvincial province, string newPassword, string oldPassword)
        {
            bool state = false;
            if (!LoginEntity(province.P_Email, oldPassword)) return false;    
            try
            {
                var dbProvince = db.Provincials.SingleOrDefault(p => p.P_Email.Equals(province.P_Email));
                var log = db.Logins.SingleOrDefault(l => l.email.Equals(province.P_Email));
                if (!log.password.Equals(Secrecy.HashPassword(newPassword)))
                {
                    log.password = Secrecy.HashPassword(newPassword);
                    log.email = province.P_Email;
                    db.SubmitChanges();
                    state = true;
                }
                dbProvince.P_Email = province.P_Email;
                dbProvince.p_Manageer = province.p_Manageer;
                dbProvince.P_Name = province.P_Name;
                dbProvince.P_Tellphone = province.P_Tellphone;
                dbProvince.P_Cellphone = province.P_Cellphone;
                dbProvince.Num_Areas = province.Num_Areas;
                dbProvince.Num_Districts = province.Num_Districts;
                db.SubmitChanges();
                state = true;
            }
            catch (Exception)
            {
                state = false;
            }
            return state;

        }

        public bool AddSchoolReport(SchoolReport report, string rEMIS, FileInfos file)
        {
            Boolean state = false;
            string UniqueCode = "";
            //Add the fileinfomation related to this report/entry
            if (file != null)
            {
                UniqueCode = Secrecy.HashPassword(file.ToString() + report.ToString()); //Does the code that works needs to be understood everytime... I think the books of software engineering might have missed this
                FileInformation fileInfo = new FileInformation
                {
                    EMIS = rEMIS,
                    Code = UniqueCode,
                    Name = file.Name,
                    ContentLength = file.ContentLength,
                    ContentType = file.ContentType,
                };                
                try
                {
                    db.FileInformations.InsertOnSubmit(fileInfo);
                    db.SubmitChanges();
                    state = true;
                }
                catch (Exception e)
                {
                    PrintError(e);
                    return false;
                }
            }

            S_Report sreport = new S_Report
            {
                EMIS = rEMIS,
                Allocation = report.Allocation,
                BankCharges = report.BankCharges,
                Expenditure = report.Expenditure,
                Food = report.Food,
                Gas_Wood = report.Gas_Wood,
                Greenies = report.Greenies,
                Honorarium = report.Honorarium,
                Month = report.Month,
                Submission_Date = report.Submission_Date,
                NumDays = report.NumDays,
                Balance = report.Balance,
                AvgLearnerFed = report.AvgLearnerFed,
                AvgBudget = report.AvgBudget,
                ClosingBalance = report.ClosingBalance,
                Code = UniqueCode
            };
            try
            {
                db.S_Reports.InsertOnSubmit(sreport);
                db.SubmitChanges();
                state = true;
            }
            catch(Exception e)
            {
                Console.Clear();
                PrintError(e);
            }
            return state;

        }


        public SchoolReport GetSchoolReport(int ID)
        {
            var report = (from r in db.S_Reports where r.R_ID.Equals(ID) select r).FirstOrDefault();

            if(report != null)
            {
                return new SchoolReport()
                {
                    R_ID = report.R_ID,
                    Allocation = report.Allocation,
                    Balance = report.Balance,
                    BankCharges = report.BankCharges,
                    EMIS = report.EMIS,
                    Expenditure = report.Expenditure,
                    Food = report.Food,
                    Gas_Wood = report.Gas_Wood,
                    Greenies = report.Greenies,
                    Honorarium = report.Honorarium,
                    Month = report.Month,
                    NumDays = report.NumDays,
                    Submission_Date = report.Submission_Date,
                    AvgLearnerFed = report.AvgLearnerFed,
                    ClosingBalance = report.ClosingBalance,
                    AvgBudget = report.AvgBudget,
                    Code = report.Code
                };
            }
            return null;
        }
        public List<SchoolReport> GetSchoolReports(string EMIS, int Sortby)
        {
            List<SchoolReport> reports = new List<SchoolReport>();
            dynamic dbreports = null;
            //Get the reports in the Sortby format
            if(Sortby == 1) //Month
                dbreports = (from r in db.S_Reports
                             where r.EMIS.Equals(EMIS) orderby 
                             Convert.ToInt32(r.Month) ascending
                             select r) ?? null;
            if(Sortby == 2) //Year
                dbreports = (from r in db.S_Reports
                             where r.EMIS.Equals(EMIS) orderby 
                             Convert.ToDateTime(r.Submission_Date).Year
                             ascending select r) ?? null;
            if(Sortby == 3) //Allocated asc
                dbreports = (from r in db.S_Reports
                             where r.EMIS.Equals(EMIS) orderby 
                             r.Allocation ascending select r) ?? null;
            if (Sortby == 4) //Allocated desc
                dbreports = (from r in db.S_Reports
                             where r.EMIS.Equals(EMIS) orderby 
                             r.Allocation descending select r) ?? null;
            if (Sortby == 5) //Balalance asc
                dbreports = (from r in db.S_Reports
                             where r.EMIS.Equals(EMIS) orderby 
                             r.Balance ascending select r) ?? null;
            if (Sortby == 6) //Balalnce desc
                dbreports = (from r in db.S_Reports
                             where r.EMIS.Equals(EMIS) orderby r.Balance 
                             descending select r) ?? null;
            if (Sortby == 7) //Expenditure asc
                dbreports = (from r in db.S_Reports
                             where r.EMIS.Equals(EMIS) orderby 
                             r.Expenditure ascending select r) ?? null;
            if (Sortby == 8) //Expenditure desc
                dbreports = (from r in db.S_Reports
                             where r.EMIS.Equals(EMIS) orderby
                             r.Expenditure descending select r) ?? null;

            if (dbreports != null)
            {
                foreach(S_Report scr in dbreports)
                {
                    reports.Add(new SchoolReport()
                    {
                        //Add all the information to this
                        EMIS = scr.EMIS,
                        Allocation = scr.Allocation,
                        Balance = scr.Balance,
                        BankCharges = scr.BankCharges,
                        Expenditure = scr.Expenditure,
                        Food = scr.Food,
                        Gas_Wood = scr.Gas_Wood,
                        Greenies = scr.Greenies,
                        Honorarium = scr.Honorarium,
                        Month = scr.Month,
                        NumDays = scr.NumDays,
                        Submission_Date = scr.Submission_Date,
                        R_ID = scr.R_ID,
                        AvgLearnerFed = scr.AvgLearnerFed,
                        AvgBudget = scr.AvgBudget,
                        ClosingBalance = scr.ClosingBalance
                    });
                }
                
            }
            return (reports.Count() > 0)? reports : null;
        }
        public string GetAreaNumber(string email)
        {
            var area = (from a in db.AreaOffices
                        where a.A_Email.Equals(email)
                        select a).FirstOrDefault();
            if(area != null)
            {
                return area.A_No;
            }
            return "";
        }
        
        public List<School> GetSchools(string email)
        {
            List<School> schools = new List<School>();
            dynamic dbschools = (from s in db.Schools
                                 where s.A_No.Equals(GetAreaNumber(email))
                                 select s);
            if(dbschools != null)
            {
                foreach(School sc in dbschools)
                {
                    schools.Add(new School()
                    {
                        A_No = sc.A_No,
                        EMIS = sc.EMIS,
                        S_CellPhone = sc.S_CellPhone,
                        S_Email = sc.S_Email,
                        S_Name = sc.S_Name,
                        S_No_learners = sc.S_No_learners,
                        S_PrincipalName = sc.S_PrincipalName,
                        S_TellNumber = sc.S_TellNumber,
                        S_Budget = sc.S_Budget
                    }) ;
                }
                return schools;
            }
            return null;
        }

        public bool EditScholReport(SchoolReport report)
        {
            Boolean state = false;
            var dbReport = db.S_Reports.FirstOrDefault(a => a.R_ID == report.R_ID);
            try
            {
                dbReport.Allocation = report.Allocation;
                dbReport.Balance = report.Balance;
                dbReport.BankCharges = report.BankCharges;
                dbReport.Food = report.Food;
                dbReport.Gas_Wood = report.Gas_Wood;
                dbReport.Greenies = report.Greenies;
                dbReport.Honorarium = report.Honorarium;
                dbReport.Month = report.Month;
                dbReport.NumDays = report.NumDays;
                dbReport.Submission_Date = report.Submission_Date;
                dbReport.Expenditure = report.Expenditure;
                dbReport.AvgLearnerFed = report.AvgLearnerFed;
                dbReport.ClosingBalance = report.ClosingBalance;
                
                dbReport.AvgBudget = report.AvgBudget;
                db.SubmitChanges();
                state = true;
            }
            catch(Exception e)
            {
                Console.Clear();
                PrintError(e);
                state = false;
            }
            return state;
        }
        public ourDistrict GetDistrict(string email, string password)
        {
            if(LoginEntity(email, password))
            {
                var dbDistrict = db.Districts.FirstOrDefault(d => d.D_Email.Equals(email));
                int ACount = (from a in db.AreaOffices where a.D_No.Equals(dbDistrict.D_No) select a).Count();
                return new ourDistrict()
                {
                    D_Email = dbDistrict.D_Email,
                    D_Cellphone = dbDistrict.D_Cellphone,
                    D_Manager = dbDistrict.D_Manager,
                    D_Name = dbDistrict.D_Name,
                    D_NumAreas = ACount,
                    D_No = dbDistrict.D_No,
                    D_Tellphone = dbDistrict.D_Tellphone,
                };
            }
            return null;
        }
        public List<SchoolMessage> GetSchoolMessages(string email, string password)
        {
            dynamic dbmessages = null;
            if(GetLevel(email, password).Equals(1))
            {
                var tempA_No = GetSchool(email, password).A_No;
                dbmessages = (from m in db.AreaSchoolMessages
                              where m.A_No.Equals(tempA_No)
                              orderby m.Date descending select m);
            }
            if(GetLevel(email, password).Equals(2))
            {
                dbmessages = (from m in db.AreaSchoolMessages
                              where m.A_No.Equals(GetAreaNumber(email))
                              orderby m.Date descending select m);
            }

            List<SchoolMessage> messages = new List<SchoolMessage>();
            if (dbmessages != null)
            {
                foreach (AreaSchoolMessage m in dbmessages)
                {
                    messages.Add(new SchoolMessage()
                    {
                        Message = m.Message,
                        Subject = m.Subject,
                        Date = m.Date,
                        Type = m.Type,
                        M_ID = m.M_ID
                    });
                }
            }
            return (messages.Count > 0) ? messages : null;

        }
        private int CountReportsSubmitted(string Email, string Month, int Year)
        {
            return (from r in db.S_Reports
                               join s in db.Schools on
                               r.EMIS equals s.EMIS
                               where s.A_No == GetAreaNumber(Email) && r.Month == Month &&
                               Convert.ToDateTime(r.Submission_Date).Year == Year
                               select r).Count();
        }
        /*
         * Areas have their own reports, how much was allocatedh to them and balance.
         */
        public AreaReport GetAreaReport(string Email, string Password, string Month, int Year)
        {
            if(LoginEntity(Email, Password))
            {
                var Report = (from r in db.A_Reports where 
                              r.Month == Month && 
                              Convert.ToDateTime(r.Submission_Date).Year == Year && r.A_No == GetAreaNumber(Email)
                              select r).FirstOrDefault() ?? null;
               
                if(Report != null)
                {
                    return new AreaReport()
                    {
                        Submission_Date = Report.Submission_Date,
                        Allocation = Report.Allocation,
                        Balance = Report.Balance,
                        BankCharges = Report.BankCharges,
                        Expenditure = Report.Expenditure,
                        Month = Report.Month,
                        Schools_Submitted = CountReportsSubmitted(Email, Month, Year),
                        R_ID = Report.R_ID
                    };
                }
            }
            return null;
            
        }

        public bool AddAreaReport(AreaReport report, string Email, string Password)
        {
            Boolean state = false;
            var tempArea = (from r in db.A_Reports where r.R_ID == report.R_ID select r).FirstOrDefault() ?? null;
            //Check if the areareport being submitted is the new one or new changes are added to an existing one
            
            A_Report areaReport = new A_Report()
            {
                Allocation = report.Allocation,
                A_No = GetAreaNumber(Email),
                Balance = report.Balance,
                BankCharges = report.BankCharges,
                Expenditure = report.Expenditure,
                Month = report.Month,
                Schools_Submitted = report.Schools_Submitted,
                Submission_Date = report.Submission_Date
            };
            try
            {
                if (tempArea != null)
                {
                    tempArea.Allocation = report.Allocation;
                    tempArea.Balance = report.Balance;
                    tempArea.BankCharges = report.BankCharges;
                    tempArea.Expenditure = report.Expenditure;
                    tempArea.Month = report.Month;
                    tempArea.Schools_Submitted = report.Schools_Submitted;
                    tempArea.Submission_Date = report.Submission_Date;
                    db.SubmitChanges();
                    state = true;
                    return state;
                }
                db.A_Reports.InsertOnSubmit(areaReport);
                db.SubmitChanges();
                state = true;
            }
            catch(Exception e)
            {
                PrintError(e);
                state = false;
            }
            return state;
        }
        /* SortAreaReports does two main operations, it adds up all the reports that belong 
         * to a particular area office(circuit) and then fliter them as requested*/
        private SchoolReport SortAreaReports(dynamic reports, int Sort)
        {
            SchoolReport tempReport = new SchoolReport();
            foreach (var report in reports)
            {
                switch (Sort)
                {
                    case 1:
                    {
                        if(report.tempType == 1)
                        {
                            tempReport.Food += (double)report.tempFood;
                            tempReport.BankCharges += (double)report.tempCharges;
                            tempReport.Balance += (double)report.tempBalance;
                            tempReport.Greenies += (double)report.tempVeges;
                            tempReport.AvgLearnerFed += (int)report.tempAvgLearners;
                            tempReport.Allocation += (double)report.tempAllocated;
                            tempReport.Expenditure += (double)report.tempExpenses;
                            tempReport.Honorarium += (double)report.tempHonori;
                            tempReport.NumDays += (int)report.tempDays;
                            tempReport.Gas_Wood += (double)report.tempWood;
                            tempReport.ClosingBalance += (double)report.tempClosingBalance;
                            tempReport.AvgBudget += (double)report.tempAvgBudget;
                        }
                    }
                    break;
                        //Case 2 where you sort by School type 2
                    case 2:
                    {
                        if (report.tempType == 2)
                        {
                            tempReport.Food += (double)report.tempFood;
                            tempReport.BankCharges += (double)report.tempCharges;
                            tempReport.Balance += (double)report.tempBalance;
                            tempReport.Greenies += (double)report.tempVeges;
                            tempReport.AvgLearnerFed += (int)report.tempAvgLearners;
                            tempReport.Allocation += (double)report.tempAllocated;
                            tempReport.Expenditure += (double)report.tempExpenses;
                            tempReport.Honorarium += (double)report.tempHonori;
                            tempReport.NumDays += (int)report.tempDays;
                            tempReport.Gas_Wood += (double)report.tempWood;
                            tempReport.ClosingBalance += (double)report.tempClosingBalance;
                            tempReport.AvgBudget += (double)report.tempAvgBudget;
                        }
                    }
                    break;
                    case 3:
                    {
                        if (report.tempType == 3)
                        {
                            tempReport.Food += (double)report.tempFood;
                            tempReport.BankCharges += (double)report.tempCharges;
                            tempReport.Balance += (double)report.tempBalance;
                            tempReport.Greenies += (double)report.tempVeges;
                            tempReport.AvgLearnerFed += (int)report.tempAvgLearners;
                            tempReport.Allocation += (double)report.tempAllocated;
                            tempReport.Expenditure += (double)report.tempExpenses;
                            tempReport.Honorarium += (double)report.tempHonori;
                            tempReport.NumDays += (int)report.tempDays;
                            tempReport.Gas_Wood += (double)report.tempWood;
                            tempReport.ClosingBalance += (double)report.tempClosingBalance;
                            tempReport.AvgBudget += (double)report.tempAvgBudget;
                        }
                    }
                    break;
                    default:
                    {
                            tempReport.Food += (double)report.tempFood;
                            tempReport.BankCharges += (double)report.tempCharges;
                            tempReport.Balance += (double)report.tempBalance;
                            tempReport.Greenies += (double)report.tempVeges;
                            tempReport.AvgLearnerFed += (int)report.tempAvgLearners;
                            tempReport.Allocation += (double)report.tempAllocated;
                            tempReport.Expenditure += (double)report.tempExpenses;
                            tempReport.Honorarium += (double)report.tempHonori;
                            tempReport.NumDays += (int)report.tempDays;
                            tempReport.Gas_Wood += (double)report.tempWood;
                            tempReport.ClosingBalance += (double)report.tempClosingBalance;
                            tempReport.AvgBudget += (double)report.tempAvgBudget;
                    }
                    break;       
                }     
            }
            return tempReport;
        }
        
        public SchoolReport GenerateAreaReport(string Email, string Password, string Month, int Year, int Sort = 4)
        {
            //Getting reports from school under this area for this Month & this Year, plus which type of school(primary, secondary, special)
            dynamic reports = (from r in db.S_Reports join s in db.Schools on
                               r.EMIS equals s.EMIS
                               where s.A_No == GetAreaNumber(Email) && r.Month == Month  && 
                               Convert.ToDateTime(r.Submission_Date).Year == Year
                               select new
                               {
                                   tempFood = r.Food,
                                   tempCharges = r.BankCharges,
                                   tempWood = r.Gas_Wood,
                                   tempAvgLearners = r.AvgLearnerFed,
                                   tempAllocated = r.Allocation,
                                   tempBalance = r.Balance,
                                   tempExpenses = r.Expenditure,
                                   tempVeges = r.Greenies,
                                   tempHonori = r.Honorarium,
                                   tempDays = r.NumDays,
                                   tempType = s.S_Type,
                                   tempAvgBudget = r.AvgBudget,
                                   tempClosingBalance = r.ClosingBalance
                               }) ?? null;
            if(reports != null)
            {
                //Returning added values from all(typed) schools under this area
                return SortAreaReports(reports, Sort);
            }
            return null;       
        }

        public List<TempReport> GenerateDistrictReport(string email, string password, string month, int Year)
        {
            if (!LoginEntity(email, password)) return null;

            if(GetLevel(email, password).Equals(4))
            {

            }

            string DistrictNumber = db.Districts.SingleOrDefault(a => a.D_Email == email).D_No;
            List<TempReport> tempReports = new List<TempReport>();


            dynamic reports = (from r in db.S_Reports
                               join s in db.Schools on
                               r.EMIS equals s.EMIS
                               where ((from x in db.AreaOffices where 
                                     x.A_No == s.A_No select x).SingleOrDefault().D_No == DistrictNumber) &&
                               Convert.ToDateTime(r.Submission_Date).Year == Year && r.Month == month
                               select new
                               {
                                   //Information pertaining to the report
                                   tempFood = r.Food,
                                   tempCharges = r.BankCharges,
                                   tempWood = r.Gas_Wood,
                                   tempAvgLearners = r.AvgLearnerFed,
                                   tempAllocated = r.Allocation,
                                   tempBalance = r.Balance,
                                   tempDate = r.Submission_Date,
                                   tempExpenses = r.Expenditure,
                                   tempVeges = r.Greenies,
                                   tempHonori = r.Honorarium,
                                   tempDays = r.NumDays,
                                   tempAvgBudget = s.S_Budget,
                                   tempClosingBalance = r.ClosingBalance,
                                   //School Information
                                   tempSchoolEMIS = s.EMIS,
                                   tempSchoolName = s.S_Name,
                                   tempSchoolTelephone = s.S_TellNumber,
                                   tempSchoolCellphone = s.S_CellPhone,
                                   tempSchoolType = s.S_Type,
                                   tempLearners = s.S_No_learners,
                                   tempBudget = r.AvgBudget,
                                   tempAreaNumber = s.A_No
                               }) ?? null;

            if (reports == null) return null;

            foreach(var report in reports)
            {
                tempReports.Add(new TempReport
                {
                    //School info
                    TempAreaNumber = report.tempAreaNumber,
                    TempSchoolCellphone = report.tempSchoolCellphone,
                    TempSchoolEMIS = report.tempSchoolEMIS,
                    TempSchoolName = report.tempSchoolName,
                    TempSchoolType = report.tempSchoolType,
                    TempSchoolTelephone = report.tempSchoolTelephone,
                    TempNumLearners = report.tempLearners,
                    TempBudget = report.tempBudget,
                    //the report info pertaning to the school above
                    TempFood = report.tempFood,
                    TempCharges = report.tempCharges,
                    TempWood = report.tempWood,
                    TempAvgLearners = report.tempAvgLearners,
                    TempAllocated = report.tempAllocated,
                    TempBalance = report.tempBalance,
                    TempClosingBalance = report.tempClosingBalance,
                    TempAvgBudget = report.tempAvgBudget,
                    TempDate = report.tempDate,
                    TempDays = report.tempDays,
                    TempExpenses = report.tempExpenses,
                    TempVeges = report.tempVeges,
                    TempHonori = report.tempHonori
                }) ;
            }

            return (tempReports.Count() > 0)? tempReports : null;
        }
        
        /*The following function is used to  check if the user is authorised to see the requested report. Using the URL parameters*/
        public bool VerifyReportAuth(int reportID, string Email, string Password, string Level)
        {
            dynamic auth = null;
            //Check authority first
            if (Level.Equals("1"))
                auth = db.S_Reports.FirstOrDefault(a => a.R_ID.Equals(reportID) 
                && a.EMIS.Equals(GetEMIS(Email, Password))) ?? null;

            if (Level.Equals("2"))
                auth = db.A_Reports.FirstOrDefault(a => a.R_ID.Equals(reportID) 
                && a.A_No.Equals(GetAreaNumber(Email))) ?? null;

            if(Level.Equals("3"))
            {
                auth = (from report in db.S_Reports join school in db.Schools on report.EMIS equals 
                        school.EMIS where report.R_ID == reportID &&
                        db.AreaOffices.
                        FirstOrDefault(a => a.A_No == school.A_No).D_No == GetDistrict(Email, Password).D_No
                        select report).FirstOrDefault() ?? null;
            }

            return (auth != null) ? true : false;
        }

        public string DeleteSchoolReport(string Email, string Password, int ReportID)
        {
            
            string Name  = "";
            var report = db.S_Reports.FirstOrDefault(r => r.R_ID.Equals(ReportID) && r.EMIS.Equals(GetEMIS(Email, Password)));
            try
            {
                if(report != null)
                {
                    var file = db.FileInformations.FirstOrDefault(a => a.Code == report.Code) ?? null;
                    if(file != null)
                    {
                        Name = file.Name;
                        db.FileInformations.DeleteOnSubmit(file);
                    }
                    db.S_Reports.DeleteOnSubmit(report);
                    db.SubmitChanges();            
                }
            }
            catch(Exception e)
            {
                PrintError(e);
            }
            return Name;
        }

        public bool AddSchoolMessage(string Email, string Password, SchoolMessage message)
        {
            Boolean state = false;
            if(LoginEntity(Email, Password))
            {
                try
                {
                    if (GetLevel(Email, Password).Equals(2))
                    {
                        AreaSchoolMessage dbMessage = new AreaSchoolMessage()
                        {
                            A_No = GetAreaNumber(Email),
                            Date = message.Date,
                            Type = message.Type,
                            Subject = message.Subject,
                            Message = message.Message
                        };
                        db.AreaSchoolMessages.InsertOnSubmit(dbMessage);
                        db.SubmitChanges();
                    }

                    if (GetLevel(Email, Password).Equals(3))
                    {
                        DistrictAreaMessage districtAreaMessage = new DistrictAreaMessage()
                        {
                            D_No = GetDistrict(Email, Password).D_No,
                            Date = message.Date,
                            Type = message.Type,
                            Subject = message.Subject,
                            Message = message.Message
                        };
                        db.DistrictAreaMessages.InsertOnSubmit(districtAreaMessage);
                        db.SubmitChanges();  
                    }
                    state = true;
                }
                catch (Exception e)
                {
                    PrintError(e);
                    state = false;
                }
            }
            return state;
        }

        public bool DeleteMessage(string Email, string Password, string ID)
        {
            Boolean state = false;
            if(LoginEntity(Email, Password))
            {
                try
                {
                    AreaSchoolMessage schoolMessage = null;
                    DistrictAreaMessage districtAreaMessage = null;
                    if(GetLevel(Email, Password).Equals(2))
                    {
                        schoolMessage = db.AreaSchoolMessages.FirstOrDefault(a => 
                                        a.A_No == GetAreaNumber(Email) && a.M_ID == Convert.ToInt32(ID));
                        db.AreaSchoolMessages.DeleteOnSubmit(schoolMessage);
                        db.SubmitChanges();
                        state = true;
                    }
                    if(GetLevel(Email, Password).Equals(3))
                    {
                        districtAreaMessage = db.DistrictAreaMessages.FirstOrDefault(d => d.D_No == GetDistrict(Email, Password).D_No 
                                                                                        && d.M_ID == Convert.ToInt32(ID));
                        db.DistrictAreaMessages.DeleteOnSubmit(districtAreaMessage);
                        db.SubmitChanges();
                        state = true;
                    }
                }
                catch(Exception e)
                {
                    PrintError(e);
                    state = false;
                }
            }
            return state;
        }

        public string GetDistrictName(string Email, string Password)
        {
            string Name = "";
            Name = (from d in db.Districts join a in db.AreaOffices 
                    on d.D_No equals a.D_No where 
                    a.A_No == GetAreaNumber(Email)
                    select d.D_Name).FirstOrDefault();
            return Name;
        }
        public string DisplayDistrictName(string Email, string Password)
        {
            string Name = "";
            Name = (from d in db.Districts where d.D_Email.Equals(Email) select d.D_Name).FirstOrDefault();
            return Name;
        }

        private string GetDistrictNumber(string email, string password)
        {
            return db.Districts.SingleOrDefault(a => a.D_Email == email).D_No;
        }

        public SchoolReport SortedSchoolSummary(dynamic Reports)
        {
            throw new NotImplementedException();
        }
        public List<AreaMessage> GetAreaMessages(string email, string password)
        {
            List<AreaMessage> messages = new List<AreaMessage>();
            dynamic dbMessages = null;
            if (GetLevel(email, password).Equals(2))
                dbMessages = (from m in db.DistrictAreaMessages where m.D_No == GetArea(email, password).D_No orderby m.Date descending select m);
            if (GetLevel(email, password).Equals(3))
                dbMessages = (from m in db.DistrictAreaMessages where m.D_No == GetDistrict(email, password).D_No orderby m.Date descending select m);
            if(dbMessages != null)
            {
                foreach(DistrictAreaMessage temp in dbMessages)
                {
                    messages.Add(new AreaMessage()
                    {
                        M_ID = temp.M_ID,
                        Subject = temp.Subject,
                        Type = temp.Type,
                        Date = temp.Date,
                        D_No = temp.D_No,
                        Message = temp.Message
                    });
                }
                return messages;
            }
            return null;
        }
        public bool AddAreaMessage(string Email, string Password, AreaMessage message)
        {
            bool state = false;
            if (!LoginEntity(Email, Password)) return state;
            DistrictAreaMessage areaMessage = new DistrictAreaMessage()
            {
                Date = message.Date,
                D_No = message.D_No,
                M_ID = message.M_ID,
                Message = message.Message,
                Subject = message.Subject,
                Type = message.Type
            };

            try
            {
                db.DistrictAreaMessages.InsertOnSubmit(areaMessage);
                db.SubmitChanges();
                state = true;
            }
            catch(Exception e)
            {
                state = false;
                PrintError(e);
            }
            return state;
        }

        private AreaOffice GetAreaInfo(string AreaNumber)
        {
            var area = db.AreaOffices.FirstOrDefault(a => a.A_No == AreaNumber) ?? null;
            if(area == null)
                return null;
            return area;
        }

        public List<AreaOffice> GetAreas(string email, string password)
        {
            dynamic areas = (from a in db.AreaOffices where a.D_No == GetDistrict(email, password).D_No select a) ?? null;
            List<AreaOffice> areaReports = new List<AreaOffice>();
            if(areas != null)
            {
                foreach(AreaOffice area in areas)
                {
                    areaReports.Add(new AreaOffice()
                    {
                        A_No = area.A_No,
                        A_Cellphone = area.A_Cellphone,
                        A_Tellphone = area.A_Tellphone,
                        A_Manager = area.A_Manager,
                        A_Name = area.A_Name,
                        A_Email = area.A_Email,
                        A_NumSchools = area.A_NumSchools
                    });
                }
                return areaReports;
            }
            return null;
        }
        private TempReport SchoolQuarterReport(string EMIS, dynamic reports)
        {
            if (reports == null) return null;
            TempReport tempReport = new TempReport();
            foreach(var report in reports)
            {
                if(EMIS == report.tempSchoolEMIS)
                {
                    //Important information regardinng the schools
                    tempReport.TempSchoolEMIS = report.tempSchoolEMIS;
                    tempReport.TempSchoolName = report.tempSchoolName;
                    tempReport.TempSchoolCellphone = report.tempSchoolCellphone;
                    tempReport.TempSchoolTelephone = report.tempSchoolTelephone;
                    tempReport.TempSchoolType = report.tempSchoolType;
                    tempReport.TempAreaNumber = report.tempAreaNumber;
                    tempReport.TempNumLearners = report.tempLearners;
                    tempReport.TempAvgLearners += report.tempAvgLearners;
                    //Related to the finances
                    tempReport.TempBudget += report.tempBudget;
                    tempReport.TempAllocated += report.tempAllocated;
                    tempReport.TempBalance += report.tempBalance;
                    tempReport.TempExpenses += report.tempExpenses;
                }
            }
            tempReport.TempAvgLearners /= 3;
            return tempReport;
        }

        public List<TempReport> GenerateDistrictQuarterReport(string email, string password, int quarter, int year)
        {        
            int MonthIn = 0;
            int MonthOut = 0;
            //Used to determine the months that belong to the @param quarter
            switch(quarter)
            {
                case 1:
                    {     
                        MonthIn = 4;
                        MonthOut = 6; 
                    }
                    break;
                case 2:
                    {
                        MonthIn =7;
                        MonthOut = 9;
                    }
                    break;
                case 3:
                    {
                        MonthIn = 10;
                        MonthOut = 12;
                    }
                    break;
                case 4:
                    {
                        MonthIn = 1;
                        MonthOut = 3;
                    }
                    break;
            }

            if (!LoginEntity(email, password)) return null;
            string DistrictNumber = db.Districts.SingleOrDefault(a => a.D_Email == email).D_No;
            List<TempReport> tempReports = new List<TempReport>();

            dynamic reports = (from r in db.S_Reports
                               join s in db.Schools on
                               r.EMIS equals s.EMIS
                               where ((from x in db.AreaOffices
                                       where x.A_No == s.A_No
                                       select x).SingleOrDefault().D_No == DistrictNumber) &&
                               Convert.ToDateTime(r.Submission_Date).Year == year 
                               && (Convert.ToInt32(r.Month) >= MonthIn && Convert.ToInt32(r.Month) <= MonthOut)
                               select new
                               {
                                   //Information pertaining to the report
                                   tempAvgLearners = r.AvgLearnerFed,
                                   tempAllocated = r.Allocation,
                                   tempBalance = r.Balance,
                                   tempExpenses = r.Expenditure,   
                                   tempAvgBudget = s.S_Budget,                   
                                   //School Information
                                   tempSchoolEMIS = s.EMIS,
                                   tempSchoolName = s.S_Name,
                                   tempSchoolTelephone = s.S_TellNumber,
                                   tempSchoolCellphone = s.S_CellPhone,
                                   tempSchoolType = s.S_Type,
                                   tempLearners = s.S_No_learners,
                                   tempBudget = r.AvgBudget,
                                   tempAreaNumber = s.A_No
                               }) ?? null;

            if (reports == null) return null;
            foreach (var report in reports)
            {
                //Check if the entity summary has not been calculated... if not, add it to tempReports
                if (!tempReports.Exists(a => a.TempSchoolEMIS == report.tempSchoolEMIS))
                {
                    tempReports.Add(SchoolQuarterReport(report.tempSchoolEMIS, reports));
                }
            }     
            return tempReports;
        }
        private dynamic SortReportList(string EMIS, string D_No, int sort)
        {
            //Sort the reports for the specific district 
            dynamic dbreports = null;
            switch (sort)
            {
                case 1:
                    {
                        dbreports = (from r in db.S_Reports
                                     join s in db.Schools on
                                     r.EMIS equals s.EMIS
                                     where ((from x in db.AreaOffices where x.A_No == s.A_No select x).SingleOrDefault().D_No == D_No)
                                     && r.EMIS == EMIS
                                     orderby r.Month
                                     select r);
                    }
                    break;
                case 2:
                    {

                        dbreports = (from r in db.S_Reports
                                     join s in db.Schools on
                                     r.EMIS equals s.EMIS
                                     where ((from x in db.AreaOffices where x.A_No == s.A_No select x).SingleOrDefault().D_No == D_No)
                                     && r.EMIS == EMIS
                                     orderby Convert.ToDateTime(r.Submission_Date).Year
                                     select r);
                    }
                    break;
                case 3:
                    {

                        dbreports = (from r in db.S_Reports
                                     join s in db.Schools on
                                     r.EMIS equals s.EMIS
                                     where ((from x in db.AreaOffices where x.A_No == s.A_No select x).SingleOrDefault().D_No == D_No)
                                     && r.EMIS == EMIS
                                     orderby r.Allocation
                                     select r);
                    }
                    break;
                case 4:
                    {

                        dbreports = (from r in db.S_Reports
                                     join s in db.Schools on
                                     r.EMIS equals s.EMIS
                                     where ((from x in db.AreaOffices where x.A_No == s.A_No select x).SingleOrDefault().D_No == D_No)
                                     && r.EMIS == EMIS
                                     orderby r.Balance
                                     select r);
                    }
                    break;
                case 5:
                    {
                        dbreports = (from r in db.S_Reports
                                     join s in db.Schools on
                                     r.EMIS equals s.EMIS
                                     where ((from x in db.AreaOffices where x.A_No == s.A_No select x).SingleOrDefault().D_No == D_No)
                                     && r.EMIS == EMIS
                                     orderby r.Expenditure
                                     select r);
                    }
                    break;
                default:
                    {
                        dbreports = (from r in db.S_Reports
                                     join s in db.Schools on
                                     r.EMIS equals s.EMIS
                                     where ((from x in db.AreaOffices where x.A_No == s.A_No select x).SingleOrDefault().D_No == D_No)
                                     && r.EMIS == EMIS
                                     orderby r.Month    
                                     select r);
                    }
                    break;
            }
            return (dbreports != null) ? dbreports : null;
        }
        private dynamic GetSchoolReportsForArea(string email, string password, string EMIS)
        {
            string A_No = GetArea(email, password).A_No;
            dynamic reports = (from repos in db.S_Reports join school 
                               in db.Schools on repos.EMIS equals school.EMIS
                               where school.EMIS == EMIS && school.A_No == A_No orderby repos.Submission_Date select repos) ?? null;
            return (reports != null) ? reports : null;
        }
        public List<SchoolReport> GetSchoolReportsByAD(string email, string password, string EMIS, int sort)
        {
            List<SchoolReport> reports = new List<SchoolReport>();
            dynamic dbreports = null;
            string D_No = "";

            if (GetLevel(email, password) == 3)
            {
                D_No = GetDistrictNumber(email, password);
                dbreports = SortReportList(EMIS, D_No, sort) ?? null;
            }             
            else if (GetLevel(email, password) == 2)
            {
                //Only the district gets the privilege of having ordered reports... because
                dbreports = GetSchoolReportsForArea(email, password, EMIS);
            }

            if (dbreports != null)
            {
                foreach (S_Report scr in dbreports)
                {
                    reports.Add(new SchoolReport()
                    {
                        //Add all the information to this
                        EMIS = scr.EMIS,
                        Allocation = scr.Allocation,
                        Balance = scr.Balance,
                        BankCharges = scr.BankCharges,
                        Expenditure = scr.Expenditure,
                        Food = scr.Food,
                        Gas_Wood = scr.Gas_Wood,
                        Greenies = scr.Greenies,
                        Honorarium = scr.Honorarium,
                        Month = scr.Month,
                        NumDays = scr.NumDays,
                        Submission_Date = scr.Submission_Date,
                        R_ID = scr.R_ID,
                        AvgLearnerFed = scr.AvgLearnerFed,
                        AvgBudget = scr.AvgBudget,
                        ClosingBalance = scr.ClosingBalance
                    });
                }

            }
            return (reports.Count() > 0) ? reports : null;
        }

        public bool LogData(string email, string password, string metadata)
        {
            return 0>0;
        }

        public bool DisconnectSchool(string email, string password, string EMIS)
        {
            string A_No = GetArea(email, password).A_No;
            bool state = false;
            var dbSchool = db.Schools.SingleOrDefault(s => s.EMIS == EMIS && s.A_No == A_No);
            if(dbSchool != null)
            {
                try
                {
                    dbSchool.A_No = null;
                    db.SubmitChanges();
                    state = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    state = false;
                }
            }
            return state;
        }

        public bool DisconnectArea(string email, string password, string AreaNumber)
        {
            //Districts are allowed to disconnect circuits from them. This action changes everthing, the calculations and message distribution
            string D_No = GetDistrictNumber(email, password);
            bool state = false;
            var dbArea = db.AreaOffices.SingleOrDefault(a => a.A_No == AreaNumber && a.D_No == D_No);
            if (dbArea != null)
            {
                try
                {
                    dbArea.D_No = null;
                    db.SubmitChanges();
                    state = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    state = false;
                }
            }
            return state;
        }

        public FileInfos GetFileInformation(string EMIS, string Code)
        {
            //File infomation only accessible through the unique code and the emis of the school requesting it... sometimes even the district for downloading purposes
            var fileInformation = db.FileInformations.FirstOrDefault(f => f.Code.Equals(Code) && f.EMIS == EMIS) ?? null;
            if(fileInformation != null)
            {
                return new FileInfos()
                {
                    Name = fileInformation.Name,
                    EMIS = fileInformation.EMIS,
                    ContentLength = fileInformation.ContentLength,
                    ContentType = fileInformation.ContentType
                };
            }
            return null;
        }

        public List<ourDistrict> GetDistrictsForArea()
        {
            dynamic dbDistricts = (from a in db.Districts select a) ?? null;
            List<ourDistrict> districts = new List<ourDistrict>();
            if (dbDistricts != null)
            {
               foreach(var dbd in dbDistricts)
               {
                    districts.Add(new ourDistrict()
                    {
                        D_Name = dbd.D_Name,
                        D_No = dbd.D_No
                    });
               }
                return districts;
            }
            return null;
        }

        public List<ourArea> GetAreasForSchool()
        {
            dynamic dbDistricts = (from a in db.AreaOffices select a) ?? null;
            List<ourArea> areas = new List<ourArea>();
            if (dbDistricts != null)
            {
                foreach (var dbd in dbDistricts)
                {
                    areas.Add(new ourArea()
                    {
                        A_Name = dbd.A_Name,
                        A_No = dbd.A_No,
                    });
                }
                return areas;
            }
            return null;
        }
        
        public List<TempReport> ExtendedQuarterReport(string P_No, string districtNo, string areaNo, string EMIS, int quarter, int year)
        {
            int MonthIn = 0;
            int MonthOut = 0;
            //Used to determine the months that belong to the @param quarter
            switch (quarter)
            {
                case 1:
                    {
                        MonthIn = 4;
                        MonthOut = 6;
                    }
                    break;
                case 2:
                    {
                        MonthIn = 7;
                        MonthOut = 9;
                    }
                    break;
                case 3:
                    {
                        MonthIn = 10;
                        MonthOut = 12;
                    }
                    break;
                case 4:
                    {
                        MonthIn = 1;
                        MonthOut = 3;
                    }
                    break;
            }

            dynamic reports = null;
            List<TempReport> tempReports = new List<TempReport>();
            if (districtNo.Equals("0"))
            {
                reports = QuarterReportProvince(P_No, year, MonthIn, MonthOut);
            }
            else if (areaNo.Equals("0") && !districtNo.Equals("0") && EMIS.Equals("0"))
            {
                reports = QuarterReportDistrict(districtNo, year, MonthIn, MonthOut);
            }
            else if (!areaNo.Equals("0") && !districtNo.Equals("0") && EMIS.Equals("0"))
            {
                reports = QuarterReportCircuit(districtNo, areaNo, year, MonthIn, MonthOut);
            }
            else if (!areaNo.Equals("0") && !districtNo.Equals("0") && !EMIS.Equals("0"))
            {
                reports = SchoolQuarterReport(EMIS, null);
            }

            if (reports == null) return null;
            foreach (var report in reports)
            {
                //Check if the entity summary has not been calculated... if not, add it to tempReports
                if (!tempReports.Exists(a => a.TempSchoolEMIS == report.tempSchoolEMIS))
                {
                    tempReports.Add(SchoolQuarterReport(report.tempSchoolEMIS, reports));
                }
            }
            return tempReports;
        }
        private dynamic QuarterReportProvince(string provinceNo, int year, int MonthIn, int MonthOut)
        {
            dynamic reports = (from r in db.S_Reports
                               join s in db.Schools on
                               r.EMIS equals s.EMIS
                               where s.P_No == provinceNo &&
                               Convert.ToDateTime(r.Submission_Date).Year == year
                               && (Convert.ToInt32(r.Month) >= MonthIn && Convert.ToInt32(r.Month) <= MonthOut)
                               select new
                               {
                                   //Information pertaining to the report
                                   tempAvgLearners = r.AvgLearnerFed,
                                   tempAllocated = r.Allocation,
                                   tempBalance = r.Balance,
                                   tempExpenses = r.Expenditure,
                                   tempAvgBudget = s.S_Budget,
                                   //School Information
                                   tempSchoolEMIS = s.EMIS,
                                   tempSchoolName = s.S_Name,
                                   tempSchoolTelephone = s.S_TellNumber,
                                   tempSchoolCellphone = s.S_CellPhone,
                                   tempSchoolType = s.S_Type,
                                   tempLearners = s.S_No_learners,
                                   tempBudget = r.AvgBudget,
                                   tempAreaNumber = s.A_No
                               }) ?? null;
            return reports;
        }

        private dynamic QuarterReportDistrict(string districtNo, int year, int MonthIn, int MonthOut)
        {
            dynamic reports = (from r in db.S_Reports
                               join s in db.Schools on
                               r.EMIS equals s.EMIS
                               where((from x in db.AreaOffices
                                           where x.A_No == s.A_No
                                           select x).SingleOrDefault().D_No == districtNo) &&
                               Convert.ToDateTime(r.Submission_Date).Year == year
                               && (Convert.ToInt32(r.Month) >= MonthIn && Convert.ToInt32(r.Month) <= MonthOut)
                               select new
                               {
                                   //Information pertaining to the report
                                   tempAvgLearners = r.AvgLearnerFed,
                                   tempAllocated = r.Allocation,
                                   tempBalance = r.Balance,
                                   tempExpenses = r.Expenditure,
                                   tempAvgBudget = s.S_Budget,
                                   //School Information
                                   tempSchoolEMIS = s.EMIS,
                                   tempSchoolName = s.S_Name,
                                   tempSchoolTelephone = s.S_TellNumber,
                                   tempSchoolCellphone = s.S_CellPhone,
                                   tempSchoolType = s.S_Type,
                                   tempLearners = s.S_No_learners,
                                   tempBudget = r.AvgBudget,
                                   tempAreaNumber = s.A_No
                               }) ?? null;
            return reports;
        }
        private dynamic QuarterReportCircuit(string districtNo, string areaNo, int year, int MonthIn, int MonthOut)
        {
            dynamic reports = (from r in db.S_Reports
                               join s in db.Schools on
                               r.EMIS equals s.EMIS
                               where ((from x in db.AreaOffices
                                       where x.A_No == s.A_No && x.A_No == areaNo
                                       select x).SingleOrDefault().D_No == districtNo) &&
                               Convert.ToDateTime(r.Submission_Date).Year == year
                               && (Convert.ToInt32(r.Month) >= MonthIn && Convert.ToInt32(r.Month) <= MonthOut)
                               select new
                               {
                                   //Information pertaining to the report
                                   tempAvgLearners = r.AvgLearnerFed,
                                   tempAllocated = r.Allocation,
                                   tempBalance = r.Balance,
                                   tempExpenses = r.Expenditure,
                                   tempAvgBudget = s.S_Budget,
                                   //School Information
                                   tempSchoolEMIS = s.EMIS,
                                   tempSchoolName = s.S_Name,
                                   tempSchoolTelephone = s.S_TellNumber,
                                   tempSchoolCellphone = s.S_CellPhone,
                                   tempSchoolType = s.S_Type,
                                   tempLearners = s.S_No_learners,
                                   tempBudget = r.AvgBudget,
                                   tempAreaNumber = s.A_No
                               }) ?? null;
            return reports;
        }
        private dynamic ReportsForProvince(string provinceNo, int year, string month)
        {
            dynamic Reports = (from r in db.S_Reports
                               join s in db.Schools on
                               r.EMIS equals s.EMIS
                               where s.P_No == provinceNo  &&
                               Convert.ToDateTime(r.Submission_Date).Year == year && r.Month == month
                               select new
                               {
                                   //Information pertaining to the report
                                   tempFood = r.Food,
                                   tempCharges = r.BankCharges,
                                   tempWood = r.Gas_Wood,
                                   tempAvgLearners = r.AvgLearnerFed,
                                   tempAllocated = r.Allocation,
                                   tempBalance = r.Balance,
                                   tempDate = r.Submission_Date,
                                   tempExpenses = r.Expenditure,
                                   tempVeges = r.Greenies,
                                   tempHonori = r.Honorarium,
                                   tempDays = r.NumDays,
                                   tempAvgBudget = s.S_Budget,
                                   tempClosingBalance = r.ClosingBalance,
                                   //School Information
                                   tempSchoolEMIS = s.EMIS,
                                   tempSchoolName = s.S_Name,
                                   tempSchoolTelephone = s.S_TellNumber,
                                   tempSchoolCellphone = s.S_CellPhone,
                                   tempSchoolType = s.S_Type,
                                   tempLearners = s.S_No_learners,
                                   tempBudget = r.AvgBudget,
                                   tempAreaNumber = s.A_No
                               }) ?? null;


            return Reports;
        }
        private dynamic ReportsByDistrict(string districtNo, int year, string month)
        {
            dynamic Reports = (from r in db.S_Reports
                               join s in db.Schools on
                               r.EMIS equals s.EMIS
                               where ((from x in db.AreaOffices
                                       where x.A_No == s.A_No
                                       select x).SingleOrDefault().D_No == districtNo) &&
                               Convert.ToDateTime(r.Submission_Date).Year == year && r.Month == month
                               select new
                               {
                                   //Information pertaining to the report
                                   tempFood = r.Food,
                                   tempCharges = r.BankCharges,
                                   tempWood = r.Gas_Wood,
                                   tempAvgLearners = r.AvgLearnerFed,
                                   tempAllocated = r.Allocation,
                                   tempBalance = r.Balance,
                                   tempDate = r.Submission_Date,
                                   tempExpenses = r.Expenditure,
                                   tempVeges = r.Greenies,
                                   tempHonori = r.Honorarium,
                                   tempDays = r.NumDays,
                                   tempAvgBudget = s.S_Budget,
                                   tempClosingBalance = r.ClosingBalance,
                                   //School Information
                                   tempSchoolEMIS = s.EMIS,
                                   tempSchoolName = s.S_Name,
                                   tempSchoolTelephone = s.S_TellNumber,
                                   tempSchoolCellphone = s.S_CellPhone,
                                   tempSchoolType = s.S_Type,
                                   tempLearners = s.S_No_learners,
                                   tempBudget = r.AvgBudget,
                                   tempAreaNumber = s.A_No
                               }) ?? null;


            return Reports;
        }
        private dynamic ReportsByCircuits(string districtNo, string areaNo, int year, string month)
        {
            dynamic Reports = (from r in db.S_Reports
                               join s in db.Schools on
                               r.EMIS equals s.EMIS
                               where ((from x in db.AreaOffices
                                       where x.A_No == s.A_No && x.A_No == areaNo
                                       select x).SingleOrDefault().D_No == districtNo) &&
                               Convert.ToDateTime(r.Submission_Date).Year == year && r.Month == month
                               select new
                               {
                                   //Information pertaining to the report
                                   tempFood = r.Food,
                                   tempCharges = r.BankCharges,
                                   tempWood = r.Gas_Wood,
                                   tempAvgLearners = r.AvgLearnerFed,
                                   tempAllocated = r.Allocation,
                                   tempBalance = r.Balance,
                                   tempDate = r.Submission_Date,
                                   tempExpenses = r.Expenditure,
                                   tempVeges = r.Greenies,
                                   tempHonori = r.Honorarium,
                                   tempDays = r.NumDays,
                                   tempAvgBudget = s.S_Budget,
                                   tempClosingBalance = r.ClosingBalance,
                                   //School Information
                                   tempSchoolEMIS = s.EMIS,
                                   tempSchoolName = s.S_Name,
                                   tempSchoolTelephone = s.S_TellNumber,
                                   tempSchoolCellphone = s.S_CellPhone,
                                   tempSchoolType = s.S_Type,
                                   tempLearners = s.S_No_learners,
                                   tempBudget = r.AvgBudget,
                                   tempAreaNumber = s.A_No
                               }) ?? null;


            return Reports;
        }
        private dynamic ReportsBySchools(string districtNo, string areaNo, string EMIS, int year, string month)
        {
            dynamic Reports = (from r in db.S_Reports
                               join s in db.Schools on
                               r.EMIS equals s.EMIS
                               where ((from x in db.AreaOffices
                                       where x.A_No == s.A_No && x.A_No == areaNo
                                       select x).SingleOrDefault().D_No == districtNo) && s.EMIS == EMIS &&
                               Convert.ToDateTime(r.Submission_Date).Year == year && r.Month == month
                               select new
                               {
                                   //Information pertaining to the report
                                   tempFood = r.Food,
                                   tempCharges = r.BankCharges,
                                   tempWood = r.Gas_Wood,
                                   tempAvgLearners = r.AvgLearnerFed,
                                   tempAllocated = r.Allocation,
                                   tempBalance = r.Balance,
                                   tempDate = r.Submission_Date,
                                   tempExpenses = r.Expenditure,
                                   tempVeges = r.Greenies,
                                   tempHonori = r.Honorarium,
                                   tempDays = r.NumDays,
                                   tempAvgBudget = s.S_Budget,
                                   tempClosingBalance = r.ClosingBalance,
                                   //School Information
                                   tempSchoolEMIS = s.EMIS,
                                   tempSchoolName = s.S_Name,
                                   tempSchoolTelephone = s.S_TellNumber,
                                   tempSchoolCellphone = s.S_CellPhone,
                                   tempSchoolType = s.S_Type,
                                   tempLearners = s.S_No_learners,
                                   tempBudget = r.AvgBudget,
                                   tempAreaNumber = s.A_No
                               }) ?? null;


            return Reports;
        }
        public List<TempReport> ExtendedDistrictReport(string P_No, string districtNo, string areaNo, string EMIS, int year, string month)
        {
            dynamic reports = null;
            List<TempReport> tempReports = new List<TempReport>();
            if (districtNo.Equals("0"))
            {
                reports =  ReportsForProvince(P_No, year, month);
            }
            else if(areaNo.Equals("0") && !districtNo.Equals("0") && EMIS.Equals("0"))
            {
                reports = ReportsByDistrict(districtNo, year, month);
            }
            else if(!areaNo.Equals("0") && !districtNo.Equals("0") && EMIS.Equals("0"))
            {
                reports = ReportsByCircuits(districtNo, areaNo, year, month);
            }
            else if (!areaNo.Equals("0") && !districtNo.Equals("0") && !EMIS.Equals("0"))
            {
                reports = ReportsBySchools(districtNo, areaNo, EMIS, year, month);
            }

            if (reports == null) return null;

            foreach (var report in reports)
            {
                tempReports.Add(new TempReport
                {
                    //School info
                    TempAreaNumber = report.tempAreaNumber,
                    TempSchoolCellphone = report.tempSchoolCellphone,
                    TempSchoolEMIS = report.tempSchoolEMIS,
                    TempSchoolName = report.tempSchoolName,
                    TempSchoolType = report.tempSchoolType,
                    TempSchoolTelephone = report.tempSchoolTelephone,
                    TempNumLearners = report.tempLearners,
                    TempBudget = report.tempBudget,
                    //the report info pertaning to the school above
                    TempFood = report.tempFood,
                    TempCharges = report.tempCharges,
                    TempWood = report.tempWood,
                    TempAvgLearners = report.tempAvgLearners,
                    TempAllocated = report.tempAllocated,
                    TempBalance = report.tempBalance,
                    TempClosingBalance = report.tempClosingBalance,
                    TempAvgBudget = report.tempAvgBudget,
                    TempDate = report.tempDate,
                    TempDays = report.tempDays,
                    TempExpenses = report.tempExpenses,
                    TempVeges = report.tempVeges,
                    TempHonori = report.tempHonori
                });
            }

            return (tempReports.Count() > 0) ? tempReports : null;
        }

        public List<ourArea> GetCircuitsByDistricts(string districtNo)
        {
            dynamic circuits = (from c in db.AreaOffices where c.D_No == districtNo select c) ?? null;
            List<ourArea> areas = new List<ourArea>();

            if (circuits == null) return null;

            foreach(var c in circuits)
            {
                areas.Add(new ourArea()
                {
                    D_No = c.D_No,
                    A_No = c.A_No,
                    A_Name = c.A_Name,
                    A_Email =  c.A_Email
                });
            }
            return areas;
        }

        public List<ourDistrict> GetDistrictsByProvince(string provincialNo)
        {
            dynamic dbDistricts = (from d in db.Districts where d.P_No == provincialNo select d) ?? null;
            List<ourDistrict> districts = new List<ourDistrict>();
            if (dbDistricts == null) return null;

            foreach(var d in dbDistricts)
            {
                districts.Add(new ourDistrict()
                {
                    D_Email = d.D_Email,
                    D_Name = d.D_Name,
                    D_No = d.D_No,
                    P_No = d.P_No
                });
            }
            return districts;
        }

        public List<ourEntity> GetSchoolsByCircuit(string areaNo)
        {
            dynamic dbSchools = (from s in db.Schools where s.A_No == areaNo select s) ?? null;
            if (dbSchools == null) return null;

            List<ourEntity> schools = new List<ourEntity>();
            foreach(var s in dbSchools)
            {
                schools.Add(new ourEntity()
                {
                    EMIS = s.EMIS,
                    S_Email = s.S_Email,
                    S_Name = s.S_Name,
                    A_No = s.A_No        
                });  
            }
            return schools;
        }

        public List<ourProvincial> GetAllProvinces()
        {
            dynamic dbProvinces = db.Provincials.Where(a => a.P_No !=  null) ?? null;
            List<ourProvincial> provinces = new List<ourProvincial>();
            if (dbProvinces == null) return null;
            foreach(var prov in dbProvinces)
            {
                provinces.Add(new ourProvincial()
                {
                    P_No = prov.P_No,
                    P_Email = prov.P_Email,
                    P_Name = prov.P_Name,
                    p_Manageer = prov.p_Manageer
                });
            }
            return provinces;
        }

        public ourProvincial GetProvinceByID(string P_No)
        {
            var dbProvince = db.Provincials.FirstOrDefault(p => p.P_No == P_No);
            if (dbProvince == null) return null;

            return new ourProvincial()
            {
                P_Email = dbProvince.P_Email,
                P_No = dbProvince.P_No,
                P_Cellphone = dbProvince.P_Cellphone,
                P_Name = dbProvince.P_Name,
                P_Tellphone = dbProvince.P_Tellphone,
                p_Manageer = dbProvince.p_Manageer,
                Num_Areas = dbProvince.Num_Areas,
                Num_Districts = dbProvince.Num_Districts
            };

        }

        public string TestFunction(string data)
        {
            return "this is the data that you just sent to the service: " + data;
        }

        public List<ourArea> GetCicuitsByProvince(string provincialNo)
        {
            List<ourArea> circuits = new List<ourArea>();
            dynamic dbAreas = (from a in db.AreaOffices join d 
                               in db.Districts on a.D_No 
                               equals d.D_No
                               where d.P_No == provincialNo select a);

            if (dbAreas == null) return null;
            foreach(var circuit in dbAreas)
            {
                circuits.Add(new ourArea()
                {
                    A_No = circuit.A_No,
                    A_Email = circuit.A_Email,
                    A_Name = circuit.A_Name,
                });
            }
            return circuits;
        }
    }
}
