<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="School_Website.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Monthly Register</h3>
    <div class="row">
        <div class="col-md-6">
            <div class="profile-form">
                <div class="login-input">
                    <label for="month">Month</label>
                    <input type="text" name="month" id="month" runat="server">
                </div>
                <div class="login-input">
                    <label for="date">Submission Date</label>
                    <input type="date" name="date" id="date" runat="server" >
                </div>
                <div class="login-input">
                    <label for="days">No. Feeding Days</label>
                    <input type="number" name="days" id="days" runat="server">
                </div>
                <div class="login-input">
                    <label for="Food">Food</label>
                    <input type="text" name="food" id="food" runat="server">
                </div>

                <div class="login-input">
                    <label for="veges">Veges</label>
                    <input type="text" name="veges" id="veges" runat="server">
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="profile-form">                   
                <div class="login-input">
                    <label for="gas">Gas/Wood</label>
                    <input type="text" name="gas" id="gas">
                </div>
                <div class="login-input">
                    <label for="honorarium">Honorarium/STIPEND</label>
                    <input type="text" name="honorarium" id="honorarium" runat="server">
                </div>
                <div class="login-input">
                    <label for="charges">Bank Charges</label>
                    <input type="text" name="charges" id="charges" runat="server">
                </div>
                <div class="login-input">
                    <label for="total">Total</label>
                    <input type="text" name="total" id="total" runat="server">
                </div>
                <div class="login-input">
                    <label for="balance">Balance</label>
                    <input type="text" name="balance" id="balance" runat="server">
                </div>
                <div class="login-input">
                    <button id="submit">Submit</button>
                    <button id="cancel">Cancel</button>
                </div>             
            </div>
        </div>
    </div>
</asp:Content>
