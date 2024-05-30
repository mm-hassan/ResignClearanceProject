<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PendingIntimation.aspx.cs" Inherits="ResignClearanceInterview.PendingIntimation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
        <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1></h1>
          </div>
   
        </div>
      </div><!-- /.container-fluid -->
    </section>



       <!-- Main content Request type start------------------------------------------------------------------------->
    <section class="content">
      
      <div class="card">
        <div class="card-header" style="background-color:#363940; color:white;">
          <h3 class="card-title">Pending Intimation</h3>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>
          </div>
        </div>
        <div class="card-body"> 
            <div class="card">
              <div class="card-body" style="width: 100%; height: 460px; overflow: auto;">
                  
                   <div class="row">
                      <div class="col-4 col-md-2 text-center">
                          <asp:Image ID="Image1" runat="server" alt="user-avatar" class="img-circle img-fluid" style="height:150px; width:200px;" ImageUrl="~/Assest/dist/img/logo.PNG" />
                    <%--  <img src="Assest/dist/img/user1-128x128.jpg" alt="user-avatar" class="img-circle img-fluid">--%>
                    </div>
                    <div class="col-8 col-md-5">
                        <span runat="server" id="lbl_RequestId" style="display:none;"></span>
                        <span runat="server" id="lbl_ConcernDep" style="display:none;"></span>
                         <p>Employee Code</p>
                         <asp:DropDownList ID="dd_Employees" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="dd_Employees_SelectedIndexChanged">
                                 
                         </asp:DropDownList>

                        <asp:TextBox ID="txt_Employeeno" runat="server" TextMode="Number" class="form-control form-control-sm" OnTextChanged="txt_Employeeno_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <p runat="server" id="lbl_MsgEmp" class="text-danger">.</p>
                        <br />
                      <p class="text-muted text-sm"><b>Department </b> <span runat="server" id="lbl_Department" style="color:#5960ff; font-weight: 900;">11-Feb-2022</span></p>
                      <p class="text-muted text-sm"><b>Designation </b> <span runat="server" id="lbl_Designation" style="color:#5960ff; font-weight: 900;">11-Feb-2022</span></p>
                      <p class="text-muted text-sm"><b>Email Address</b> <span runat="server" id="lbl_Email" style="color:#5960ff; font-weight: 900;">11-Feb-2022</span></p>
                      <p class="text-muted text-sm"><b>Phone No </b> <span runat="server" id="lbl_Phoneno" style="color:#5960ff; font-weight: 900;">11-Feb-2022</span></p>
                      
                    </div>
                      <div class="col-12 col-md-5">
                          <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="touchspin-inner">
                                    <p>Message</p>
                                    <asp:TextBox ID="txt_Message" runat="server" TextMode="MultiLine" class="form-control form-control-sm" Height="300">Your resignation request is pending due to some issues.</asp:TextBox>
                                </div>
                          </div> 
                           <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="touchspin-inner pt-1 text-right">
                                    
                                    <%--<asp:LinkButton ID="btn_Reject" runat="server" class="btn btn-sm bg-teal" OnClick="btn_Reject_Click" style="background-color: #363940!important;">
                                     <i class="fas fa-user"></i> Reject</asp:LinkButton>--%>
                                    <asp:LinkButton ID="btn_Sent" runat="server" class="btn btn-sm btn-primary" OnClick="btn_Sent_Click">
                                     <i class="fas fa-mail-bulk"></i> Sent Now</asp:LinkButton>
                                    <p runat="server" id="lbl_Msg">.</p>
                                </div>
                          </div>                       
                    </div>
                    
                  </div>

              </div>
            </div>
        </div>
        <div class="card-footer">

            <div class="row">
                <div class="col-8">
                    
                </div>
                <div class="col-4 text-right">
                    <asp:Label ID="lbl_GridMsg" runat="server" />
                </div>
            </div>

           
        </div>
      </div>
    </section>





  </div>

 
</asp:Content>
