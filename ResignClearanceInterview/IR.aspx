<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="IR.aspx.cs" Inherits="ResignClearanceInterview.IR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--   <style>
        .starempty
        {
            background-image: url(Assest/dist/img/RatingControl/starempty.png);
            width:50px;
            height:50px;
        }
        .starfilled
        {
            background-image: url(Assest/dist/img/RatingControl/starfilled.png);
           width:50px;
            height:50px;
        }
        .starwaiting
        {
            background-image: url(Assest/dist/img/RatingControl/starwaiting.png);
            width:50px;
            height:50px;
        }
    </style>--%>
     <style>
       input[type="file"]::-webkit-file-upload-button {
            color: #666; /* or any other color */
            outline: none;
            border: none;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
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

      <section class="content">
      <!-- Default box -->
      <div class="card card-solid">
        <div class="card-body pb-0">
          <div class="row d-flex align-items-stretch">
            <div class="col-12 col-sm-6 col-md-12 d-flex align-items-stretch">
              <div class="card bg-light w-100">
                <div class="card-header text-muted border-bottom-0">
                 <%--<p class="text-center">Employee Information</p>--%>
                </div>
                <div class="card-body pt-0">
                  <div class="row">
                      <div class="col-4 col-md-2 text-center">
                          <asp:Image ID="Image1" runat="server" alt="user-avatar" class="img-circle img-fluid" style="height:150px; width:200px;" ImageUrl="~/Assest/dist/img/logo.PNG" />
                    <%--  <img src="Assest/dist/img/user1-128x128.jpg" alt="user-avatar" class="img-circle img-fluid">--%>
                    </div>
                    <div class="col-8 col-md-5">
                        <asp:TextBox ID="txt_Employeecode" runat="server" CssClass="form-control" placeholder="Employee Code" AutoPostBack="True" OnTextChanged="txt_Employeecode_TextChanged"></asp:TextBox>
                      <h2 class="lead" runat="server" id="lbl_EmployeeName" style="color:#5960ff; font-weight: 900; text-decoration: underline;"><b>Hafiz Muhammad Ismail</b></h2>
                      <p runat="server" id="lbl_Designation">Asst. Manager</p>
                      <%--<h2 class="lead" runat="server" id="lbl_Designation"><b>Asst. Manager</b></h2>--%>
                      <p class="text-muted text-sm"><b>Date of Joining: </b> <span runat="server" id="lbl_DateofJoining" style="color:#5960ff; font-weight: 900;">11-Feb-2022</span></p>
                      <ul class="ml-4 mb-0 fa-ul text-muted">
                        <li class="small"><span class="fa-li"><i class="fas fa-lg fa-building"></i></span>Address: <span runat="server" id="lbl_Address" style="color:#5960ff; font-weight: 900;">FS 79/5 Malir Colony Karachi.</span></li>
                        <li class="small"><span class="fa-li"><i class="fas fa-lg fa-phone"></i></span> Phone #: <span runat="server" id="lbl_PhoneNo" style="color:#5960ff; font-weight: 900;">923132014201</span></li>
                      </ul>
                    </div>
                      <div class="col-12 col-md-5">
                      <%--<h2 class="lead" runat="server" id=""><b>Hafiz Muhammad Ismail</b></h2>--%>
                    <%--  <h2 class="lead text-right p-0 m-0" runat="server" id="ss"><b></b> <span runat="server" id="lbl_HodName" style="color:#5960ff; font-weight: 900; text-decoration: underline;"><b>Irfan Ahmed Khan</b></span></h2>
                      --%>
                      <h2 class="lead text-right p-0 m-0" runat="server" id="ss"><b>MANAGER : </b> <span runat="server" id="lbl_Linemanager" style="color:#5960ff; font-weight: 900; text-decoration: underline;"><b>MANAGER</b></span></h2>
                      <h2 class="lead text-right p-0 m-0" runat="server" id="H1"><b>HOD : </b>  <span runat="server" id="lbl_HodName" style="color:#5960ff; font-weight: 900; text-decoration: underline;"><b>HOD</b></span></h2>
                      <p runat="server" id="lbl_Department" class="text-right p-0 m-0">Bussiness Technology</p>
                      <%--<h2 class="lead" runat="server" id="lbl_Designation"><b>Asst. Manager</b></h2>--%>
                      <%--<p class="text-muted text-sm"><b>Date of Joining: </b> <span runat="server" id="Span2">11-Feb-2022</span></p>
                      <ul class="ml-4 mb-0 fa-ul text-muted">
                        <li class="small"><span class="fa-li"><i class="fas fa-lg fa-building"></i></span>Address: <span runat="server" id="Span3">FS 79/5 Malir Colony Karachi.</span></li>
                        <li class="small"><span class="fa-li"><i class="fas fa-lg fa-phone"></i></span> Phone #: <span runat="server" id="Span4">923132014201</span></li>
                          <li class="small"><span class="fa-li"><i class="fas fa-lg fa-phone"></i></span> Phone #: <span runat="server" id="Span5">923132014201</span></li>
                      </ul>--%>
                    </div>
                    
                  </div>
                </div>
               <%-- <div class="card-footer">
                  <div class="text-right">
                    <a href="#" class="btn btn-sm bg-teal">
                      <i class="fas fa-comments"></i>
                    </a>
                    <a href="#" class="btn btn-sm btn-primary">
                      <i class="fas fa-user"></i> View Profile
                    </a>
                  </div>
                </div>--%>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- /.card -->
    </section>




    <!-- Main content Request type start------------------------------------------------------------------------->
    <section class="content">
      <div class="card">
        <div class="card-header" style="background-color:#363940; color:white;">
          <h3 class="card-title">Request type <asp:Label ID="lbl_TypeError" runat="server" class="lbl_error"></asp:Label></h3>
          <div class="card-tools">
              <%--<button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>--%>           <%-- <button type="button" class="btn btn-tool" data-card-widget="remove" title="Remove">
              <i class="fas fa-times"></i>
            </button>--%>
          </div>
        </div>
        <div class="card-body">
            <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="sparkline15-list mg-t-30">
                            <div class="sparkline15-graph">
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 col-6">
                                        <div class="touchspin-inner">
                                             <label>Resign Type</label>
                                             <asp:DropDownList ID="dd_ResignType" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="dd_ResignType_SelectedIndexChanged">
                                                <asp:ListItem>Select</asp:ListItem>
                                             </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 col-6">
                                        <div class="touchspin-inner">
                                              <label>Last Duty Date</label>
                                              <asp:TextBox ID="txt_LastDutyDate" runat="server" TextMode="Date" class="form-control form-control-sm" ReadOnly="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 col-6">
                                        <div class="touchspin-inner">
                                             <label>Resign Reason</label>
                                             <asp:DropDownList ID="dd_ResignReason" runat="server" CssClass="form-control form-control-sm">
                                                <asp:ListItem>Select</asp:ListItem>
                                             </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                      <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 col-6" runat="server" id="img_d">
                                      <div class="touchspin-inner">
                                        <label>Signed Attachment</label>
                                        <!-- File Upload Control -->
                                        <div class="custom-file">
                                          <asp:FileUpload ID="FileUpload1" runat="server" CssClass="custom-file-input" />
                                          <label class="custom-file-label" for="FileUpload1" id="img_lbl">Choose file</label>
                                        </div>
                                      </div>
                                    </div>

                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12 col-12">
                                        <div class="touchspin-inner">
                                              <label>Remarks</label>
                                              <asp:TextBox ID="txt_Remarks" runat="server" TextMode="MultiLine" class="form-control form-control-sm" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
        <div class="card-footer text-right">
            <asp:Label ID="lbl_Msg" runat="server"></asp:Label>
        </div>
        <!-- /.card-footer-->
      </div>
      <!-- /.card -->
    </section>
  


      <section class="content">
      <div class="card collapsed-card">
        <div class="card-header" style="background-color:#363940; color:white;">
          <h3 class="card-title">How was experience in Alkaram ?</h3>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-plus"></i>
            </button>
          </div>
        </div>
        <div class="card-body"> 
            <div class="card">
              <div class="card-body" style="width: 100%; height: 100%; overflow: auto;">
                   <asp:GridView ID="gv_Questions" runat="server" CssClass="table table-bordered table-striped" Font-Size="13px" OnRowDataBound="gv_detail_RowDataBound" AutoGenerateColumns="false">
                            <columns>
                                <asp:BoundField DataField="SEQ_NO" HeaderText="Seq No" />
                                <asp:BoundField DataField="QUESTION" HeaderText="Question" />
                                  <asp:TemplateField HeaderText="Rating">
                                      <ItemTemplate>
                                           <asp:DropDownList id="dd_Rating" runat="server" CssClass="form-control form-control-sm">
                                               <asp:ListItem Text="1" Value="1">Poor 😔</asp:ListItem>
                                               <asp:ListItem Text="2" Value="2">Fair 😏</asp:ListItem>
                                               <asp:ListItem Text="3" Value="3">Good 🙂</asp:ListItem>
                                               <asp:ListItem Text="4" Value="4">Very Good 😜</asp:ListItem>
                                               <asp:ListItem Text="5" Value="5">Excellent 😍</asp:ListItem>
                                           </asp:DropDownList>
                                         
                                       <%--   <asp:TextBox ID="txt_Rating" runat="server" CssClass="form-control form-control-sm" onkeypress='return event.charCode >= 49 && event.charCode <= 53' MaxLength="1" style="width:50px;"></asp:TextBox>
                                          <ajaxToolkit:Rating ID="Rating2" runat="server" CurrentRating="1" MaxRating="5" StarCssClass="starempty" FilledStarCssClass="starfilled" WaitingStarCssClass="starwaiting" EmptyStarCssClass="starempty"></ajaxToolkit:Rating> --%>

                                      </ItemTemplate>
                                  </asp:TemplateField>
                            </columns>
                   </asp:GridView>
              </div>
            </div>
        </div>
        <div class="card-footer">

            <div class="row">
                <div class="col-8">

                </div>
                <div class="col-4 text-right">
                     <asp:Button ID="btn_Submit" runat="server" Text="Submit" class="btn btn-dark" OnClick="btn_Submit_Click"/>          
                </div>
            </div>

           
        </div>
      </div>
    </section>

  </div>
  <!-- /.content-wrapper -->
              <script>
                  let imgInput = document.getElementById('<%= FileUpload1.ClientID %>');
                  imgInput.addEventListener('change', function (e) {
                      var img = e.currentTarget.files[0].name
                      document.getElementById('img_lbl').innerHTML = img;
                  })


    </script>
</asp:Content>

