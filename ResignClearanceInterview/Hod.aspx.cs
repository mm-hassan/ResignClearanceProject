using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResignClearanceInterview
{
    public partial class Hod : System.Web.UI.Page
    {
        Database db = new Database();
        string EmployeeCode, EmployeeName, EmployeeDesignation;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSession();
            if (!Page.IsPostBack)
            {
                loadPendingRequests();
                loadRecentApprovals();
                div_EmployeeDetails.Visible = false;
            }
        }

        public void getSession()
        {
            if (Session["EmployeeCode"] != null)
            {
                EmployeeCode = Session["EmployeeCode"].ToString();
                EmployeeName = Session["EmployeeName"].ToString();
                EmployeeDesignation = Session["EmployeeDesignation"].ToString();
            }
            else
            {
                Response.Redirect("LockScreen.aspx");
            }
        }


        public void loadRecentApprovals()
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, C.APPROVAL_MESSAGE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_LIVE.HRM_EMP_RCI_APPROVALS C WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = C.APPROVAL_SHORT_NAME AND A.HOD_APR_ON IS NOT NULL AND A.EMP_CD IN (SELECT DISTINCT TT.EMP_CD FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW TT WHERE TT.EMP_HOD_CD = '" + EmployeeCode + "' OR (SELECT DISTINCT X.EMP_CD FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW X WHERE X.EMP_CD = '" + EmployeeCode + "' AND X.ROLL = 'SUPER') = '" + EmployeeCode + "') ORDER BY REQUEST_ID DESC";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    gv_RecentApprovals.DataSource = dt;
                    gv_RecentApprovals.DataBind();
                }
                else
                {
                    gv_RecentApprovals.DataSource = dt;
                    gv_RecentApprovals.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void loadPendingRequests()
        {
            try
            {
                DataTable dt = new DataTable();
                //*rz comment 24-07-2023*//
                //string query = "SELECT (A.REQUEST_ID || '-' || A.EMP_CD) REQUEST_EMP_ID, A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = 'NR' AND A.EMP_CD IN (SELECT DISTINCT TT.EMP_CD FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW TT WHERE (TT.EMP_HOD_CD = '" + EmployeeCode + "' OR TT.LINE_MANAGER_CD = '" + EmployeeCode + "') OR (SELECT DISTINCT X.EMP_CD FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW X WHERE X.EMP_CD = '" + EmployeeCode + "' AND X.ROLL = 'SUPER') = '" + EmployeeCode + "') ORDER BY REQUEST_ID DESC";

                //*rz new 24-07-2023*//
                string query = "SELECT (A.REQUEST_ID || '-' || A.EMP_CD) REQUEST_EMP_ID, A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, C.REG_CD, a.resign_attachment FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_EMPLOYEE C WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = 'NR' AND A.EMP_CD IN (SELECT DISTINCT TT.EMP_CD FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW TT WHERE (TT.EMP_HOD_CD = '" + EmployeeCode + "' OR TT.LINE_MANAGER_CD = '" + EmployeeCode + "') OR (SELECT DISTINCT X.EMP_CD FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW X WHERE X.EMP_CD = '" + EmployeeCode + "' AND X.ROLL = 'SUPER') = '" + EmployeeCode + "') AND C.EMP_CD = A.EMP_CD AND A.ISACTIVE = 'Y' ORDER BY REQUEST_ID DESC";

                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    gv_PendingRequests.DataSource = dt;
                    gv_PendingRequests.DataBind();
                    //div_EmployeeDetails.Visible = true;
                    lbl_GridMsg.Text = "Please click on 'view' to process the request.";
                    lbl_GridMsg.ForeColor = System.Drawing.Color.Green;
                }
                else 
                {
                    //div_EmployeeDetails.Visible = false;
                    lbl_GridMsg.Text = "No any pending request.";
                    lbl_GridMsg.ForeColor = System.Drawing.Color.Red;
                    gv_PendingRequests.DataSource = dt;
                    gv_PendingRequests.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void loadEmployeeInfo(string EmployeeCode, string RequestID)
        {
            try
            {
                DataTable dt = new DataTable();
                //string query = "SELECT A.EMP_CD, A.EMPLOYEE_NAME, A.DEPARTMENT_NAME, A.DESIGNATION_NAME, A.HOD_NAME, B.MOBILE_NO, TO_CHAR(B.APPOINTMENT_DATE)APPOINTMENT_DATE, B.PRESENT_ADDRESS FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW A, HRM_EMPLOYEE B where 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.EMP_CD = '" + EmployeeCode + "'";
                string query = "SELECT A.EMP_CD, A.EMPLOYEE_NAME, A.DEPARTMENT_NAME, A.DESIGNATION_NAME, A.HOD_NAME, B.MOBILE_NO, TO_CHAR(B.APPOINTMENT_DATE) APPOINTMENT_DATE, B.PRESENT_ADDRESS, (SELECT D.DETAIL_NAME FROM HRM_SETUP_DETL D WHERE D.SEQ_NO = 132 AND D.DETAIL_ID = C.RESIGN_TYPE) RESIGN_TYPE, (SELECT D.DETAIL_NAME FROM HRM_SETUP_DETL D WHERE D.SEQ_NO = 133 AND D.DETAIL_ID = C.RESIGN_REASON) RESIGN_REASON, (TO_DATE(C.LAST_DUTY_DATE) - TO_DATE(C.L$IN_DATE)) NOTICE_PERIODS_DAYS FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW A, HRM_EMPLOYEE B, HRM_LIVE.HRM_EMP_RCI_REQUEST    C where 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.EMP_CD = C.EMP_CD AND B.EMP_CD = C.EMP_CD AND A.EMP_CD = '" + EmployeeCode + "' AND C.REQUEST_ID ='"+ RequestID +"'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        lbl_EmployeeCode.InnerText = dr["EMP_CD"].ToString();
                        lbl_EmployeeName.InnerText = dr["EMPLOYEE_NAME"].ToString();
                        lbl_Designation.InnerText = dr["DESIGNATION_NAME"].ToString();
                        lbl_Department.InnerText = dr["DEPARTMENT_NAME"].ToString();
                        lbl_Address.InnerText = dr["PRESENT_ADDRESS"].ToString();
                        lbl_PhoneNo.InnerText = dr["MOBILE_NO"].ToString();
                        lbl_DateofJoining.InnerText = dr["APPOINTMENT_DATE"].ToString();
                        lbl_ResignReason.InnerText = dr["RESIGN_REASON"].ToString();
                        lbl_ResignType.InnerText = dr["RESIGN_TYPE"].ToString();
                        lbl_NoticePeriodDays.InnerText = dr["NOTICE_PERIODS_DAYS"].ToString();
                    }
                }
                else 
                {
                        lbl_RequestId.InnerText = string.Empty;
                        lbl_EmployeeCode.InnerText = string.Empty;
                        lbl_EmployeeName.InnerText = "Employee Name";
                        lbl_Designation.InnerText = "Designation";
                        lbl_Department.InnerText = "Department";
                        lbl_Address.InnerText = "Adam Jee Rd, Landhi Town, Karachi, Karachi City, Sindh.";
                        lbl_PhoneNo.InnerText = "(021) 35018638";
                        lbl_DateofJoining.InnerText = "11-FEB-2023";
                        lbl_ResignReason.InnerText = "Retirement";
                        lbl_ResignType.InnerText = "Voluntary";
                        lbl_NoticePeriodDays.InnerText = "30";
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void getImage(string val)
        {
            try
            {
                byte[] bytes;
                string fileName;
                string orcconstring = db.getConnectionOracleCustom();
                OracleConnection orccon = new OracleConnection(orcconstring);
                orccon.Open();
                string msql = "select i.image from hrm_employee e, hrm_employee_image i where e.emp_cd = '" + val + "' and e.EMP_CD = i.emp_cd";
                OracleCommand cmd = new OracleCommand(msql, orccon);
                OracleDataReader odr = cmd.ExecuteReader();
                odr.Read();
                bytes = (byte[])odr["image"];

                //fileName = odr["emp_cd"].ToString();
                Image1.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(bytes);
             
                orccon.Close();

            }
            catch (Exception ex)
            {
                Image1.ImageUrl = "~/Assest/dist/img/logo.PNG";
            }
        }


        protected void lbl_View_Click(object sender, EventArgs e)
        {
            try
            {
                div_EmployeeDetails.Visible = true;
                string Req_Emp_ID = (sender as LinkButton).CommandArgument;
                string[] Ids = Req_Emp_ID.Split('-');
                string Request_ID = Ids[0].ToString();
                string Employee_ID = Ids[1].ToString();
                loadEmployeeInfo(Employee_ID, Request_ID);
                getImage(Employee_ID);

                lbl_RequestId.InnerText = Request_ID.ToString();
            }
            catch (Exception ex)
            {
                
            }
        }


        public void unload()
        {
            try
            {
                loadEmployeeInfo("0","0");
                getImage("0");
                lbl_RequestId.InnerText = string.Empty;
                loadPendingRequests();
                loadRecentApprovals();
                cb_Waiveoff.Checked = false;
                txt_Remarks.Text = string.Empty;
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_Approved_Click(object sender, EventArgs e)
        {
            try
            {
                string Remarks = txt_Remarks.Text;
                string Employeecd = lbl_EmployeeCode.InnerText.ToString();
                string ReqId = lbl_RequestId.InnerText.ToString();
                string Waiveoff = "N";
                if (cb_Waiveoff.Checked)
                {
                    Waiveoff = "Y";
                }

                string Cadre = "";
                string regCd = string.Empty;

                if (ReqId != "0" || ReqId != null || ReqId != string.Empty)
                {
                    string msql = "SELECT E.HRM_CADRE_CD,E.REG_CD FROM HRM_EMPLOYEE E WHERE E.EMP_CD = '" + Employeecd + "'";
                    DataTable dt = new DataTable();
                    dt = db.GetData(msql);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        Cadre = dr["HRM_CADRE_CD"].ToString();
                        regCd = dr["REG_CD"].ToString();
                    }

                    string Query = string.Empty;
                    if (Cadre == "13")
                    {
                        Query = "UPDATE HRM_LIVE.HRM_EMP_RCI_REQUEST SET HOD_REMARKS = '" + Remarks + "', HOD_APR_ON = SYSDATE, HOD_APR_BY = '" + EmployeeCode + "', WAIVEOFF='" + Waiveoff + "', HR_TA_REMARKS = 'There is no need for approval for this cadre', HR_TA_APR_ON = SYSDATE, HR_TA_APR_BY = '0', REQUEST_STATUS = 'HRTA' WHERE REQUEST_ID = '" + ReqId + "' AND EMP_CD = '" + Employeecd + "'";
                    }
                    else
                    {
                        Query = "UPDATE HRM_LIVE.HRM_EMP_RCI_REQUEST SET HOD_REMARKS = '" + Remarks + "', HOD_APR_ON = SYSDATE, HOD_APR_BY = '" + EmployeeCode + "', REQUEST_STATUS = 'HODA', WAIVEOFF='" + Waiveoff + "' WHERE REQUEST_ID = '" + ReqId + "' AND EMP_CD = '" + Employeecd + "'";
                    }
                  
                    string Result = db.PostData(Query);
                    if (Result == "Done")
                    {
                        if (Cadre == "13")
                        {
                            EmailIntimationConcernDepartments(regCd);
                        }
                        else 
                        {
                            EmailIntimationHrTeam(regCd);
                        }
                        
                        lbl_Msg.InnerText = "Approved Sucefully.";
                        txt_Remarks.Text = string.Empty;
                        unload();
                    }
                }
                else 
                {
                    lbl_Msg.InnerText = "Please select any one record.";
                }
            }
            catch (Exception ex)
            {
                
            }
        }


        protected void btn_Reject_Click(object sender, EventArgs e)
        {
            try
            {
                string Remarks = txt_Remarks.Text;
                string Employeecd = lbl_EmployeeCode.InnerText.ToString();
                string ReqId = lbl_RequestId.InnerText.ToString();
                if (ReqId != "0" || ReqId != null || ReqId != string.Empty)
                {
                    string Query = "UPDATE HRM_LIVE.HRM_EMP_RCI_REQUEST SET HOD_REMARKS = '" + Remarks + "', HOD_APR_ON = SYSDATE, HOD_APR_BY = '" + EmployeeCode + "', REQUEST_STATUS = 'HODR' WHERE REQUEST_ID = '" + ReqId + "' AND EMP_CD = '" + Employeecd + "'";
                    string Result = db.PostData(Query);
                    if (Result == "Done")
                    {
                        lbl_Msg.InnerText = "Reject Sucefully.";
                        txt_Remarks.Text = string.Empty;
                        unload();
                    }
                }
                else
                {
                    lbl_Msg.InnerText = "Please select any one record.";
                }
            }
            catch (Exception ex)
            {

            }
        }




        public void EmailIntimationHrTeam(string regCd)
        {
            try
            {
                    //###### Email Body
                    string eBody = string.Empty;
                    eBody += "Hi HR BP," + "<br /><br />";
                    eBody += "Please check the portal for a new resignation request with the employee details listed below." + "<br /><br />";
                    eBody += "Employee Code : " + lbl_EmployeeCode.InnerText + "<br />";
                    eBody += "Employee Name : " + lbl_EmployeeName.InnerHtml + "<br />";
                    eBody += "Employee Department : " + lbl_Department.InnerHtml + "<br />";
                    eBody += "Employee Designation : " + lbl_Designation.InnerText + "<br /><br />";
                    eBody += "http://203.170.75.251/rci/" + "<br />";
                    eBody += " **System Generated Email** ";
                    //###### Email Body

                    //###### SMS Body
                    string sBody = string.Empty;
                    sBody += "Hi HR BP," + "\n";
                    sBody += "Please check the portal for a new resignation request with the employee details listed below." + "\n";
                    sBody += "Employee Code : " + lbl_EmployeeCode.InnerText + "\n";
                    sBody += "Employee Name : " + lbl_EmployeeName.InnerHtml + "\n";
                    sBody += "Employee Department : " + lbl_Department.InnerHtml + "\n";
                    sBody += "Employee Designation : " + lbl_Designation.InnerText + "\n";
                    sBody += "http://203.170.75.251/rci/" + "\n";
                    sBody += " **System Generated SMS** ";
                    //###### SMS Body

                    SendResponse res = new SendResponse();
                    //*rz comment 25-07-2023*//
                    string query = "SELECT B.EMP_NAME, B.E_MAIL, replace(B.MOBILE_NO, '-', '') MOBILE_NO FROM HRM_LIVE.HRM_EMP_RCI_ROLES A, HRM_EMPLOYEE B WHERE 1 = 1 AND A.EMPLOYEE_CD = B.EMP_CD AND B.EMP_STATUS = 'A' AND A.ROLE_NAME = 'HR_TA'";
                    //*rz working new 25-07-2023*//
                    //string query = "SELECT B.EMP_NAME, B.E_MAIL, replace(B.MOBILE_NO, '-', '') MOBILE_NO FROM HRM_LIVE.HRM_EMP_RCI_ROLES A, HRM_EMPLOYEE B WHERE 1 = 1 AND A.EMPLOYEE_CD = B.EMP_CD AND B.EMP_STATUS = 'A' AND A.ROLE_NAME = 'HR_TA' AND B.REG_CD = '"+regCd+"'";
                    DataTable dt = new DataTable();
                    dt = db.GetData(query);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string _Email = dr["E_MAIL"].ToString();
                            string _PhoneNo = dr["MOBILE_NO"].ToString();

                            if (!string.IsNullOrEmpty(_Email))
                            {
                                res.SendEmail(_Email, eBody);
                            }
                            if (!string.IsNullOrEmpty(_PhoneNo))
                            {
                                res.SendSmS(_PhoneNo, sBody);
                            }
                           
                        }
                    } 
            }
            catch (Exception ex)
            {

            }
        }


        public void EmailIntimationConcernDepartments(string regCd)
        {
            try
            {
                //###### Email Body
                string eBody = string.Empty;
                eBody += "Hi," + "<br /><br />";
                eBody += "Please check the portal for a new resignation request with the employee details listed below." + "<br /><br />";
                eBody += "Employee Code : " + lbl_EmployeeCode.InnerText + "<br />";
                eBody += "Employee Name : " + lbl_EmployeeName.InnerHtml + "<br />";
                eBody += "Employee Department : " + lbl_Department.InnerHtml + "<br />";
                eBody += "Employee Designation : " + lbl_Designation.InnerText + "<br /><br />";
                eBody += "http://203.170.75.251/rci/" + "<br />";
                eBody += " **System Generated Email** ";
                //###### Email Body


                //###### SMS Body
                string sBody = string.Empty;
                sBody += "Hi," + "\n";
                sBody += "Please check the portal for a new resignation request with the employee details listed below." + "\n";
                sBody += "Employee Code : " + lbl_EmployeeCode.InnerText + "\n";
                sBody += "Employee Name : " + lbl_EmployeeName.InnerHtml + "\n";
                sBody += "Employee Department : " + lbl_Department.InnerHtml + "\n";
                sBody += "Employee Designation : " + lbl_Designation.InnerText + "\n";
                sBody += "http://203.170.75.251/rci/" + "\n";
                sBody += " **System Generated SMS** ";
                //###### SMS Body

                SendResponse res = new SendResponse();
               
                //*rz comment 25-07-2023*//
                string query = "SELECT DISTINCT B.EMP_NAME, B.E_MAIL, replace(B.MOBILE_NO, '-', '') MOBILE_NO FROM HRM_LIVE.HRM_EMP_RCI_ROLES A, HRM_EMPLOYEE B WHERE 1 = 1 AND A.EMPLOYEE_CD = B.EMP_CD AND B.EMP_STATUS = 'A' AND A.ROLE_NAME NOT IN('SUPER','HR_TA', 'HR_HEAD')";
                
                //*rz working new 25-07-2023*//
                //string query = "SELECT B.EMP_NAME, B.E_MAIL, replace(B.MOBILE_NO, '-', '') MOBILE_NO FROM HRM_LIVE.HRM_EMP_RCI_ROLES A, HRM_EMPLOYEE B WHERE 1 = 1 AND A.EMPLOYEE_CD = B.EMP_CD AND B.EMP_STATUS = 'A' AND A.ROLE_NAME NOT IN ('SUPER', 'HR_TA', 'HR_HEAD') AND B.REG_CD = '"+regCd+"'";
                //string query = "SELECT B.EMP_NAME, B.E_MAIL, replace(B.MOBILE_NO, '-', '') MOBILE_NO FROM HRM_LIVE.HRM_EMP_RCI_ROLES A, HRM_EMPLOYEE B WHERE 1 = 1 AND A.EMPLOYEE_CD = B.EMP_CD AND B.EMP_STATUS = 'A' AND A.ROLE_NAME NOT IN ('SUPER', 'HR_TA', 'HR_HEAD') AND B.REG_CD = " + regCd + "";
                DataTable dt = new DataTable();
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string _Email = dr["E_MAIL"].ToString();
                        string _PhoneNo = dr["MOBILE_NO"].ToString();

                        //res.SendEmail(_Email, eBody);
                        //res.SendSmS(_PhoneNo, sBody);

                        if (!string.IsNullOrEmpty(_Email))
                        {
                            res.SendEmail(_Email, eBody);
                        }
                        if (!string.IsNullOrEmpty(_PhoneNo))
                        {
                            res.SendSmS(_PhoneNo, sBody);
                        }
                            
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gv_PendingRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DataRowView dr = (DataRowView)e.Row.DataItem;
                //string val = dr[9].ToString();
                //if (!string.IsNullOrEmpty(val) && val != "0")
                //{
                //    // Get the URL or base64-encoded image data based on your specific logic
                //    string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr[9]);

                //    // Find the Image control within the row
                //    System.Web.UI.WebControls.Image img = e.Row.FindControl("Imag") as System.Web.UI.WebControls.Image;

                //    if (img != null)
                //    {
                //        img.ImageUrl = imageUrl;
                //    }
                //}

            }
        }

        protected void btn_view_ltr_Click(object sender, EventArgs e)
        {
            try
            {
                string empCd = lbl_EmployeeCode.InnerText.ToString();

                DataTable dt = db.GetData("select a.resign_attachment from HRM_LIVE.HRM_EMP_RCI_request A where a.emp_cd like '" + empCd + "'");
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["resign_attachment"] != DBNull.Value)
                    {
                        byte[] imgData = (byte[])dr["resign_attachment"];
                        // Set the response headers to force the browser to download the file
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "image/jpeg"; // Change the content type according to the image format
                        Response.AddHeader("content-disposition", "attachment;filename=image.jpg"); // Change the filename as needed

                        // Send the image data as a file to the client's browser
                        Response.BinaryWrite(imgData);
                        Response.End();
                    }
                    else
                    {
                        string message = "Old resigns do not have attachments.";
                        string script = "<script type='text/javascript'>alert('" + message + "');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    }
                  
                }

            }
            catch (Exception ex)
            {
               
            }
        }


        protected void gv_RecentApprovals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_RecentApprovals.PageIndex = e.NewPageIndex;
            loadRecentApprovals();
        }

    }
}