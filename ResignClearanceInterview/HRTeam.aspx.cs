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
    public partial class HRTeam : System.Web.UI.Page
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
                txt_Noticeperioddate.Enabled = false;
                div_EmployeeDetails.Visible = false;
                div_EmployeeFeedback.Visible = false;
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



        public void loadEmployeeRating(string SeqNo)
        {
            try
            {
                DataTable dt = new DataTable();
                //string query = "SELECT B.SEQ_NO, B.QUESTION, C.ANSWERS_RATING FROM HRM_LIVE.HRM_EMP_RCI_APPROVALS A, HRM_LIVE.HRM_EMP_RCI_QUESTIONS B, HRM_LIVE.HRM_EMP_RCI_ANSWERS   C WHERE A.SEQ_NO = C.REQUEST_ID AND B.SEQ_NO = C.QUESTION_SEQ_NO AND A.SEQ_NO = '"+ SeqNo +"'";
                //string query = "SELECT B.SEQ_NO, B.QUESTION, D.RATING_NAME RATING FROM HRM_LIVE.HRM_EMP_RCI_APPROVALS A, HRM_LIVE.HRM_EMP_RCI_QUESTIONS B, HRM_LIVE.HRM_EMP_RCI_ANSWERS   C, HRM_LIVE.HRM_EMP_RCI_ANSWERS_RATINGS    D WHERE A.SEQ_NO = C.REQUEST_ID AND B.SEQ_NO = C.QUESTION_SEQ_NO AND C.ANSWERS_RATING = D.RATING_ID AND A.SEQ_NO = '" + SeqNo + "'";
                string query = "SELECT a.SEQ_NO, A.QUESTION, C.RATING_NAME RATING FROM HRM_LIVE.HRM_EMP_RCI_QUESTIONS A, HRM_LIVE.HRM_EMP_RCI_ANSWERS B, HRM_LIVE.HRM_EMP_RCI_ANSWERS_RATINGS C WHERE 1 = 1 AND A.SEQ_NO = B.QUESTION_SEQ_NO AND C.RATING_ID = B.ANSWERS_RATING AND B.REQUEST_ID = '" + SeqNo + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    gv_Questions.DataSource = dt;
                    gv_Questions.DataBind();
                }
                else
                {
                    gv_Questions.DataSource = dt;
                    gv_Questions.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void loadRecentApprovals()
        {
            try
            {
                DataTable dt = new DataTable();
                //string query = "SELECT A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, C.APPROVAL_MESSAGE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_LIVE.HRM_EMP_RCI_APPROVALS C WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = C.APPROVAL_SHORT_NAME AND A.REQUEST_STATUS NOT IN ('NR', 'HODA') AND A.EMP_CD IN (SELECT DISTINCT TT.EMP_CD FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW TT WHERE TT.EMP_HOD_CD = '" + EmployeeCode + "' OR (SELECT DISTINCT X.EMP_CD FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW X WHERE X.EMP_CD = '" + EmployeeCode + "' AND X.ROLL = 'SUPER') = '" + EmployeeCode + "') AND A.HR_TA_APR_ON IS NOT NULL ORDER BY REQUEST_ID DESC";
                //string query = "SELECT A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, B.DEPARTMENT_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, C.APPROVAL_MESSAGE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_LIVE.HRM_EMP_RCI_APPROVALS C WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = C.APPROVAL_SHORT_NAME AND A.REQUEST_STATUS NOT IN ('NR', 'HODA') AND A.HR_TA_APR_ON IS NOT NULL ORDER BY REQUEST_ID DESC";
                string query = "SELECT A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, B.DEPARTMENT_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, C.APPROVAL_MESSAGE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_LIVE.HRM_EMP_RCI_APPROVALS C, HRM_EMPLOYEE D WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.EMP_CD = D.EMP_CD AND D.REG_CD =(SELECT K.REG_CD FROM HRM_EMPLOYEE K WHERE K.EMP_CD = '" + EmployeeCode + "') AND A.REQUEST_STATUS = C.APPROVAL_SHORT_NAME AND A.REQUEST_STATUS NOT IN ('NR', 'HODA') AND A.HR_TA_APR_ON IS NOT NULL ORDER BY REQUEST_ID DESC";
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
                //string query = "SELECT (A.REQUEST_ID || '-' || A.EMP_CD) REQUEST_EMP_ID, A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = 'HODA' AND A.EMP_CD IN (SELECT DISTINCT TT.EMP_CD FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW TT WHERE TT.EMP_HOD_CD = '" + EmployeeCode + "' OR (SELECT DISTINCT X.EMP_CD FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW X WHERE X.EMP_CD = '" + EmployeeCode + "' AND X.ROLL = 'SUPER') = '" + EmployeeCode + "') AND (A.WAIVEOFF = 'N' OR A.WAIVEOFF IS NULL) ORDER BY REQUEST_ID DESC";
                //string query = "SELECT (A.REQUEST_ID || '-' || A.EMP_CD) REQUEST_EMP_ID, A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, B.DEPARTMENT_NAME, A.REMARKS, A.HOD_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = 'HODA' AND A.HR_TA_APR_ON IS NULL AND (A.WAIVEOFF = 'N' OR A.WAIVEOFF IS NULL) ORDER BY REQUEST_ID DESC";
                //*rz comment 24-07-2023*//
                //string query = "SELECT (A.REQUEST_ID || '-' || A.EMP_CD) REQUEST_EMP_ID, A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, B.DEPARTMENT_NAME, A.REMARKS, A.HOD_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_EMPLOYEE C WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.EMP_CD = C.EMP_CD AND B.EMP_CD = C.EMP_CD AND C.REG_CD = (SELECT K.REG_CD FROM HRM_EMPLOYEE K WHERE K.EMP_CD = '"+ EmployeeCode +"') AND A.REQUEST_STATUS = 'HODA' AND A.HR_TA_APR_ON IS NULL AND (A.WAIVEOFF = 'N' OR A.WAIVEOFF IS NULL) ORDER BY REQUEST_ID DESC";
                
                //*rz new 24-07-2023*//
                string query = "SELECT (A.REQUEST_ID || '-' || A.EMP_CD) REQUEST_EMP_ID, A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, B.DEPARTMENT_NAME, A.REMARKS, A.HOD_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, C.REG_CD,B.UNIT_NAME LOCATION_NAME,B.DIVISION_NAME FROM HRM_LIVE.HRM_EMP_RCI_REQUEST    A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_EMPLOYEE C WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.EMP_CD = C.EMP_CD AND B.EMP_CD = C.EMP_CD AND A.REQUEST_STATUS = 'HODA' AND A.HR_TA_APR_ON IS NULL AND (A.WAIVEOFF = 'N' OR A.WAIVEOFF IS NULL) AND C.EMP_CD = A.EMP_CD AND C.REG_CD IN (SELECT T.REG_CD FROM HRM_EMP_RCI_REGION_ALLOW_V T WHERE T.EMP_CD = '" + EmployeeCode + "') AND A.ISACTIVE = 'Y' ORDER BY REQUEST_ID DESC";
                
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
                string query = "SELECT A.EMP_CD, A.EMPLOYEE_NAME, A.DEPARTMENT_NAME, A.DESIGNATION_NAME, A.HOD_NAME, B.MOBILE_NO, TO_CHAR(B.APPOINTMENT_DATE) APPOINTMENT_DATE, B.PRESENT_ADDRESS, (SELECT D.DETAIL_NAME FROM HRM_SETUP_DETL D WHERE D.SEQ_NO = 132 AND D.DETAIL_ID = C.RESIGN_TYPE) RESIGN_TYPE, (SELECT D.DETAIL_NAME FROM HRM_SETUP_DETL D WHERE D.SEQ_NO = 133 AND D.DETAIL_ID = C.RESIGN_REASON) RESIGN_REASON, (TO_DATE(C.LAST_DUTY_DATE) - TO_DATE(C.L$IN_DATE)) NOTICE_PERIODS_DAYS, TO_CHAR(C.LAST_DUTY_DATE)LAST_DUTY_DATE FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW A, HRM_EMPLOYEE B, HRM_LIVE.HRM_EMP_RCI_REQUEST    C where 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.EMP_CD = C.EMP_CD AND B.EMP_CD = C.EMP_CD AND A.EMP_CD = '" + EmployeeCode + "' AND C.REQUEST_ID ='" + RequestID + "'";
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
                        //txt_Noticeperioddate.Text = Convert.ToDateTime(dr["LAST_DUTY_DATE"]);
                        DateTime dat = Convert.ToDateTime(dr["LAST_DUTY_DATE"]);
                        txt_Noticeperioddate.Text = dat.ToString("yyyy-MM-dd");

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
                        cb_Noticeperioddate.Checked = false;
                        txt_Noticeperioddate.Text = DateTime.Now.ToString();
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
                div_EmployeeFeedback.Visible = true;
                string Req_Emp_ID = (sender as LinkButton).CommandArgument;
                string[] Ids = Req_Emp_ID.Split('-');
                string Request_ID = Ids[0].ToString();
                string Employee_ID = Ids[1].ToString();
                loadEmployeeInfo(Employee_ID, Request_ID);
                loadEmployeeRating(Request_ID);
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
                loadEmployeeRating("0");
                lbl_RequestId.InnerText = string.Empty;
                loadPendingRequests();
                loadRecentApprovals();
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

                DateTime LastDutyDate = Convert.ToDateTime(txt_Noticeperioddate.Text);


                if (ReqId != "0" || ReqId != null || ReqId != string.Empty)
                {
                    string Query = string.Empty;
                    if (cb_Noticeperioddate.Checked)
                    {
                        string QueryLogs = "INSERT INTO HRM_LIVE.HRM_EMP_RCI_APPROVALS_LOGS(LOG_ID, REQUEST_NO, ITEM_DESCRIPTIONS, ITEM_STATUS, REMARKS, APPROVAL_DEP, L$USR_IN, L$IN_DATE) ";
                        QueryLogs += " VALUES ((SELECT NVL(MAX(LOG_ID),0)+1 FROM HRM_LIVE.HRM_EMP_RCI_APPROVALS_LOGS), '" + ReqId + "','Notice Period Days Extension','Date Change', (SELECT A.LAST_DUTY_DATE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A WHERE A.REQUEST_ID = '" + ReqId + "'), 'HR', '" + EmployeeCode + "', sysdate)";
                        db.PostData(QueryLogs);

                        Query = "UPDATE HRM_LIVE.HRM_EMP_RCI_REQUEST SET HR_TA_REMARKS = '" + Remarks + "', HR_TA_APR_ON = SYSDATE, HR_TA_APR_BY = '" + EmployeeCode + "', REQUEST_STATUS = 'HRTA', LAST_DUTY_DATE = TO_DATE('" + LastDutyDate + "', 'mm/dd/yyyy hh:mi:ss am') WHERE REQUEST_ID = '" + ReqId + "' AND EMP_CD = '" + Employeecd + "'";
                    }
                    else 
                    {
                        Query = "UPDATE HRM_LIVE.HRM_EMP_RCI_REQUEST SET HR_TA_REMARKS = '" + Remarks + "', HR_TA_APR_ON = SYSDATE, HR_TA_APR_BY = '" + EmployeeCode + "', REQUEST_STATUS = 'HRTA' WHERE REQUEST_ID = '" + ReqId + "' AND EMP_CD = '" + Employeecd + "'";
                    }

                    string Result = db.PostData(Query);
                    if (Result == "Done")
                    {
                        string EmpInfoUptQuery = "UPDATE HRM_EMPLOYEE E SET E.NOTICE_PAY_FROM = (SELECT A.L$IN_DATE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A WHERE A.REQUEST_ID = '" + ReqId + "'), E.NOTICE_PAY_TO = (SELECT A.LAST_DUTY_DATE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A WHERE A.REQUEST_ID = '" + ReqId + "') WHERE E.EMP_CD ='" + Employeecd + "' ";
                        db.PostData(EmpInfoUptQuery);

                        EmailIntimationConcernDepartments();
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
                    string Query = "UPDATE HRM_LIVE.HRM_EMP_RCI_REQUEST SET HR_TA_REMARKS = '" + Remarks + "', HR_TA_APR_ON = SYSDATE, HR_TA_APR_BY = '" + EmployeeCode + "', REQUEST_STATUS = 'HRTR' WHERE REQUEST_ID = '" + ReqId + "' AND EMP_CD = '" + Employeecd + "'";
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


        public void EmailIntimationConcernDepartments()
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
                string query = "SELECT B.EMP_NAME, B.E_MAIL, replace(B.MOBILE_NO, '-', '') MOBILE_NO FROM HRM_LIVE.HRM_EMP_RCI_ROLES A, HRM_EMPLOYEE B WHERE 1 = 1 AND A.EMPLOYEE_CD = B.EMP_CD AND B.EMP_STATUS = 'A' AND A.ROLE_NAME NOT IN('SUPER','HR_TA', 'HR_HEAD', 'IR_OPE','PAY_OPE','AUDIT')";
                DataTable dt = new DataTable();
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string _Email = dr["E_MAIL"].ToString();
                        string _PhoneNo = dr["MOBILE_NO"].ToString();
                        res.SendEmail(_Email, eBody);
                        res.SendSmS(_PhoneNo, sBody);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void cb_Noticeperioddate_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_Noticeperioddate.Checked)
            {
                txt_Noticeperioddate.Enabled = true;
            }
            else 
            {
                txt_Noticeperioddate.Enabled = false;
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

        protected void btn_revert_back_Click(object sender, EventArgs e)
        {
            try
            {
                 string Employeecd = lbl_EmployeeCode.InnerText.ToString();
                 string updateQuery = "UPDATE HRM_LIVE.HRM_EMP_RCI_REQUEST SET ISACTIVE = 'N',REQUEST_STATUS = 'RB' WHERE EMP_CD = '" + Employeecd + "'";
                 string result = db.PostData(updateQuery);
                 if (result == "Done")
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Successfully Reverted!');", true);
                     loadPendingRequests();
                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", "alert('Can't process request please try again later!');", true);
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