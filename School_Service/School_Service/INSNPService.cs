using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace School_Service
{
    [ServiceContract]
    public interface INSNPService
    {
        [OperationContract]
        bool LoginEntity(string Email, string Password);

        [OperationContract]
        bool RegisterEntity(ourEntity school, string Level, string Password);

        [OperationContract]
        bool EditSchool(ourEntity school, string AreaNum, string provNo, string OldPassword, string Password);

        [OperationContract]
        ourEntity GetSchool(string email, string password);

        [OperationContract]
        ourProvincial GetProvince(string email, string password);

        [OperationContract]
        List<TempReport> ExtendedDistrictReport(string P_No, string districtNo, string areaNo, string EMIS, int year, string month);

        [OperationContract]
        bool EditProvince(ourProvincial province, string oldPassword, string newPassword);

        [OperationContract]
        int GetLevel(string email, string password);

        [OperationContract]
        string GetEMIS(string email, string password);

        [OperationContract]
        bool AddSchoolReport(SchoolReport report, string rEMIS, FileInfos file);

        [OperationContract]  
        SchoolReport GetSchoolReport(int ID);

        [OperationContract]
        bool EditScholReport(SchoolReport report);

        [OperationContract]
        string DeleteSchoolReport(string email, string password, int ReportID);

        [OperationContract]
        List<SchoolReport> GetSchoolReports(string EMIS, int Sortby);

        [OperationContract]
        FileInfos GetFileInformation(string EMIS, string Code);

        [OperationContract]
        List<SchoolReport> GetSchoolReportsByAD(string email, string password, string EMIS, int sort);//AD stands for Area or District

        [OperationContract]
        ourArea GetArea(string email, string password);

        [OperationContract]
        bool EditArea(ourArea area, string D_No, string newPassword, string oldPassword);

        [OperationContract]
        ourDistrict GetDistrict(string email, string password);

        [OperationContract]
        bool EditDistrict(ourDistrict district, string P_No, string newPassword, string oldPassword);

        [OperationContract]
        AreaReport GetAreaReport(string Email, string Password, string Month, int Year);

        [OperationContract]
        bool AddAreaReport(AreaReport report, string Email, string Password);

        [OperationContract]
        List<School> GetSchools(string email);

        [OperationContract]
        List<ourDistrict> GetDistrictsForArea();

        [OperationContract]
        List<ourArea> GetAreasForSchool();

        [OperationContract]
        SchoolReport GenerateAreaReport(string Email, string Password, string month, int year, int sort);

        [OperationContract]
        List<TempReport> GenerateDistrictReport(string email, string password, string month, int Year);   

        [OperationContract]
        bool VerifyReportAuth(int reportID, string email, string password, string level);

        [OperationContract]
        bool AddSchoolMessage(string Email, string Password, SchoolMessage message);

        [OperationContract]
        bool AddAreaMessage(string Email, string Password, AreaMessage message);

        [OperationContract]
        bool DeleteMessage(string Email, string Password, string ID);

        [OperationContract]
        List<SchoolMessage> GetSchoolMessages(string email, string password);

        [OperationContract]
        List<AreaMessage> GetAreaMessages(string email, string password);

        [OperationContract]
        string GetDistrictName(string Email, string Password);

        [OperationContract]
        SchoolReport SortedSchoolSummary(dynamic Reports);

        [OperationContract]
        List<AreaOffice> GetAreas(string email, string password);

        [OperationContract]
        string GetAreaNumber(string email);

        [OperationContract]
        string DisplayDistrictName(string Email, string Password);

        [OperationContract]
        List<TempReport> GenerateDistrictQuarterReport(string email, string password, int quarter, int year);

        [OperationContract]
        List<TempReport> ExtendedQuarterReport(string P_No, string districtNo, string areaNo, string EMIS, int quarter, int year);
        [OperationContract]
        bool DisconnectSchool(string email, string password, string EMIS);

        [OperationContract]
        bool DisconnectArea(string email, string password, string AreaNumber);

        [OperationContract]
        List<ourArea> GetCircuitsByDistricts(string districtNo);
        [OperationContract]
        List<ourArea> GetCicuitsByProvince(string provincialNo);

        [OperationContract]
        List<ourDistrict> GetDistrictsByProvince(string provincialNo);

        [OperationContract]
        List<ourEntity> GetSchoolsByCircuit(string areaNo);


        [OperationContract]
        List<ourProvincial> GetAllProvinces();

        [OperationContract]
        ourProvincial GetProvinceByID(string P_No);
        //Logging functions
        [OperationContract]
        bool LogData(string email, string password, string metadata);

        [OperationContract]
        string TestFunction(string data);
    }
}
 