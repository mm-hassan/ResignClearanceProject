<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ResignClearanceInterview.Report" %>
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
          <h3 class="card-title">Reports Panel</h3>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>
          </div>
        </div>
        <div class="card-body"> 
            <div class="card">
              <div class="card-body" style="width: 100%; height: 400px; overflow: auto;">
                  
                   <div class="row">
                      <div class="col-4 col-md-2 text-center">
                          <asp:Image ID="Image1" runat="server" alt="user-avatar" class="img-circle img-fluid" style="height:150px; width:200px;" ImageUrl="~/Assest/dist/img/logo.PNG" />
                    <%--  <img src="Assest/dist/img/user1-128x128.jpg" alt="user-avatar" class="img-circle img-fluid">--%>
                    </div>
                    <div class="col-8 col-md-10">
                        <span runat="server" id="lbl_RequestId" style="display:none;"></span>
                        <span runat="server" id="lbl_ConcernDep" style="display:none;"></span>
                         <p>Employee Code</p>
                         <asp:DropDownList ID="dd_Employees" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="dd_Employees_SelectedIndexChanged">
                                 
                         </asp:DropDownList>

                        <asp:TextBox ID="txt_Employeeno" runat="server" class="form-control form-control-sm" OnTextChanged="txt_Employeeno_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <p runat="server" id="lbl_Msg">.</p>
                        <br />
                      <p class="text-muted text-sm"><b>Department </b> <span runat="server" id="lbl_Department" style="color:#5960ff; font-weight: 900;"></span></p>
                      <p class="text-muted text-sm"><b>Designation </b> <span runat="server" id="lbl_Designation" style="color:#5960ff; font-weight: 900;"></span>
                      <p class="text-muted text-sm"><b>Email Address</b> <span runat="server" id="lbl_Email" style="color:#5960ff; font-weight: 900;"></span></p>
                      <p class="text-muted text-sm"><b>Phone No </b> <span runat="server" id="lbl_Phoneno" style="color:#5960ff; font-weight: 900;"></span></p>
                           
                      <div class="text-right">
                          <asp:LinkButton ID="btn_Statusreport" runat="server" class="btn btn-sm btn-danger" OnClick="btn_Statusreport_Click"><i class="fas fa-receipt"></i> Status Report</asp:LinkButton>
                          <asp:LinkButton ID="btn_Clearancereport" runat="server" class="btn btn-sm btn-primary" OnClick="btn_Clearancereport_Click"><i class="fas fa-mail-bulk"></i> Clearance Report</asp:LinkButton>
                          <asp:LinkButton ID="btn_Exitinterviewreport" runat="server" class="btn btn-sm btn-secondary" OnClick="btn_Exitinterviewreport_Click"><i class="fas fa-info"></i> Exit Interview Report</asp:LinkButton>
                          <asp:LinkButton ID="btn_Allpendingstatus" runat="server" class="btn btn-sm btn-dark" OnClick="btn_Allpendingstatus_Click"><i class="fas fa-receipt"></i> All Pending Status</asp:LinkButton>
                          <asp:LinkButton ID="btn_Excelreport" runat="server" ToolTip="All pending data exports in Excel" class="btn btn-sm btn-danger" OnClick="btn_Excelreport_Click"><i class="fas fa-receipt"></i> Export in Excel</asp:LinkButton>
                      </div>
                    </div>
                      
                    
                  </div>

              </div>
            </div>
        </div>
        <div class="card-footer">
            <div class="card" style="width: 25rem;">
        <div class="card-body">
            <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="val" runat="server" BackColor="#CCCCCC" Font-Size="Large" ForeColor="Red" />
            <h5 class="card-title">Generate Report</h5>
            <div class="input-group mb-3">
                <div class="input-group-prepend">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control form-control-sm" placeholder="From Date" style="max-width: 125px;"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy" />
                                        <asp:RequiredFieldValidator ValidationGroup="val" ControlToValidate="txtFromDate" ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="From Date field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>

                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control form-control-sm" placeholder="To Date" style="max-width: 125px;"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy" />
                                        <asp:RequiredFieldValidator ValidationGroup="val" ControlToValidate="txtToDate" ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="To Date field cannot be empty" ForeColor="Red" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                  
                    <asp:Button ID="Button1" ValidationGroup="val" runat="server" Text="Generate Report" CssClass="btn btn-sm btn-info"  style="max-width: 115px;" OnClick="btn_GenerateReport_Click" />
                </div>
                
                
            </div>
        </div>
    </div>
            <div class="row">
                <div class="col-8" style="display:none;">
                    <asp:GridView ID="gv_Details" runat="server"></asp:GridView>
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
