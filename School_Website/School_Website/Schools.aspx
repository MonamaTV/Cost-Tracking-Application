<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Schools.aspx.cs" Inherits="School_Website.Schools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h3 id="organization" runat="server">Schools</h3>
    <div class="input-search">
        <input type="text" id="searchSchool" class="searchSchool" placeholder="Search entity..."/>
    </div>
    <div class="row">
        <table class="schools-table our-table">
            <thead>
                <tr>
                    <th>No.</th>
                    <th id="enitityNo" runat="server">EMIS</th>
                    <th>Name</th>
                    <th id="manager" runat="server">Principal Name</th>
                    <th>Telephone</th>
                    <th>Cellphone</th>
                    <th id="numLearners" runat="server">No. Learners</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody id="schoolsInfo" runat="server">
                
            </tbody>
        </table>
    </div>
    <script>
        school.className = 'active'; //
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

        var disconnectFunction = (number, name) => {
            if (confirm(`Are you sure you want to disconnnet ${name}?`)) {
                window.location.href = `Schools.aspx?disconnet=${number}`;
            }
            else {
                
            }
        }

        var disconnectSchool = (EMIS) => {
            confirm.name = "NSPN";
            if (confirm("Do you want to disconnect this school?")) {
                window.location.href = `Schools.aspx?disconnectschools=${EMIS}`;
            }
            else {
                window.location.href = `Schools.aspx?`;
            }
        }
        
        
    </script>
</asp:Content>
