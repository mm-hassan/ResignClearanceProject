<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="ResignClearanceInterview.Store" %>
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
          <h3 class="card-title">Pending Request</h3>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>
          </div>
        </div>
        <div class="card-body p-1"> 
            <div class="card">
              <div class="card-body p-0" style="width: 100%; height: 300px; overflow: auto;">
                   <asp:GridView ID="gv_PendingRequests" runat="server" CssClass="table table-bordered table-striped" Font-Size="13px" AutoGenerateColumns="false">
                              <Columns>
                                  <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                           <asp:LinkButton ID="lbl_View" CssClass="btn btn-outline-dark" runat="server" CommandName="View" CommandArgument='<%# Bind("REQUEST_EMP_ID") %>' OnClick="lbl_View_Click">View</asp:LinkButton>
                                        </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="REQUEST_ID" HeaderText="Request ID" />
                                  <asp:BoundField DataField="EMP_CD" HeaderText="Employee Code" />
                                  <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Employee Name" />
                                  <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation" />
                                  <asp:BoundField DataField="L$IN_DATE" HeaderText="Resign Date" />
                                  <asp:BoundField DataField="LAST_DUTY_DATE" HeaderText="Last Duty Date" />
                                  <asp:BoundField DataField="REMARKS" HeaderText="Employee Remarks" />
                                  <asp:BoundField DataField="HOD_REMARKS" HeaderText="HOD Remarks" />
                                  <asp:BoundField DataField="HR_TA_REMARKS" HeaderText="HR BP Remarks" />
                                  <asp:BoundField DataField="HR_HOD_REMARKS" HeaderText="HR Head Remarks" />
                                  <asp:BoundField DataField="WAIVE_OFF" HeaderText="Waive Off" />
                                  <asp:BoundField DataField="REG_CD" HeaderText="REG CODE" />
                              </Columns>
                   </asp:GridView>
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







      <section class="content" id="div_EmployeeDetails" runat="server">
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
                        <span runat="server" id="lbl_RequestId" style="display:none;"></span>
                      <span runat="server" id="lbl_EmployeeCode" style="display:none;"> </span><h2 class="lead" runat="server" id="lbl_EmployeeName" style="color:#5960ff; font-weight: 900; text-decoration: underline;"><b>Employee Name</b></h2>
                      <p runat="server" id="lbl_Designation">Designation</p> <p style="display:none;" runat="server" id="lbl_Department">Department</p>
                      <%--<h2 class="lead" runat="server" id="lbl_Designation"><b>Asst. Manager</b></h2>--%>
                      <p class="text-muted text-sm"><b>Date of Joining: </b> <span runat="server" id="lbl_DateofJoining" style="color:#5960ff; font-weight: 900;">11-Feb-2022</span></p>
                      <ul class="ml-4 mb-0 fa-ul text-muted">
                        <li class="small" style="display:none;"><span class="fa-li"><i class="fas fa-lg fa-building"></i></span><b>Address:</b> <span runat="server" id="lbl_Address" style="color:#5960ff; font-weight: 900;">Adam Jee Rd, Landhi Town, Karachi, Karachi City, Sindh.</span></li>
                        <li class="small" style="display:none;"><span class="fa-li"><i class="fas fa-lg fa-phone"></i></span><b>Phone #:</b> <span runat="server" id="lbl_PhoneNo" style="color:#5960ff; font-weight: 900;">(021) 35018638</span></li>
                        
                        <li class="small" style="display:none;"><span class="fa-li"><i class="fas fa-lg fa-paper-plane"></i></span><b>Resign Reason:</b> <span runat="server" id="lbl_ResignReason" style="color:#5960ff; font-weight: 900;">Retirement</span></li>
                        <li class="small" style="display:none;"><span class="fa-li"><i class="fas fa-lg fa-paper-plane"></i></span><b>Resign Type:</b> <span runat="server" id="lbl_ResignType" style="color:#5960ff; font-weight: 900;">Voluntary</span></li>
                        <li class="small"><span class="fa-li"><i class="fas fa-sync"></i></span><b>Notice Period Days:</b> <span runat="server" id="lbl_NoticePeriodDays" style="color:#5960ff; font-weight: 900;">30</span></li>
                      </ul>
                    </div>
                      <div class="col-12 col-md-5">
                          <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="touchspin-inner">
                                    <p>Remarks</p>
                                    <asp:TextBox ID="txt_Remarks" runat="server" TextMode="MultiLine" class="form-control form-control-sm"></asp:TextBox>
                                </div>
                          </div> 
                           <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="touchspin-inner pt-1 text-right">
                                    
                                    <%--<asp:LinkButton ID="btn_Reject" runat="server" class="btn btn-sm bg-teal" OnClick="btn_Reject_Click" style="background-color: #363940!important;">
                                     <i class="fas fa-user"></i> Reject</asp:LinkButton>--%>
                                    <asp:LinkButton ID="btn_Approved" runat="server" class="btn btn-sm btn-primary" OnClick="btn_Approved_Click">
                                     <i class="fas fa-user"></i> Approve</asp:LinkButton>
                                    <p runat="server" id="lbl_Msg">.</p>
                                </div>
                          </div>                       
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
    <section class="content" id="div_EmployeeFeedback" runat="server">
      
      <div class="card">
        <div class="card-header" style="background-color:#363940; color:white;">
          <h3 class="card-title">Employee Asset Details</h3>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-minus"></i>
            </button>
          </div>
        </div>
        <div class="card-body" id="div_Cardbody" runat="server"> 
            <div class="card">
              <div class="card-body" style="width: 100%; height: auto; overflow: auto;">
                   <asp:GridView ID="gv_AssetDtl" runat="server" CssClass="table table-bordered table-striped" Font-Size="13px" AutoGenerateColumns="false" OnRowDataBound="gv_detail_RowDataBound">
                            <Columns>
                                  <%--<asp:TemplateField HeaderText="Block">
                                      <ItemTemplate >
                                          <asp:CheckBox ID="cb_Clear" runat="server" /> <b>Clear</b> 
                                      </ItemTemplate>
                                  </asp:TemplateField>--%>
                                  <asp:BoundField DataField="ITEM_ID" HeaderText="Item ID" />
                                  <asp:BoundField DataField="DESCRIPTION" HeaderText="Item Description" />
                                  <asp:BoundField DataField="issue_date" HeaderText="Issue Date" />
                                  <%--<asp:BoundField DataField="STATUS" HeaderText="Status" />--%>
                             </Columns>
                   </asp:GridView>
              </div>
            </div>
        </div>
        <div class="card-footer">

            <div class="row">
                <div class="col-12">
                    <asp:Label ID="lbl_AssetMsg" runat="server" />
                </div>
             <%--   <div class="col-4 text-right">
                     <asp:Button ID="btn_Submit" runat="server" Text="Submit" class="btn btn-dark" style="width: 200px;"/>          
                </div>--%>
            </div>

           
        </div>
      </div>
    </section>














       <!-- Main content Request type start------------------------------------------------------------------------->
    <section class="content">
      
      <div class="card collapsed-card">
        <div class="card-header" style="background-color:#363940; color:white;">
          <h3 class="card-title">Recent approved requests</h3>
          <div class="card-tools">
            <button type="button" class="btn btn-tool" data-card-widget="collapse" title="Collapse">
              <i class="fas fa-plus"></i>
            </button>
          </div>
        </div>
        <div class="card-body"> 
            <div class="card">
              <div class="card-body" style="width: 100%; height: 400px; overflow: auto;">
                   <asp:GridView ID="gv_RecentApprovals" runat="server" CssClass="table table-bordered table-striped" Font-Size="13px" AutoGenerateColumns="false">
                              <Columns>
                                  <asp:BoundField DataField="REQUEST_ID" HeaderText="Request ID" />
                                  <asp:BoundField DataField="EMP_CD" HeaderText="Employee Code" />
                                  <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Employee Name" />
                                  <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation" />
                                  <asp:BoundField DataField="L$IN_DATE" HeaderText="Resign Date" />
                                  <asp:BoundField DataField="LAST_DUTY_DATE" HeaderText="Last Duty Date" />
                                  <asp:BoundField DataField="REMARKS" HeaderText="Employee Remarks" />
                                  <asp:BoundField DataField="HOD_REMARKS" HeaderText="HOD Remarks" />
                                  <asp:BoundField DataField="HR_TA_REMARKS" HeaderText="HR Team Remarks" />
                                  <asp:BoundField DataField="APPROVAL_MESSAGE" HeaderText="Request Status" />
                              </Columns>
                   </asp:GridView>
              </div>
            </div>
        </div>
        <div class="card-footer">

            <div class="row">
                <div class="col-8">
                    <asp:Label ID="Label1" runat="server" />
                </div>
             <%--   <div class="col-4 text-right">
                     <asp:Button ID="btn_Submit" runat="server" Text="Submit" class="btn btn-dark" style="width: 200px;"/>          
                </div>--%>
            </div>

           
        </div>
      </div>
    </section>






  </div>
  <!-- /.content-wrapper -->
     
    
    <script language="javascript" type="text/javascript">
        function selectallbox(headercheck) {
            var ischecked = headercheck.checked;
            Parent = document.getElementById('gv_CommunicationDtl');
            var item = Parent.getElementsByTagName('input');
            for (i = 0; i < item.length; i++) {
                if (item[i].id != headercheck && item[i].type == "checkbox") {
                    if (item[i].checked != ischecked) {
                        item[i].click();
                    }
                }
            }
        }

          </script>
    
           

</asp:Content>
