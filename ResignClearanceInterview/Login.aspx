<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ResignClearanceInterview.Login" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Login - AK Resign Clearance Interview Request Form</title>
  <link rel="shortcut icon" href="Assest/dist/img/logo.ico">
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
  <link rel="stylesheet" href="Assest/plugins/fontawesome-free/css/all.min.css">
  <link rel="stylesheet" href="Assest/plugins/icheck-bootstrap/icheck-bootstrap.min.css">
  <link rel="stylesheet" href="Assest/dist/css/adminlte.min.css">

<%--    ---------------Animated bg--%>

  <style>
      body 
      {

        background-image: url("Assest/dist/img/Resignation.jpg");
        /*background-image: url("Assest/dist/img/newbg.jpg");*/
        background-color: black;
        background-repeat: no-repeat;
        background-attachment: fixed;
        background-position: center; 
        background-repeat: no-repeat;
        background-size: 100% 100%;
    }

    /*#container 
    {
          animation: mymove 1s;
    }

    @keyframes mymove 
    {
          from {left: -300px;}
          to {left: 0px;}
    }
    .desg
    {
            animation: fadeInAnimations ease 2s; 
            animation-iteration-count: 1; 
            animation-fill-mode: forwards;
    }
    @keyframes fadeInAnimations 
   { 
            0% { 
                opacity: 0; 
            } 
            100% { 
                opacity: 1; 
            } 
   }
    
    .login-logo 
    { 
            animation: fadeInAnimation ease 5s; 
            animation-iteration-count: 1; 
            animation-fill-mode: forwards;
    } 
   @keyframes fadeInAnimation 
   { 
            0% { 
                opacity: 0; 
            } 
            100% { 
                opacity: 1; 
            } 
   }*/


  </style>
</head>
<body class="hold-transition login-page">

<form id="form1" runat="server">
    <div class="login-box">
      <div class="login-logo">
          <%--<img src="Assest/dist/img/lglogo.png" style="height: 100px; width: 100px; filter: opacity(100%);">--%>
          <h5 style="color:white;">Online</h5><h5 style="color:white;">Online Clerance Portal</h5>
      </div>
      <!-- /.login-logo -->
      <div class="card" id="container" style="background-color: #0000001a; border-radius: 50px 1px; margin-bottom: 50px;">
        <div class="card-body login-card-body" style="background-color: #0000001a; border-radius: 50px 1px; border-style: solid; border-color: #e8f0fe;">
          <p class="login-box-msg" style="color:White; font-size: 25px;">Sign in</p>
            <div class="input-group mb-3">
                <asp:TextBox ID="txt_EmployeeCode" runat="server" class="form-control" placeholder="Employee Code"></asp:TextBox>
              <div class="input-group-append">
                <div class="input-group-text">
                  <span class="fas fa-user" style="color: #e8f0fe;"></span>
                </div>
              </div>
            </div>
            <div class="input-group mb-3">
                <asp:TextBox ID="txt_EmployeePin" runat="server" class="form-control" placeholder="ESS Pin" TextMode="Password"></asp:TextBox>
              <div class="input-group-append">
                <div class="input-group-text">
                  <span class="fas fa-lock" style="color: #e8f0fe;"></span>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-8">
                  <p style="color:White; font-size: 15px;">Use ESS PIN Code</p>
              </div>
              <div class="col-4">
                  <asp:Button ID="btnLogin" runat="server" Text="Sign In" 
                      class="btn btn-primary btn-block" onclick="btnLogin_Click" style="background-color:#00000000; color:#e8f0fe; border-color:#e8f0fe; border-radius: 50px 0px;"/>
              </div>
            </div>
        </div>
      </div>
        <%--<p>Use this application on Google Chrome for the best experience.</p>--%>
    <marquee>
    <div class="col-12 desg" style="padding-left: 30px; color:white; padding-bottom: 100px; text-align:center;">
            <p style="color:#000000;">Use this application on Google Chrome for the best experience | Design & Develop by <b>BT Department</b>.</p>
    </div>
    </marquee>
    </div>
    
</form>

<script src="Assest/plugins/jquery/jquery.min.js"></script>
<script src="Assest/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
<script src="Assest/dist/js/adminlte.min.js"></script>
</body>
</html>

