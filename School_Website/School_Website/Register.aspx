<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="School_Website.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register | NSNP</title>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <!-- Bootstrap CSS -->
    <script src="https://kit.fontawesome.com/a076d05399.js"></script>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/main.css" />
</head>
<body>
    <header class="myHeader">
        <nav class="navbar navbar-expand-lg shadow-sm">
            <a class="navbar-brand" href="Login.aspx"><img src="images/North_West.png" alt="The logo of the province North West"/></a>
            <ul class="navbar-nav ml-auto">
                <li class="nav-item">
                    <a href="Login.aspx" class="nav-link btnRegister"><i class="fas fa-sign-in-alt"></i> Login</a>
                </li>
            </ul>
        </nav>
    </header>

    <form id="form1" runat="server">
        <section class="mainSection text-center">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="login-form" style="margin-top: 35px;">                     
                        <div class="login-input">
                            <label for="username">Organization No.</label>
                            <input type="text" name="entityno" id="entityno"  runat="server" required="required" placeholder="e.g. EMIS"/>
                        </div>
                        <div class="login-input">
                            <label for="username">Email</label>
                            <input type="email" name="username" id="username" runat="server" required="required" placeholder="Email" />
                        </div>  
                        <div class="login-input">
                            <label for="password">Password</label>
                            <input type="password" name="password" id="password" runat="server" required="required" placeholder="Password" />
                        </div>
                        <div class="login-input">
                            <label for="password">Confirm Password</label>
                            <input type="password" name="password1" id="password1" runat="server" required="required" placeholder="Re-enter your password" />
                        </div>
                        <div class="login-input">
                            <div class="alert alert-danger" role="alert" id="Alert1" runat="server">
                                Your passwords do not match.
                            </div>
                        </div>
                        <div class="login-input">
                            <label for="entityType">Organization Type</label>
                            <asp:DropDownList id="entityType" runat="server" class="dropMenu text-left">
                                <asp:ListItem Text="School" Value="1" />
                                <asp:ListItem Text="Circuit" Value="2" />
                                <asp:ListItem Text="District" Value="3" />
                                <asp:ListItem Text="Provincial" Value="4" />
                            </asp:DropDownList>
                        </div>
                        <div class="login-input">
                            <asp:Button Text="Register" runat="server" class="aspButtons" ID="Signup" OnClick="Signup_Click" />
                        </div> 
                        <div class="login-input">
                            <div class="alert alert-success" role="alert" id="ErrorAlert" runat="server">
                                Could not register your organization. Try again
                            </div>
                        </div>
                        <a href="Login.aspx" class="forgot-password">Already have account? Login</a>
                    </div>
                </div>
            </div>
        </div>
    </section>
    </form>
</body>
</html>
