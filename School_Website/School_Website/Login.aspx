<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="School_Website.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login | NSNP</title>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <!-- Bootstrap CSS -->
    <script src="https://kit.fontawesome.com/a076d05399.js"></script>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="script/script.js"></script>
    <link rel="stylesheet" href="css/main.css" />
    <style>
        .nav {
           height: 20px !important;
           padding: 0;
        }
    </style>

</head>
<body>
    <header class="myHeader">
        <nav class="navbar navbar-expand-lg shadow-sm">
            <a class="navbar-brand" href="Login.aspx"><img src="images/North_West.png" alt=""/></a>
            

            
        
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <a href="Register.aspx" class="nav-link btnRegister"><i class="fas fa-sign-in-alt"></i> Register</a>
                    </li>
                </ul>
            
        </nav>
    </header>
    <form id="form1" runat="server">
        <section class="mainSection text-center">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    
                    <div class="login-form"> 
                        <div class="login-input">
                            <div class="alert alert-danger" role="alert" id="Alert" runat="server">
                                Either your email or password is incorrect
                            </div>
                        </div>
                        <div class="login-input">
                            <label for="username">Email</label>
                            <input type="email" name="username" id="username" runat="server" onkeyup="Try()"  />
                        </div>
                        <div class="login-input">
                            <label for="password">Password</label>
                            <input type="password" name="password" id="password"  runat="server" />
                        </div>
                        <div class="login-input">
                            <asp:Button Text="LOGIN" runat="server" class="aspButtons" ID="Signin" OnClick="Signin_Click"  />
                        </div>
                        <a href="ForgotPassword.aspx" class="forgot-password">Forgot password?</a>
                        <a href="Register.aspx" class="forgot-password"> | Register organization</a>
                    </div>
                </div>
            </div>
        </div>
    </section>
    </form>
</body>
</html>
