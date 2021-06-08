<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="AboutSchool.aspx.cs" Inherits="School_Website.AboutSchool" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                    <asp:ListItem Text="Allocated" Value="3" />
                    <asp:ListItem Text="Balance" Value="4" />
                    <asp:ListItem Text="Expenditure" Value="5" />
                   
                </asp:DropDownList>
            </div>

            <div class="row" id="cardsData" runat="server"> 
                
           </div>

            <script>
                //Developer Options for the developer who developed the project not you!
            </script>
     </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
