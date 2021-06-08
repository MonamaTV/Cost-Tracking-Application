<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Provincial.aspx.cs" Inherits="School_Website.Admin.Provincial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    NSNP | Details
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
         </asp:DropDownList>
        </div>
    <div class="row" id="cardsData" runat="server"> 
                
    </div>
  </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
