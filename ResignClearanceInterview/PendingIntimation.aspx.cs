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
    public partial class PendingIntimation : System.Web.UI.Page
    {
        Database db = new Database();
        string EmployeeCode, EmployeeName, EmployeeDesignation, EmployeeDepartment;

        string DepartmentName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSession();
            if (!Page.IsPostBack)
            {
                //loadEmployees(EmployeeCode);
                dd_Employees.Visible = false;
                lbl_ConcernDep.InnerText = EmployeeName + " - " + EmployeeDepartment;
            }
        }

        public void getSession()
        {
            if (Session["EmployeeCode"] != null)
            {
                EmployeeCode = Session["EmployeeCode"].ToString();
                EmployeeName = Session["EmployeeName"].ToString();
                EmployeeDesignation = Session["EmployeeDesignation"].ToString();
                EmployeeDepartment = Session["EmployeeDepartment"].ToString();
            }
            else
            {
                Response.Redirect("LockScreen.aspx");
            }
        }



        public void loadEmployees(string EmployeeCode)
        {
            try
            {
                DataTable dt = new DataTable();
                string Getquery = "SELECT T.ROLL FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW T WHERE T.EMP_CD = '" + EmployeeCode + "'";
                dt = db.GetData(Getquery);
                if (dt.Rows.Count > 0)
                {
                    string query = string.Empty;
                    query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B WHERE 1 =1 ";
                            

                    foreach (DataRow dr in dt.Rows)
                    {
                        string Role = dr["ROLL"].ToString();

                        if (Role == "SUPER")
                        {
                            //query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B";
                            query += " AND A.EMP_CD = B.EMP_CD";
                            DepartmentName = "Supper User";
                        }

                        else if (Role == "HOD")
                        {
                            //query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B";
                            query += " AND A.EMP_CD = B.EMP_CD";
                            DepartmentName = "Department Head";
                        }
                        else if (Role == "HR_TA")
                        {
                            //query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B";
                            query += " AND A.EMP_CD = B.EMP_CD AND A.HR_TA_APR_ON IS NULL";
                            DepartmentName = "HR Business Partner";
                        }
                        else if (Role == "HR_HEAD")
                        {
                            //query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B"; 
                            query += " AND A.EMP_CD = B.EMP_CD AND A.HR_TA_APR_ON IS NULL";
                            DepartmentName = "HR Head";
                        }
                        else if (Role == "BT")
                        {
                            //query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B";
                            query += " AND A.EMP_CD = B.EMP_CD AND A.BT_APR_ON IS NULL AND (A.HR_TA_APR_ON IS NOT NULL OR A.HR_HOD_APR_ON IS NOT NULL)";
                            DepartmentName = "Bussiness Technology";
                        }
                        else if (Role == "FIN")
                        {
                            //query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B";
                            query += " AND A.EMP_CD = B.EMP_CD AND A.ACCOUNT_APR_ON IS NULL AND (A.HR_TA_APR_ON IS NOT NULL OR A.HR_HOD_APR_ON IS NOT NULL)";
                            DepartmentName = "Accounts Payroll";
                        }
                        else if (Role == "ADMIN")
                        {
                           // query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B";
                            query += " AND A.EMP_CD = B.EMP_CD AND A.ADMIN_APR_ON IS NULL AND (A.HR_TA_APR_ON IS NOT NULL OR A.HR_HOD_APR_ON IS NOT NULL)";
                            DepartmentName = "Administration";
                        }
                        else if (Role == "HR")
                        {
                            //query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B";
                            query += " AND A.EMP_CD = B.EMP_CD AND A.HR_APR_ON IS NULL AND (A.HR_TA_APR_ON IS NOT NULL OR A.HR_HOD_APR_ON IS NOT NULL)";
                            DepartmentName = "Human Resource";
                        }
                        else if (Role == "STORE")
                        {
                            //query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B"; 
                            query += " AND A.EMP_CD = B.EMP_CD AND A.STORE_APR_ON IS NULL AND (A.HR_TA_APR_ON IS NOT NULL OR A.HR_HOD_APR_ON IS NOT NULL)";
                            DepartmentName = "Central Store";
                        }
                        else if (Role == "TMS")
                        {
                            //query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B";
                            query += " AND A.EMP_CD = B.EMP_CD AND A.TMS_APR_ON IS NULL AND (A.HR_TA_APR_ON IS NOT NULL OR A.HR_HOD_APR_ON IS NOT NULL)";
                            DepartmentName = "Time Management System";
                        }
                        //else if (Role == "IR")
                        //{
                        //    //query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B"; 
                        //    query += " AND A.EMP_CD = B.EMP_CD AND A.IR_APR_ON IS NULL AND (A.HR_TA_APR_ON IS NOT NULL OR A.HR_HOD_APR_ON IS NOT NULL)";
                        //    DepartmentName = "IR";
                        //}
                    }

                    DataTable dt1 = new DataTable();
                    dt1 = db.GetData(query);
                    if (dt1.Rows.Count > 0)
                    {
                        dd_Employees.DataSource = dt1;
                        dd_Employees.DataTextField = "VALUE_TEXT";
                        dd_Employees.DataValueField = "VALUE_ID";
                        dd_Employees.DataBind();
                        dd_Employees.Items.Insert(0, new ListItem("Select Employee", "0"));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void IntimationEmailSMS(string emailAddress, string PhoneNo)
        {
            try
            {
                string empCd = txt_Employeeno.Text.ToString();

                string EmployeeName = db.getEmpName("SELECT A.EMP_NAME FROM HRM_EMPLOYEE A WHERE A.EMP_CD = '" + empCd + "'");


                SendResponse res = new SendResponse();


                //########## Email Body
                 string eBody = string.Empty;
                        eBody += "Hi "+ EmployeeName + "<br /><br />";
                        eBody += txt_Message.Text + "<br /><br />";
                        eBody += "Please Contact to Mr. " + lbl_ConcernDep.InnerText + " Department." + "<br />";
                        eBody += " **AKTM** ";
                        res.SendEmail(emailAddress, eBody);
                //########## Email Body

                //########## SMS Body
                  string sBody = string.Empty;
                         sBody += "Hi " + EmployeeName + "\n";
                         sBody += txt_Message.Text + "\n";
                         sBody += "Please Contact to Mr. " + lbl_ConcernDep.InnerText + " Department." + "\n";
                         sBody += " **AKTM** ";
                         res.SendSmS(PhoneNo, sBody);
                //########## SMS Body

                  lbl_Msg.InnerText = "Sent";
             }
 
            catch (Exception ex)
            {
                
            }
        }

        protected void dd_Employees_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                string[] Ids = dd_Employees.Text.Split('-');
                string Request_ID = Ids[0].ToString();
                string Employee_ID = Ids[1].ToString();
                loadEmployeeInfo(Employee_ID, Request_ID);
                getImage(Employee_ID);

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        protected void txt_Employeeno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Employee_ID = txt_Employeeno.Text.Trim();
                DataTable dt = new DataTable();
                string query = "SELECT MAX(A.REQUEST_ID)REQUEST_ID, A.EMP_CD FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A WHERE A.EMP_CD IN ('" + Employee_ID + "') GROUP BY A.EMP_CD";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    loadEmployeeInfo(dr["EMP_CD"].ToString(), dr["REQUEST_ID"].ToString());
                    getImage(Employee_ID);
                    lbl_MsgEmp.InnerText = string.Empty;
                }
                else
                {
                    lbl_MsgEmp.InnerText = "Resign not exist against this number";
                    getImage("0101");
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
                //string fileName;
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


        public void loadEmployeeInfo(string EmployeeCode, string RequestID)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT A.EMP_CD, A.EMPLOYEE_NAME, A.DEPARTMENT_NAME, A.DESIGNATION_NAME, B.E_MAIL, A.HOD_NAME, B.MOBILE_NO, TO_CHAR(B.APPOINTMENT_DATE) APPOINTMENT_DATE, B.PRESENT_ADDRESS, (SELECT D.DETAIL_NAME FROM HRM_SETUP_DETL D WHERE D.SEQ_NO = 132 AND D.DETAIL_ID = C.RESIGN_TYPE) RESIGN_TYPE, (SELECT D.DETAIL_NAME FROM HRM_SETUP_DETL D WHERE D.SEQ_NO = 133 AND D.DETAIL_ID = C.RESIGN_REASON) RESIGN_REASON, (TO_DATE(C.LAST_DUTY_DATE) - TO_DATE(C.L$IN_DATE)) NOTICE_PERIODS_DAYS, TO_CHAR(C.LAST_DUTY_DATE)LAST_DUTY_DATE FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW A, HRM_EMPLOYEE B, HRM_LIVE.HRM_EMP_RCI_REQUEST    C where 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.EMP_CD = C.EMP_CD AND B.EMP_CD = C.EMP_CD AND A.EMP_CD = '" + EmployeeCode + "' AND C.REQUEST_ID ='" + RequestID + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        
                        lbl_Designation.InnerText = dr["DESIGNATION_NAME"].ToString();
                        lbl_Department.InnerText = dr["DEPARTMENT_NAME"].ToString();
                        lbl_Email.InnerText = dr["E_MAIL"].ToString();
                        lbl_Phoneno.InnerText = dr["MOBILE_NO"].ToString().Replace("-","");
                    }
                }
                else
                {
                    lbl_Email.InnerText = "hafizismail@alkaram.com";
                    lbl_Phoneno.InnerText = "923132014201";
                    lbl_Designation.InnerText = "Software Developer";
                    lbl_Department.InnerText = "Business Technology";
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_Sent_Click(object sender, EventArgs e)
        {
            try
            {
                IntimationEmailSMS(lbl_Email.InnerText.ToString(), lbl_Phoneno.InnerText.ToString());


            }
            catch (Exception ex)
            {
                
            }
        }


 



    }
}