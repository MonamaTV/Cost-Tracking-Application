<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="School_Website.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ERROR | NSNP</title>
    <script src="script/script.js"></script>
    <script src="https://kit.fontawesome.com/a076d05399.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <link rel="stylesheet" href="css/main.css">

</head>
<body>
    <header class="myHeader shadow-sm">
        <nav class="navbar navbar-expand-lg ">
            <a class="navbar-brand" href="Login.aspx"><img src="images/North_West.png" alt=""></a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#myNavigation" aria-controls="myNavigation" aria-expanded="false" aria-label="Toggle navigation">
                <i class="fas fa-minus"></i>
                <i class="fas fa-minus"></i>
                <i class="fas fa-minus"></i>
            </button>

            <div class="collapse navbar-collapse" id="myNavigation">
                <ul class="navbar-nav mr-auto">
                    <li class="nav-item active">
                        <a class="nav-link" href="Login.aspx">Home</a>
                    </li>

                </ul>
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <a href="Profile.aspx" class="nav-link btnRegister"><i class="fas fa-cog"></i> Settings</a>
                    </li>
                </ul>
            </div>
        </nav>
    </header>

    <section class="errorpage">
        <div class="container">
            <div class="row">
                <div class="col-md-12 text-left">
                    <h2>The page you are trying to access may <br>have been removed permanently</h2>
                    <a href="Home.aspx">Return home</a>
                </div>
            </div>
        </div>
    </section>


</body>
</html>
