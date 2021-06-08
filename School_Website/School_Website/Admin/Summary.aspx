<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="School_Website.Admin.Summary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    NSNP | Quarter Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="Manager" EnablePageMethods="true"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="ContentManager">
        <ContentTemplate>
            <br/>
            <h4 style="display: inline;" >Reports for Quarter <span id="displayMonth" runat="server"></span> </h4>
            <div class="specify-area">
                <asp:DropDownList id="specify_districts" runat="server" class="specify-schools-areas getAreas" OnSelectedIndexChanged="specify_districts_SelectedIndexChanged"  AutoPostBack="true">
                     <asp:ListItem Value="0">All Districts</asp:ListItem>
                    
                </asp:DropDownList>  

                <asp:DropDownList id="specify_circuits" runat="server" class="specify-schools-areas getAreas" OnSelectedIndexChanged="specify_circuits_SelectedIndexChanged" AutoPostBack="true">
                     <asp:ListItem Value="0">All Circuits</asp:ListItem>
                     
                </asp:DropDownList> 
                <asp:DropDownList id="specify_schools" runat="server" class="specify-schools-areas getAreas" OnSelectedIndexChanged="specify_schools_SelectedIndexChanged" AutoPostBack="true">
                     <asp:ListItem Value="0"> All Schools</asp:ListItem>
                    
                </asp:DropDownList> 
    
                <input type="text" id="searchSchool" class="search-school-in-district" placeholder="Search..."/>                  
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table class="our-table schools-table summary-table report-summary-table ">
                        <thead>
                            <tr>
                                <th>EMIS</th>
                                <th>Name</th>
                                <th>No. Learners</th>                     
                                <th>Avg Learners Fed</th>                      
                                <th>Income Received</th>
                                <th>Total Expenditure</th>
                                <th>Budget</th> 
                            </tr>
                        </thead>
                        <tbody id="ReportsSummary" runat="server" >                        
                        </tbody>
                    </table>       
                    <h2 id="noReports" runat="server" class="text-center font-weight-bold"></h2>
                </div>
            </div>
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
