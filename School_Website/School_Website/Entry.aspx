<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Entry.aspx.cs" Inherits="School_Website.Entry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    New Entry | NSNP
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="Manager" EnablePageMethods="true"></asp:ScriptManager>
   <%-- <asp:UpdatePanel runat="server" ID="ContentManager">--%>
        <ContentTemplate>
    <br />
    <h3>Monthly Register</h3>
    <div class="row">
        <div class="col-md-6">
            <div class="profile-form">      
                <div class="login-input">
                    <label for="months">Month</label>
                    <asp:DropDownList id="months" runat="server" class="dropMenu dropper text-left">
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
                    <input type="date" name="submission" id="submission" runat="server" class="sub">
                </div>
                <div class="login-input">
                    <label for="days">No. Feeding Days</label>
                    <input type="number" name="days" id="days" class="days" runat="server" min="1" max="31">
                </div>
                <div class="login-input">
                    <label for="Food">Food</label>
                    <input type="text" name="food" id="food" runat="server" class="food" >
                </div>
                <div class="login-input">
                    <label for="veges">Veges</label>
                    <input type="text" name="veges" id="veges"  runat="server" class="veges">
                </div>
                <div class="login-input">
                    <label for="avglearners">Avg Learners Fed</label>
                    <input type="text" name="avgLearners" id="avgLearners" runat="server" class="avgLearners"/>
                </div>
                 <div class="login-input">
                    <label for="gas">Gas/Wood</label>
                    <input type="text" name="gas" id="gas" runat="server" class="gas"/>
                </div>
                <div class="login-input">
                    <label for="honorarium">Honorarium/STIPEND</label>
                    <input type="text" name="honorarium" id="honorarium" runat="server" class="honorarium">
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="profile-form">  
                
                
                <div class="login-input">
                    <label for="charges">Bank Charges</label>
                    <input type="text" name="charges" id="charges" runat="server" class="changes">
                </div>
                <div class="login-input">
                    <label for="avgBudget">Budget</label>
                    <input type="text" name="avgBudget" id="avgBudget" runat="server" class="avgLearners"/>
                </div>
                <div class="login-input">
                    <label for="allocated">Allocated</label>
                    <input type="text" name="allocated" id="allocated" runat="server" class="allocated" />
                </div>
                <div class="login-input">
                    <label for="total">Total Spent</label>
                    <input type="text" name="total" id="total" runat="server" class="total" onclick="PopulateData()"/>
                </div>
                <div class="login-input">
                    <label for="balance">Balance</label>
                    <input type="text" name="balance" id="balance" runat="server" readonly class="balance" title="Balance will be calculated for you">
                </div>
                <div class="login-input">
                    <label for="closingBalance">Closing Balance</label>
                    <input type="text" name="closingBalance" id="closingBalance" runat="server" class="avgLearners"/>
                </div>
                <%--<div class="login-input" style="border-color: none !important;">
                    <input type="file" name="gas" id="bankings" runat="server" class="gas" accept="application/pdf"/>
                </div>--%>
                <div class="login-input" style="border-color: none !important;">
                    <asp:FileUpload ID="reportsFiles" runat="server" accept="application/pdf" />
                    <%--<INPUT type=file id=File1 name=File1 runat="server" />--%>
                </div>

                
                <div class="login-input">          
                    <asp:Button Text="Submit" runat="server" class="aspButtons submit" id="Submit" OnClick="Submit_Click" />
                    <asp:Button Text="Cancel" runat="server" class="aspButtons cancel" ID="Cancel" OnClick="Cancel_Click"  />    
                </div>
                

            </div>
        </div>
    </div>
    <script>
        
        entry.className = 'active';
   
        let food = document.querySelector('.food');
        let veges = document.querySelector('.veges');
        let honorarium = document.querySelector('.honorarium');
        let charges = document.querySelector('.changes');
        let allocated = document.querySelector('.allocated');
        let total = document.querySelector('.total');
        let balance = document.querySelector('.balance');
        console.log();


        function PopulateData() {
            total.value = parseInt(food.value) + parseInt(veges.value) + parseInt(honorarium.value) + parseInt(charges.value);
            balance.value = parseInt(allocated.value) - parseInt(total.value);
        }
        function alertUser() {
            alert("Fill all the blank spaces");
        }
        //comeback
    </script>
            </ContentTemplate>
   <%-- </asp:UpdatePanel>--%>
</asp:Content>
