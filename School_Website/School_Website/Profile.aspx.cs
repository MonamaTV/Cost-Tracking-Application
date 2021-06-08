using School_Website.SchoolService;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace School_Website
{
    public partial class Profile : System.Web.UI.Page
    {
        readonly NSNPServiceClient service = new NSNPServiceClient();
        private bool CheckLoggedIn()
        {
            if (Session["Email"] != null && Session["Password"] != null && Session["Level"] != null)
                return true;
            else return false;
        }
        private string GetAreaName(dynamic areas, string code)
        {
            string name = "";
            foreach (var area in areas)
            {
                if(area.A_No.Equals(code))
                {
                    return area.A_Name;
                }
            }
            return name;
        }
        private string GetDistrictName(dynamic areas, string code)
        {
            string name = "";
            foreach (var dis in areas)
            {
                if (dis.D_No.Equals(code))
                {
                    return dis.D_Name;
                }
            }
            return name;
        }
        private string GetProvinceName(dynamic prov, string code)
        {
            string name = "";
            foreach (var dis in prov)
            {
                if (dis.P_No.Equals(code))
                {
                    return dis.P_Name;
                }
            }
            return name;
        }
        private void DisplayProvinces(string code = "0")
        {
            dynamic provinces = service.GetAllProvinces();
            if (provinces == null) return;
            if (code != null && !code.Equals("0"))
            {
                dbprovince.Items.Add(new ListItem(GetProvinceName(provinces, code), code));
                foreach (var prov in provinces)
                {
                    if (!code.Equals(prov.P_No))
                        dbprovince.Items.Add(new ListItem(prov.P_Name, prov.P_No));
                }

            }
            else
            {
                dbprovince.Items.Add(new ListItem("Choose Province", "0"));
                foreach (var prov in provinces)
                {
                    dbprovince.Items.Add(new ListItem(prov.P_Name, prov.P_No));
                }
                
            }
                


            
        }
        private void DisplayAreas(dynamic ourAreas, string code)
        {
            codeName.Items.Clear();
            if (ourAreas != null)
            {
                if (code == null || code.Equals("0"))
                {
                    codeName.Items.Add(new ListItem("Choose Circuit", "0"));
                }
                else
                {
                    codeName.Items.Add(new ListItem(GetAreaName(ourAreas, code), code));
                }
                foreach (ourArea area in ourAreas)
                {
                    if(!area.A_No.Equals(code))
                        codeName.Items.Add(new ListItem(area.A_Name, area.A_No));
                }
            }
            else codeName.Items.Add(new ListItem("", ""));
        }
        
        private void DisplayDistricts(dynamic ourDistricts, string code)
        {
            if(ourDistricts != null)
            {
                if(code == null)
                {
                    codeName.Items.Add(new ListItem("Choose District", ""));
                }
                else
                {
                    codeName.Items.Add(new ListItem(GetDistrictName(ourDistricts, code), code));
                }
                foreach (ourDistrict district in ourDistricts)
                {
                    if (!district.D_No.Equals(code))
                        codeName.Items.Add(new ListItem(district.D_Name, district.D_No));
                }
            }
            else
            {
                codeName.Items.Add(new ListItem("", ""));
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Active"] = 2;

            if (!CheckLoggedIn())
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                string email = Session["Email"].ToString();
                string passwords = Session["Password"].ToString();
                if (Session["Level"].ToString().Equals("1"))
                {
                    school_types.Visible = true;
                    budgetInput.Visible = true;
                    lblProvince.Visible = true;
                    dbprovince.Visible = true;
                    
                    ourEntity school = service.GetSchool(email, passwords);
                    DisplayProvinces(school.P_No);
                    if (school != null)
                    {
                        entityno.Value = school.EMIS;
                        name.Value = school.S_Name;
                        school_types.SelectedIndex = Convert.ToInt32(school.S_Type) - 1;
                        Email.Value = school.S_Email;
                        numLearner.Value = school.S_No_learners;
                        dynamic areas = service.GetCicuitsByProvince(dbprovince.SelectedValue);
                        if (school.A_No != null && !school.A_No.Equals(""))
                        {
                            DisplayAreas(areas, school.A_No);
                        }
                        else
                        {
                            DisplayAreas(areas, "0");
                        }
                        principalName.Value = school.S_PrincipalName;
                        cellphone.Value = school.S_CellPhone;
                        telephone.Value = school.S_TellNumber;
                        budget.Value = Convert.ToString(school.S_Budget);
                        password.Value = passwords;
                        password1.Value = passwords;
                    }
                    else Response.Redirect("Login.aspx");
                }
                if (Session["Level"].ToString().Equals("2"))
                {
                    lblManagerName.InnerText = "Manager Name";
                    lblManagerPhone.InnerText = "Manager Cellphone";
                    lblLearners.InnerText = "No. of Registered Schools";
                    numLearner.Disabled = true;
                    lblArea.InnerText = "District Office";

                    ourArea area = service.GetArea(email, passwords);
                    if (area != null)
                    {
                        entityno.Value = area.A_No;
                        name.Value = area.A_Name;
                        Email.Value = area.A_Email;
                        numLearner.Value = Convert.ToString(area.A_NumSchools);
                        DisplayDistricts(service.GetDistrictsForArea(), area.D_No);
                        principalName.Value = area.A_Manager;
                        cellphone.Value = area.A_Cellphone;
                        telephone.Value = area.A_Tellphone;
                        password.Value = passwords;
                        password1.Value = passwords;
                    }
                    else Response.Redirect("Login.aspx");
                }
                if (Session["Level"].ToString().Equals("3"))
                {
                    lblManagerName.InnerText = "Manager Name";
                    lblManagerPhone.InnerText = "Manager Cellphone";
                    lblLearners.InnerText = "No. of Areas";
                    lblArea.InnerText = "Provicial Office";
                    lblProvince.Visible = true;
                    dbprovince.Visible = true;

                    ourDistrict district = service.GetDistrict(email, passwords);
                    DisplayProvinces(district.P_No ?? "0");
                    if (district != null)
                    {
                        entityno.Value = district.D_No;
                        name.Value = district.D_Name;
                        Email.Value = district.D_Email;
                        numLearner.Value = Convert.ToString(district.D_NumAreas);
                        codeName.Visible = false;
                        principalName.Value = district.D_Manager;
                        cellphone.Value = district.D_Cellphone;
                        telephone.Value = district.D_Tellphone;
                        password.Value = passwords;
                        password1.Value = passwords;
                    }
                    else Response.Redirect("Login.aspx");
                }
                if(Session["Level"].ToString().Equals("4"))
                {
                    lblManagerName.InnerText = "Manager Name";
                    lblManagerPhone.InnerText = "Manager Cellphone";
                    lblLearners.Visible = false;
                    numLearner.Visible = false;
                    lblArea.Visible = false;

                    ourProvincial province = service.GetProvince(email, passwords);
                    if(province != null)
                    {
                        entityno.Value = province.P_No;
                        name.Value = province.P_Name;
                        Email.Value = province.P_Email;
                        numLearner.Value = Convert.ToString(province.Num_Areas);
                        codeName.Visible = false;
                        principalName.Value = province.p_Manageer;
                        cellphone.Value = province.P_Cellphone;
                        telephone.Value = province.P_Tellphone;
                        password.Value = passwords;
                        password1.Value = passwords;
                    }
                    
                }
            }
            else
            {
                if(Session["Level"].ToString().Equals("1"))
                {
                    
                    if (dbprovince.SelectedValue != "0" && codeName.SelectedValue == "0")
                    {
                        dynamic areas = service.GetCicuitsByProvince(dbprovince.SelectedValue);
                        if (areas == null) return;
                        DisplayAreas(areas, "0");
                    }
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {

            try
            {
                if (Session["Level"].ToString().Equals("1"))
                {
                    ourEntity entity = new ourEntity
                    {
                        A_No = Convert.ToString(codeName.SelectedValue) ?? "",
                        P_No = Convert.ToString(dbprovince.SelectedValue) ?? "",
                        EMIS = entityno.Value,
                        S_Name = name.Value,
                        S_Type = Convert.ToInt32(school_types.SelectedValue),
                        S_Email = Email.Value,
                        S_No_learners = VerifyNumber(numLearner.Value),
                        S_PrincipalName = principalName.Value,
                        S_CellPhone = cellphone.Value,
                        S_TellNumber = telephone.Value,
                        S_Budget = Convert.ToDouble((!budget.Value.Equals("") ? budget.Value : "0"))
                    };

                    if (!password.Value.Equals("") && !password1.Value.Equals(""))
                    {
                        if (password.Value.Equals(password1.Value))
                        {
                            if (service.EditSchool(entity, entity.A_No, entity.P_No, Session["Password"].ToString(), password.Value) == true)
                            {
                                Session["Password"] = password.Value;

                                Alert.Attributes.Add("class", "alert alert-success profile-alert show-alert");
                            }
                            else
                            {

                                ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
                            }
                        }
                        else
                        {

                            ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
                        }
                    }
                    else
                    {
                        if (service.EditSchool(entity, entity.A_No, entity.P_No, Session["Password"].ToString(), Session["Password"].ToString()) == true)
                        {
                            Alert.Attributes.Add("class", "alert alert-success profile-alert show-alert");
                        }
                        else
                        {
                            ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
                        }
                    }
                }
                if (Session["Level"].ToString().Equals("2"))
                {
                    ourArea area = new ourArea()
                    {
                        A_Name = name.Value,
                        A_No = entityno.Value,
                        A_Cellphone = cellphone.Value,
                        A_Tellphone = telephone.Value,
                        A_Email = Convert.ToString(Email.Value),
                        A_Manager = principalName.Value,
                        D_No = codeName.SelectedValue
                    };

                    if (!password.Value.Equals("") && !password1.Value.Equals(""))
                    {
                        if (password.Value == password1.Value || password.Value.Equals(password1.Value))
                        {
                            if (service.EditArea(area, area.D_No, password.Value, Session["Password"].ToString()))
                            {
                                Session["Password"] = password.Value;
                                Alert.Attributes.Add("class", "alert alert-success profile-alert show-alert");
                            }
                            else ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
                        }
                        else ErrorMessage("New passwords do not match");
                    }
                    if (service.EditArea(area, area.D_No, Session["Password"].ToString(), Session["Password"].ToString()))
                    {
                        Alert.Attributes.Add("class", "alert alert-success profile-alert show-alert");
                    }
                    else ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
                }
                if (Session["Level"].ToString().Equals("3"))
                {
                    ourDistrict district = new ourDistrict()
                    {
                        D_Cellphone = cellphone.Value,
                        D_Email = Email.Value,
                        D_Manager = principalName.Value,
                        D_Name = name.Value,
                        D_Tellphone = telephone.Value,
                        D_No = entityno.Value,
                        P_No = dbprovince.SelectedValue
                    };
                    if (!password.Value.Equals("") && !password1.Value.Equals(""))
                    {
                        if (password.Value.Equals(password1.Value) || password1.Value == password.Value)
                        {
                            if (service.EditDistrict(district, district.P_No, password.Value, Session["Password"].ToString()))
                            {
                                Session["Password"] = password.Value;
                                Alert.Attributes.Add("class", "alert alert-success profile-alert show-alert");
                            }
                            else ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
                        }
                        else ErrorMessage("New passwords do not match");
                    }
                    if (service.EditDistrict(district, district.P_No, Session["Password"].ToString(), Session["Password"].ToString()))
                    {
                        Alert.Attributes.Add("class", "alert alert-success profile-alert show-alert");
                    }
                    else ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
                }
                if(Session["Level"].ToString().Equals("4"))
                {
                    ourProvincial provincial = new ourProvincial()
                    {
                        P_Cellphone = cellphone.Value,
                        P_Email = Email.Value,
                        p_Manageer = principalName.Value,
                        P_Tellphone = telephone.Value,
                        P_Name = name.Value,
                        P_No = entityno.Value
                    };
                    if (!password.Value.Equals("") && !password1.Value.Equals(""))
                    {
                        if (password.Value.Equals(password1.Value) || password1.Value == password.Value)
                        {
                            if (service.EditProvince(provincial, password.Value, Session["Password"].ToString()))
                            {
                                Session["Password"] = password.Value;
                                Alert.Attributes.Add("class", "alert alert-success profile-alert show-alert");
                            }
                            else ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
                        }
                        else ErrorMessage("New passwords do not match");
                    }
                    if (service.EditProvince(provincial, Session["Password"].ToString(), Session["Password"].ToString()))
                    {
                        Alert.Attributes.Add("class", "alert alert-success profile-alert show-alert");
                    }
                    else ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
                }
            }
            catch (Exception eq)
            { 
                ErrorAlert.Attributes.Add("class", "alert alert-danger profile-alert show-alert");
            }
        }
        private void ErrorMessage(string message)
        {
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "MessageBox", "<script language='javascript'>alert('" + message + "');</script>");
        }
        private string VerifyNumber(string number)
        {
            string value = "0";
            try
            {
                var num = Convert.ToDouble(number);
                value = Convert.ToString(num);
            }
            catch (Exception)
            {
                return value;
            }
            return value;
        }
    }
}