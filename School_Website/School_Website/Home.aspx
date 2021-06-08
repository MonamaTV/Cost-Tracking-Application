<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="School_Website.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Manage Here | NSPN
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../css/main.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:ScriptManager runat="server" ID="Manager" EnablePageMethods="true"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="ContentManager">
        <ContentTemplate>
            <div class="filter">
                <asp:DropDownList id="sortby" runat="server" class=" text-left" OnSelectedIndexChanged="sortby_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Sort by" Enabled="false" />
                    <asp:ListItem Text="Month" Value="1" />
                    <asp:ListItem Text="Year" Value="2" />
                    <asp:ListItem Text="Allocated ASC" Value="3" />
                    <asp:ListItem Text="Allocated DESC" Value="4" />
                    <asp:ListItem Text="Balance ASC" Value="5" />
                    <asp:ListItem Text="Balance DESC" Value="6" />
                    <asp:ListItem Text="Expenditure ASC" Value="7" />
                    <asp:ListItem Text="Expenditure DESC" Value="8" />
                </asp:DropDownList>
            </div>

            <div class="row" id="cardsData" runat="server"> 
                
           </div>

            <script>
                home.className = 'active';
                const sideMenu = document.querySelector('.side-menu');

                sideMenu.addEventListener('click', () => {
                    if (sideMenu.style.width === '100vw') {
                        sideMenu.style.width = '20vw';
                    }
                    else {
                        console.log(sideMenu.style.width);
                    }
                });

                
                let deletebtn = document.querySelector('.row');
                //gets called in the aspx.cs file
                function deleteRecord(ID) {
                    console.log(ID);
                    if (confirm('Are you sure you want to delete this record?')) {
                        window.location.href = `Home.aspx?Delete=true&deleteID=${ID}`;
                    }
                    else {                    
                        window.location.href = 'Home.aspx';
                    }
                }
               // deletebtn.addEventListener('click', deleteRecord);
            </script>
     </ContentTemplate>
 </asp:UpdatePanel>
        
</asp:Content>
