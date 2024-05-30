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
    public partial class Tms : System.Web.UI.Page
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



        public void loadAssetDtl(string EmployeeID)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT NVL(E.LAST_DUTY, E.NOTICE_PAY_TO)LAST_DUTY FROM HRM_EMPLOYEE E WHERE E.EMP_CD = '" + EmployeeID + "'";
                dt = db.GetDataEbs(query);
                if (dt.Rows.Count > 0)
                {
                    div_Cardbody.Visible = true;
                    lbl_AssetMsg.Text = "Please confirm, then approve the request.";
                    gv_LastDutyDtl.DataSource = dt;
                    gv_LastDutyDtl.DataBind();
                }
                else
                {
                    div_Cardbody.Visible = false;
                    lbl_AssetMsg.Text = "Last date not found on this employee.";
                    gv_LastDutyDtl.DataSource = dt;
                    gv_LastDutyDtl.DataBind();
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
                //string query = "SELECT A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, C.APPROVAL_MESSAGE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_LIVE.HRM_EMP_RCI_APPROVALS C WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = C.APPROVAL_SHORT_NAME AND A.TMS_APR_ON IS NOT NULL AND A.EMP_CD IN (SELECT DISTINCT TT.EMP_CD FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW TT WHERE TT.EMP_HOD_CD = '" + EmployeeCode + "' OR (SELECT DISTINCT X.EMP_CD FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW X WHERE X.EMP_CD = '" + EmployeeCode + "' AND X.ROLL = 'SUPER') = '" + EmployeeCode + "') ORDER BY REQUEST_ID DESC";
                string query = "SELECT A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, C.APPROVAL_MESSAGE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_LIVE.HRM_EMP_RCI_APPROVALS C WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = C.APPROVAL_SHORT_NAME AND A.TMS_APR_ON IS NOT NULL ORDER BY REQUEST_ID DESC";
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
                //string query = "SELECT (A.REQUEST_ID || '-' || A.EMP_CD) REQUEST_EMP_ID, A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, A.HR_HOD_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = 'HRTA' AND A.TMS_APR_ON IS NULL AND A.EMP_CD IN (SELECT DISTINCT TT.EMP_CD FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW TT WHERE TT.EMP_HOD_CD = '" + EmployeeCode + "' OR (SELECT DISTINCT X.EMP_CD FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW X WHERE X.EMP_CD = '" + EmployeeCode + "' AND X.ROLL = 'SUPER') = '" + EmployeeCode + "') ORDER BY REQUEST_ID DESC";
                //*rz comment 24-07-2023*//
                //string query = "SELECT (A.REQUEST_ID || '-' || A.EMP_CD) REQUEST_EMP_ID, A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, A.HR_HOD_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, (CASE WHEN A.WAIVEOFF = 'Y' THEN 'YES' ELSE 'NO' END) WAIVE_OFF FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = 'HRTA' AND A.TMS_APR_ON IS NULL ORDER BY REQUEST_ID DESC";
                
                //*rz new 24-07-2023*//
                string query = "SELECT (A.REQUEST_ID || '-' || A.EMP_CD) REQUEST_EMP_ID, A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, A.HR_HOD_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, (CASE WHEN A.WAIVEOFF = 'Y' THEN 'YES' ELSE 'NO' END) WAIVE_OFF, C.REG_CD FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_EMPLOYEE C WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = 'HRTA' AND A.TMS_APR_ON IS NULL AND C.EMP_CD = A.EMP_CD AND C.REG_CD IN (SELECT T.REG_CD FROM HRM_EMP_RCI_REGION_ALLOW_V T WHERE T.EMP_CD = '"+EmployeeCode+"') ORDER BY REQUEST_ID DESC";
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
                div_EmployeeFeedback.Visible = true;
                string Req_Emp_ID = (sender as LinkButton).CommandArgument;
                string[] Ids = Req_Emp_ID.Split('-');
                string Request_ID = Ids[0].ToString();
                string Employee_ID = Ids[1].ToString();
                loadEmployeeInfo(Employee_ID, Request_ID);
                loadAssetDtl(Employee_ID);
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
                loadAssetDtl("0");
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
                if (ReqId != "0" || ReqId != null || ReqId != string.Empty)
                {
                    string Query = "UPDATE HRM_LIVE.HRM_EMP_RCI_REQUEST SET TMS_REMARKS = '" + Remarks + "', TMS_APR_ON = SYSDATE, TMS_APR_BY = '" + EmployeeCode + "', REQUEST_STATUS = 'HRTA' WHERE REQUEST_ID = '" + ReqId + "' AND EMP_CD = '" + Employeecd + "'";
                    string Result = db.PostData(Query);
                    if (Result == "Done")
                    {
                        // Gate Pass Detail Update Start
                        string LastDutyDate = string.Empty;
                        foreach (GridViewRow row in gv_LastDutyDtl.Rows)
                        {
                            CheckBox cb_Confirm = (CheckBox)row.FindControl("cb_Confirm");
                            if (cb_Confirm.Checked)
                            {
                                LastDutyDate = gv_LastDutyDtl.Rows[row.RowIndex].Cells[1].Text;
                                string QueryCommunication = "INSERT INTO HRM_LIVE.HRM_EMP_RCI_APPROVALS_LOGS(LOG_ID, REQUEST_NO, ITEM_DESCRIPTIONS, ITEM_STATUS, REMARKS, APPROVAL_DEP, L$USR_IN, L$IN_DATE) ";
                                QueryCommunication += " VALUES ((SELECT NVL(MAX(LOG_ID),0)+1 FROM HRM_LIVE.HRM_EMP_RCI_APPROVALS_LOGS), '" + ReqId + "','Last Duty Date','Yes', '" + LastDutyDate + "', 'Time Office', '" + EmployeeCode + "', sysdate)";
                                string _Result = db.PostData(QueryCommunication);
                                if (_Result == "Done")
                                {

                                }
                            }
                        }
                        // Communication Detail Update End


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

        public void domainBlockEmail()
        {
            try
            {
                //string[] DepNames = lbl_Department.InnerText.Split('-');
                //string DepName = DepNames[1].ToString();


                 string eBody = string.Empty;
                        eBody += "Hi BT Team,"+" <br /><br />";
                        eBody += "Kindly block following details (Domain, & All Applications I.D). " + "<br /><br />";

                        eBody += "Employee Code : " + lbl_EmployeeCode.InnerText + "<br />";
                        eBody += "Employee Name : " + lbl_EmployeeName.InnerText + "<br />";
                        eBody += "Employee Designation : " + lbl_Designation.InnerText + "<br />";
                        eBody += "Employee Designation : " + lbl_Department.InnerText + "<br /><br />";
           
                        eBody += " **System Generated Email** ";

                    SendResponse res = new SendResponse();
                    res.SendEmail("jawwad.bakhtiar@alkaram.com", eBody);
             }
 
            catch (Exception ex)
            {
                
            }
        }

        public void simBlockEmail(string Nummber)
        {
            try
            {
                string eBody = string.Empty;
                eBody += "Hi," + " <br /><br />";
                eBody += "Kindly block below number and share OS till date.. " + "<br /><br />";

                eBody += "Employee Name : " + lbl_EmployeeName.InnerText + "<br />";
                eBody += "Number : " + Nummber + "<br /><br />";

                eBody += " **System Generated Email** ";

                SendResponse res = new SendResponse();
                res.SendEmailJazz("jawwad.bakhtiar@alkaram.com", "SIM Block and Share OS Amount", eBody);
            }

            catch (Exception ex)
            {

            }
        }

        public void MiFiBlockEmail(string Nummber)
        {
            try
            {
                string eBody = string.Empty;
                eBody += "Hi," + " <br /><br />";
                eBody += "Kindly block below number and share OS till date.. " + "<br /><br />";

                eBody += "Employee Name : " + lbl_EmployeeName.InnerText + "<br />";
                eBody += "Number : " + Nummber + "<br /><br />";

                eBody += " **System Generated Email** ";

                SendResponse res = new SendResponse();
                res.SendEmailJazz("jawwad.bakhtiar@alkaram.com", "MIFI Block and Share OS Amount", eBody);
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

        protected void gv_detail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[0].Width = 300;
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Width = 300;
            //e.Row.Cells[3].Width = 300;
            //e.Row.Cells[4].Width = 300;
            //e.Row.Cells[5].Width = 300;
            //e.Row.Cells[6].Width = 300;
            //e.Row.Cells[7].Width = 300;
            //e.Row.Cells[8].Width = 300;
            //e.Row.Cells[9].Width = 300;
        }


    }
}