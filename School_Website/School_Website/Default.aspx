<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="School_Website.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Homepage | NSNP</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <!-- Bootstrap CSS -->
    <script src="https://kit.fontawesome.com/a076d05399.js"></script>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="script/script.js"></script>
    <link rel="stylesheet" href="css/main.css" />
    <style>
        .marg {
            margin-top: 5em !important;
        }
        .btnLogin {

        }
        .btnLogin, .btnRegisters {
            padding: .5em 2em !important;
            width: 30%;
        }
        .btnRegisters {
            background: #fff !important;
            color: #000 !important;
            border: 3px solid #000 !important;
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
    <div class="container marg">
        <div class="row">
            <div class="col-md-12 mt-5 ">
                <h3 class="text-center">Welcome to NSNP</h3>
                <p class="text-center">The aim is to make invoicing more convenient in this ever-growing space</p>
                <div class="login-input text-center">          
                    <a href="Login.aspx"  class="aspButtons submit btnLogin"  >Login</a>
                    <a href="Register.aspx"  class="aspButtons cancel btnRegisters">Register</a>   
                </div>
            </div>
        </div>
    </div>
</body>
</html>
