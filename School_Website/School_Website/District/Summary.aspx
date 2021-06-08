<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="School_Website.District.Summary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
     Quarter Summary | NSNP
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <%--<link rel="stylesheet" href="../css/bootstrap.css" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="Manager" EnablePageMethods="true"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="ContentManager">
        <ContentTemplate>
            <br/>
            <h4 style="display: inline;" >Quarter <span id="displayMonth" runat="server"></span> Reports </h4>
            <div class="specify-area">
                <asp:DropDownList id="specify_area" runat="server" class="specify-schools-areas getAreas" OnSelectedIndexChanged="specify_area_SelectedIndexChanged" AutoPostBack="true">
                    
                </asp:DropDownList>  
                <input type="text" id="searchSchool" class="search-school-in-district" placeholder="Search school..."/>                  
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
            <script >
                let filterSchool = document.querySelectorAll('.getSchools option');
                const filterAreas = document.querySelector('.getAreas');
                filterAreas.addEventListener('change', () => {
                    let areas = document.querySelectorAll('.our-table tbody tr');
                    console.log(areas);
                });
                //
                
                let inputAct = document.querySelector('#searchSchool');
                let searchValue = document.querySelector('#searchSchool');
                let schools = document.querySelectorAll('tbody tr');
                //search live the school table
                inputAct.addEventListener('input', () => {
                    Array.from(schools).forEach((school) => {
                        let content = school.textContent.toLowerCase();
                        if (content.indexOf(searchValue.value.toLowerCase()) != -1) {
                            school.style.display = 'table-row';
                            console.log('found');
                        }
                        else {
                            school.style.display = 'none';
                        }
                    });
                });
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

