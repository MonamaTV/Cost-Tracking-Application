<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="School_Website.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Report Details | NSNP
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h3 id="titleRegister" runat="server">Register for </h3>
    <asp:Button Text="Download report" runat="server" class="download-file-name" id="downloadReport" OnClick="downloadReport_Click" /> 
        <div class="row">
        <div class="col-md-6">
            <div class="profile-form">      
                <div class="login-input">
                    <label for="months">Month</label>
                    <asp:DropDownList id="months" runat="server" class="dropMenu dropper text-left" >
                        <asp:ListItem Text="January" Value="1" />
                        <asp:ListItem Text="February" Value="2" />
                        <asp:ListItem Text="March" Value="3" />
                        <asp:ListItem Text="April" Value="4" />
                        <asp:ListItem Text="May" Value="5" />
                        <asp:ListItem Text="June" Value="6" />
                        <asp:ListItem Text="July" Value="7" />
                        <asp:ListItem Text="August" Value="8" />
                        <asp:ListItem Text="September" Value="9" />
                        <asp:ListItem Text="October" Value="10" />
                        <asp:ListItem Text="November" Value="11" />
                        <asp:ListItem Text="December" Value="12" />
                    </asp:DropDownList>
                </div>
                <div class="login-input">
                    <label for="date">Submission Date</label>
                    <input type="text" name="submission" id="submission" runat="server" readonly>
                </div>
                <div class="login-input">
                    <label for="days">No. Feeding Days</label>
                    <input type="number" name="days" id="days" runat="server" min="1" max="31">
                </div>
                <div class="login-input">
                    <label for="Food">Food</label>
                    <input type="text" name="food" id="food" runat="server">
                </div>
                <div class="login-input">
                    <label for="veges">Veges</label>
                    <input type="text" name="veges" id="veges"  runat="server">
                </div>
                <div class="login-input">
                    <label for="avgLearneres">Avg Learners Fed</label>
                    <input type="text" name="avgLearners" id="avgLearners" runat="server">
                </div>
                <div class="login-input">
                    <label for="gas">Gas/Wood</label>
                    <input type="text" name="gas" id="gas" runat="server">
                </div>             
            </div>
        </div>
        <div class="col-md-6">
            <div class="profile-form">
                
                <div class="login-input">
                    <label for="honorarium">Honorarium/STIPEND</label>
                    <input type="text" name="honorarium" id="honorarium" runat="server">
                </div>
                <div class="login-input">
                    <label for="charges">Bank Charges</label>
                    <input type="text" name="charges" id="charges" runat="server">
                </div>
                <div class="login-input">
                    <label for="avgBudget">Budget</label>
                    <input type="text" name="avgBudget" id="avgBudget" runat="server" class="avgLearners"/>
                </div>
                <div class="login-input">
                    <label for="allocated">Allocated</label>
                    <input type="text" name="allocated" id="allocated" runat="server" />
                </div>
                <div class="login-input">
                    <label for="total">Total/Spent</label>
                    <input type="text" name="total" id="total" runat="server">
                </div>
                <div class="login-input">
                    <label for="balance">Balance</label>
                    <input type="text" name="balance" id="balance" runat="server" readonly>
                </div>
                <div class="login-input">
                    <label for="closingBalance">Closing Balance</label>
                    <input type="text" name="closingBalance" id="closingBalance" runat="server" class="avgLearners"/>
                </div>            
                <div class="login-input">          
                    <asp:Button Text="Submit Changes" runat="server" class="aspButtons submit" id="Submit" OnClick="Submit_Click" />
                    <asp:Button Text="Cancel" runat="server" class="aspButtons cancel" ID="Cancel" OnClick="Cancel_Click"  />    
                </div>
                <div class="login-input">
                    <div class="alert alert-success" role="alert" id="Alert" runat="server">
                        Successfully submitted changes
                    </div>
                </div>
                <div class="login-input">
                    <div class="alert alert-danger" role="alert" id="ErrorAlert" runat="server">
                        Could not add changes. Try again!
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
