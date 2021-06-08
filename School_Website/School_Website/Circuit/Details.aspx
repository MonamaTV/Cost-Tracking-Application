<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="School_Website.views.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Summary | NSNP
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../css/main.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="Manager" EnablePageMethods="true"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="ContentManager">
        <ContentTemplate>
            <br/>
            <h4 style="display: inline;">Reports for </h4>
            <asp:DropDownList ID="schooltypes" runat="server" class="school-types"  OnSelectedIndexChanged="schooltypes_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value=4 Text="All"></asp:ListItem>
                <asp:ListItem Value=1 Text="Primary"></asp:ListItem>
                <asp:ListItem Value=2 Text="Secondary"></asp:ListItem>
                <asp:ListItem Value=3 Text="Special"></asp:ListItem>
            </asp:DropDownList>
            <div class="row">
                <div class="col-md-6">
                    <table class="our-table schools-table summary-table">
                        <thead>
                            <tr>
                                <th>Expenses</th>
                                <th>Costs</th>
                            </tr>
                        </thead>
                        <tbody id="CircuitInfo" runat="server">
                            <tr>
                                <td>Food</td>
                                <td id="food" runat="server"></td>
                            </tr>
                            <tr>
                                <td>Veges</td>
                                <td id="veges" runat="server"></td>
                            </tr>
                            <tr>
                                <td>Wood</td>
                                <td id="wood" runat="server"></td>
                            </tr>
                            <tr>
                                <td>Honorarium</td>
                                <td id="honorarium" runat="server"></td>
                            </tr>
                            <tr>
                                <td>Bank Charges</td>
                                <td id="charges" runat="server"></td>
                            </tr>
                            <tr>
                                <td>Avg Learners Fed</td>
                                <td id="avgLearners" runat="server"></td>
                            </tr>
                            <tr>
                                <td>No. of Days</td>
                                <td id="days" runat="server"></td>
                            </tr>
                            <!-- Summary of the table -->
                            <tr class="summary-tr">
                                <td class="summary" >Expenditure</td>
                                <td id="total" runat="server"></td>
                            </tr>
                            <tr class="summary-tr">
                                <td class="summary">Allocated</td>
                                <td  id="Areaallocated" runat="server"></td>
                            </tr>
                            <tr class="summary-tr">
                                <td class="summary" >Balance</td>
                                <td id="Areabalance" runat="server"></td>
                            </tr>
                        </tbody>
                    </table>
                  </div>
                <div class="col-md-6">
                    <div class="profile-form">
                        <%--<div class="login-input">
                            <label for="Submission Date">Submission Date</label>
                            <input type="text" name="submission" id="submission" runat="server" placeholder="yyyy/mm/dd">
                        </div>--%>
                        <div class="login-input">
                            <label for="charges">Bank Charges</label>
                            <input type="text" name="charges" id="areacharges" runat="server">
                        </div>
                        <div class="login-input">
                            <label for="Allocated">Allocated</label>
                            <input type="text" name="allcated" id="allocated" runat="server">
                        </div>
                        <div class="login-input">
                            <label for="Allocated">Expenditure</label>
                            <input type="text" name="expenditure" id="expenditure" runat="server">
                        </div>
                        <div class="login-input">
                            <label for="Allocated">Balance</label>
                            <input type="text" name="balance" id="balance" runat="server">
                        </div>
                        <div class="login-input">
                            <asp:Button Text="Submit Changes" runat="server" class="aspButtons submit" id="Submit" OnClick="Submit_Click" />
                            <asp:Button Text="Cancel" runat="server" class="aspButtons cancel" ID="Cancel" OnClick="Cancel_Click" /> 
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
          </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>
