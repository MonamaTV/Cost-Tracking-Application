    <%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="School_Website.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    NSPN | Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h3>Your Organization</h3>
    <asp:ScriptManager runat="server" ID="ProfileManager" EnablePageMethods="true"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="UpdateManager">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-6">
                     <div class="profile-form">          
                        <div class="login-input">
                            <label for="entityno">Organization No.</label>
                            <input type="text" name="entityno" id="entityno" readonly runat="server" title="Not changeable">
                        </div>
                        <div class="login-input">
                            <label for="name">name</label>
                            <input type="text" name="name" id="name" runat="server">
                        </div>
                          <asp:DropDownList id="school_types" runat="server" class="dropMenu dropper text-left" visible="false">
                                <asp:ListItem Text="Primary" Value="1" />
                                <asp:ListItem Text="Secondary" Value="2" />
                                <asp:ListItem Text="Special" Value="3" />
                            </asp:DropDownList>
                        <div class="login-input">
                            <label for="email">Email</label>
                            <input type="email" name="email" id="Email" readonly runat="server" title="Not changeable">
                        </div>
                        <div class="login-input changePassword ">
                            <h6 class="font-weight-bold">Change Password</h6>
                            <small class="font-italic">Use this option only when you want to change your password</small>
                            <div class="login-input">
                                 <label for="password">Password</label>
                                 <input type="password" name="password" id="password" runat="server" class="firstPassword">
                            </div>
                             <p class="mx-1 text-danger my-0 errorMessage" style="display: none;">Your passwords do not match</p>
                            <div class="login-input">
                               <label for="email">Confirm Password</label>
                              <input type="password" name="password" id="password1" runat="server" class="secondPassword">
                            </div>     
                        </div>                  
                     </div>
                </div>
                <div class="col-md-6">  
                    <div class="profile-form">  
                        <div class="login-input">
                            <label for="principalname" id="lblManagerName" runat="server">Principal Name</label>
                            <input type="text" name="name" id="principalName" runat="server">
                        </div>
                        <div class="login-input">
                            <label for="numLearner" id="lblLearners" runat="server">No. of Learners</label>
                            <input type="text" name="numLearner" id="numLearner" runat="server">
                        </div>
                        <div class="login-input">
                            <label for="area" id="lblProvince" runat="server" visible="false">Province</label>                           
                           <%--<input type="text" name="area" id="areaCode" runat="server" placeholder="Enter the no. if it has been registered">--%>
                            <asp:DropDownList ID="dbprovince" runat="server" Visible="false" class="dropMenu dropper text-left" AutoPostBack="true">
                                
                            </asp:DropDownList>
                        </div>
                        <div class="login-input">
                            <label for="area" id="lblArea" runat="server">Circuit Office</label>                           
                           <%--<input type="text" name="area" id="areaCode" runat="server" placeholder="Enter the no. if it has been registered">--%>
                            <asp:DropDownList ID="codeName" runat="server" class="dropMenu dropper text-left" AutoPostBack="true">
                                
                            </asp:DropDownList>
                        </div>
                        <div class="login-input">
                            <label for="number" id="lblManagerPhone" runat="server" >Principal Number</label>
                            <input type="text" name="cellphone" id="cellphone" runat="server">
                        </div>
                        <div class="login-input">
                            <label for="number" id="lblTellphone" runat="server">Telephone Number</label>
                            <input type="text" name="telephone" id="telephone" runat="server">
                        </div>
                        <div class="login-input" id="budgetInput" runat="server" visible="false">
                            <label for="budget" id="lblBudget" runat="server">Avg Monthly Budget</label>
                            <input type="text" name="budget" id="budget" runat="server">
                        </div>
                        <div class="login-input">
                            <asp:Button Text="Submit Changes" runat="server" ID="Submit" OnClick="Submit_Click" class="aspButtons changes"  />
                        </div>
                        <div class="login-input">
                            <div class="alert alert-success" role="alert" id="Alert" runat="server">
                                Successfully submitted changes
                            </div>
                        </div>
                        <div class="login-input">
                            <div class="alert alert-danger" role="alert" id="ErrorAlert" runat="server">
                                Something happened. Try again...
                            </div>
                        </div>
                     </div>
                </div>
            </div>
            <script>
                profile.className = 'active';

                const firstPassword = document.querySelector('.firstPassword');
                const secondPassword = document.querySelector('.secondPassword');
                const secondPasswordFunction = document.querySelector('.secondPassword');
                const errorMessage = document.querySelector('.errorMessage');

                const verifyPassword = () => {
                    if (firstPassword.value !== secondPassword.value) {
                        errorMessage.style.display = 'block';
                    }
                    else {
                        errorMessage.style.display = 'none';
                    }
                }
                secondPasswordFunction.addEventListener('input', verifyPassword);

            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
