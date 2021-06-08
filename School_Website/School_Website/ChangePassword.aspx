<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="School_Website.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password | NSNP</title>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <!-- Bootstrap CSS -->
    <script src="https://kit.fontawesome.com/a076d05399.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous" />
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
                                Either your email or token is incorrect
                            </div>
                        </div>
                         <div class="login-input">
                             <p>You are one step away from changing your password</p>
                         </div>
                        <br />
                        <div class="login-input">
                            <label for="username">Email</label>
                            <input type="email" name="username" id="username" runat="server"   />
                        </div>
                        <div class="login-input">
                            <label for="password">Password</label>
                            <input type="password" name="password" id="password" runat="server"  />
                        </div>
                        <div class="login-input">
                            <label for="password">Confirm Password</label>
                            <input type="password" id="password1" runat="server" />
                        </div>
                        <div class="login-input">
                          <asp:Button Text="Change Password" runat="server" class="aspButtons" ID="Change" OnClick="Change_Click"  />
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </section>
    </form>
</body>
</html>
