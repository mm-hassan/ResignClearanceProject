using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResignClearanceInterview
{
    public partial class IR : System.Web.UI.Page
    {
        Database db = new Database();
        string EmployeeCode, EmployeeName, EmployeeDesignation;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSession();

           
            if (!Page.IsPostBack)
            {
                //getImage(EmployeeCode);
                //loadEmployeeInfo(EmployeeCode);
                loadGrid();
                loadResignType();
                loadResignReason();
                txt_LastDutyDate.Text = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
            }
        }


        public void unload() 
        {
            loadGrid();
            loadResignType();
            loadResignReason();
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



        public void getImage(string val)
        {
            try
            {
                byte[] bytes;
                string fileName;
                string orcconstring = db.getConnectionOracleCustom();
                OracleConnection orccon = new OracleConnection(orcconstring);
                orccon.Open();
                string msql = "select i.image from hrm_employee e, hrm_employee_image i where e.emp_cd = '" + val + "' and e.EMP_CD = i.emp_cd and e.emp_status IN ('A', 'D','S')";
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

            }
        }
        
        public void loadResignType() 
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT D.DETAIL_ID, D.DETAIL_NAME FROM HRM_SETUP_DETL D WHERE D.SEQ_NO = 132 Order By D.DETAIL_ID";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    dd_ResignType.DataSource = dt;
                    dd_ResignType.DataTextField = "DETAIL_NAME";
                    dd_ResignType.DataValueField = "DETAIL_ID";
                    dd_ResignType.DataBind();
                    dd_ResignType.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void loadResignReason()
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT D.DETAIL_ID, D.DETAIL_NAME FROM HRM_SETUP_DETL D WHERE D.SEQ_NO = 133 Order By D.DETAIL_ID";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    dd_ResignReason.DataSource = dt;
                    dd_ResignReason.DataTextField = "DETAIL_NAME";
                    dd_ResignReason.DataValueField = "DETAIL_ID";
                    dd_ResignReason.DataBind();
                    dd_ResignReason.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void loadEmployeeInfo(string EmployeeCode) 
        {
            try
            {
                DataTable dt = new DataTable();
                string query = "SELECT A.EMPLOYEE_NAME, A.DEPARTMENT_NAME, A.DESIGNATION_NAME, A.LINE_MANAGER_NAME, A.HOD_NAME, B.MOBILE_NO, TO_CHAR(B.APPOINTMENT_DATE)APPOINTMENT_DATE, B.PRESENT_ADDRESS FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW A, HRM_EMPLOYEE B where 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.EMP_CD = '" + EmployeeCode + "'";
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        lbl_EmployeeName.InnerText = dr["EMPLOYEE_NAME"].ToString();
                        lbl_Designation.InnerText = dr["DESIGNATION_NAME"].ToString();
                        lbl_Address.InnerText = dr["PRESENT_ADDRESS"].ToString();
                        lbl_PhoneNo.InnerText = dr["MOBILE_NO"].ToString();
                        lbl_Department.InnerText = dr["DEPARTMENT_NAME"].ToString();
                        lbl_Linemanager.InnerText = dr["LINE_MANAGER_NAME"].ToString();
                        lbl_HodName.InnerText = dr["HOD_NAME"].ToString();
                        lbl_DateofJoining.InnerText = dr["APPOINTMENT_DATE"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void loadGrid() 
        {
            try
            {
                string query = "SELECT * FROM HRM_LIVE.HRM_EMP_RCI_QUESTIONS";
                DataTable dt = new DataTable();
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    gv_Questions.DataSource = dt;
                    gv_Questions.DataBind();
                }
            }
            catch (Exception ex)
            {
                
            }
        
        }

        protected void gv_detail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;
            //e.Row.Cells[2].Visible = false;
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                string EmpCode = txt_Employeecode.Text.ToString();

                int cdrCd = Convert.ToInt32(db.GetCrdCd("SELECT A.HRM_CADRE_CD FROM HRM_EMPLOYEE A WHERE A.EMP_CD='" + EmpCode + "'"));

                string ResignType = dd_ResignType.SelectedValue;
                string ResignReason = dd_ResignReason.SelectedValue;

                if (ResignType == "0" || ResignReason == "0")
                {
                    lbl_Msg.Text = "Please select Resign Type or Resign Reason.";
                    lbl_Msg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    // Duplication Check
                    string query = "SELECT A.REQUEST_ID FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A WHERE A.EMP_CD = '" + EmpCode + "' AND A.REQUEST_STATUS NOT IN ('HRTR') ORDER BY A.REQUEST_ID DESC";
                    DataTable dt = new DataTable();
                    dt = db.GetData(query);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        lbl_Msg.Text = "Resign already submited request no. " + dr["REQUEST_ID"].ToString();
                        lbl_Msg.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        if (cdrCd >= 12)
                        {
                            DateTime LastDutyDate = Convert.ToDateTime(txt_LastDutyDate.Text);
                            string Remarks = txt_Remarks.Text;
                            string Query = "INSERT INTO HRM_LIVE.HRM_EMP_RCI_REQUEST(REQUEST_ID, EMP_CD, RESIGN_TYPE, RESIGN_REASON, LAST_DUTY_DATE, REMARKS, ISACTIVE, L$USR_IN, L$IN_DATE, REQUEST_STATUS) ";
                            Query += " VALUES((SELECT NVL(MAX(A.REQUEST_ID), 0) + 1 FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A), '" + EmpCode + "', '" + ResignType + "', '" + ResignReason + "', TO_DATE('" + LastDutyDate + "', 'mm/dd/yyyy hh:mi:ss am'), '" + Remarks + "', 'Y',  '" + EmployeeCode + "', sysdate, 'NR')";

                            string Result = db.PostData(Query);

                            if (Result == "Done")
                            {
                                insertInterviewQuestions();
                                EmailIntimationHOD(EmpCode);
                                lbl_Msg.Text = "Request has been submitted!";
                                lbl_Msg.ForeColor = System.Drawing.Color.Green;
                                unload();
                            }
                        }
                        else
                        {
                            if (FileUpload1.HasFile)
                            {
                                DateTime LastDutyDate = Convert.ToDateTime(txt_LastDutyDate.Text);
                                string Remarks = txt_Remarks.Text;

                                string fileName = FileUpload1.FileName.ToLower();
                                string fileExtension = Path.GetExtension(fileName).ToLower();
                                if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".gif")
                                {
                                    DateTime currentDateTime = DateTime.Now;

                                    using (Stream fs = FileUpload1.PostedFile.InputStream)
                                    {
                                        using (BinaryReader br = new BinaryReader(fs))
                                        {
                                            string orcconstring = db.getConnectionOracleCustom();
                                            OracleConnection con = new OracleConnection(orcconstring);
                                            byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                            string Query = "INSERT INTO HRM_LIVE.HRM_EMP_RCI_REQUEST(REQUEST_ID, EMP_CD, RESIGN_TYPE, RESIGN_REASON, LAST_DUTY_DATE, REMARKS, ISACTIVE, L$USR_IN, L$IN_DATE, REQUEST_STATUS,resign_attachment) ";
                                            Query += " VALUES((SELECT NVL(MAX(A.REQUEST_ID), 0) + 1 FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A), '" + EmpCode + "', '" + ResignType + "', '" + ResignReason + "', TO_DATE('" + LastDutyDate + "', 'mm/dd/yyyy hh:mi:ss am'), '" + Remarks + "', 'Y',  '" + EmployeeCode + "', sysdate, 'NR',:Data)";

                                            using (OracleCommand cmd = new OracleCommand(Query))
                                            {
                                                if (bytes != null)
                                                {
                                                    cmd.Parameters.Add(":Data", bytes);
                                                }
                                                else
                                                {
                                                    string message = "Attachment is required.";
                                                    string script = "<script type='text/javascript'>alert('" + message + "');</script>";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                                                }

                                                cmd.Connection = con;
                                                con.Open();
                                                int res = cmd.ExecuteNonQuery();
                                                con.Close();
                                                if (res > 0)
                                                {
                                                    //*temp comment for testing rz 25-07-2023*//
                                                    insertInterviewQuestions();
                                                    EmailIntimationHOD(EmpCode);
                                                    lbl_Msg.Text = "Request has been submitted.";
                                                    lbl_Msg.ForeColor = System.Drawing.Color.Green;
                                                    unload();
                                                }
                                                else
                                                {
                                                    string message = "Can not make request.";
                                                    string script = "<script type='text/javascript'>alert('" + message + "');</script>";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                                                }

                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    string message = "Please select .jpg .png .jpeg .gif";
                                    string script = "<script type='text/javascript'>alert('" + message + "');</script>";
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                                }
                            }
                            else
                            {
                                string message = "Please select .jpg .png .jpeg .gif";
                                string script = "<script type='text/javascript'>alert('" + message + "');</script>";
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            }
                        }
                 
                        
                        //string Query = "INSERT INTO HRM_LIVE.HRM_EMP_RCI_REQUEST(REQUEST_ID, EMP_CD, RESIGN_TYPE, RESIGN_REASON, LAST_DUTY_DATE, REMARKS, ISACTIVE, L$USR_IN, L$IN_DATE, REQUEST_STATUS) ";
                        //Query += " VALUES((SELECT NVL(MAX(A.REQUEST_ID), 0) + 1 FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A), '" + EmpCode + "', '" + ResignType + "', '" + ResignReason + "', TO_DATE('" + LastDutyDate + "', 'mm/dd/yyyy hh:mi:ss am'), '" + Remarks + "', 'Y',  '" + EmployeeCode + "', sysdate, 'NR')";
                        //string Result = db.PostData(Query);
                        //if (Result == "Done")
                        //{
                        //    insertInterviewQuestions();
                        //    EmailIntimationHOD(EmpCode);
                        //    lbl_Msg.Text = "Resign has been submitted!";
                        //    unload();
                        //}

                    }
                }

                //string EmpCode = txt_Employeecode.Text.ToString();
                //string ResignType = dd_ResignType.SelectedValue;
                //string ResignReason = dd_ResignReason.SelectedValue;

                //DateTime LastDutyDate = Convert.ToDateTime(txt_LastDutyDate.Text);
                //string Remarks = txt_Remarks.Text;

                //string Query = "INSERT INTO HRM_LIVE.HRM_EMP_RCI_REQUEST(REQUEST_ID, EMP_CD, RESIGN_TYPE, RESIGN_REASON, LAST_DUTY_DATE, REMARKS, ISACTIVE, L$USR_IN, L$IN_DATE, REQUEST_STATUS) ";
                //Query += " VALUES((SELECT NVL(MAX(A.REQUEST_ID), 0) + 1 FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A), '" + EmpCode + "', '" + ResignType + "', '" + ResignReason + "', TO_DATE('" + LastDutyDate + "', 'mm/dd/yyyy hh:mi:ss am'), '" + Remarks + "', 'Y',  '" + EmployeeCode + "', sysdate, 'NR')";
                //string Result = db.PostData(Query);
                //if (Result == "Done")
                //{
                //    insertInterviewQuestions();
                //    //EmailIntimationHOD(EmpCode);
                //    lbl_Msg.Text = "Request has been submitted!";
                //    unload(); 
                //}

            }
            catch (Exception ex)
            {
                
            }
        }


        public void insertInterviewQuestions() 
        {
            try
            {
                int QuestionId = 0;
                string Rating = string.Empty;
                foreach (GridViewRow row in gv_Questions.Rows)
                { 
                    QuestionId = Convert.ToInt32(gv_Questions.Rows[row.RowIndex].Cells[0].Text);
                    //TextBox txt_Rating = (TextBox)row.FindControl("txt_Rating");
                    DropDownList dd_Rating = (DropDownList)row.FindControl("dd_Rating");
                    Rating = dd_Rating.Text.ToString();
                    string Query = "INSERT INTO HRM_LIVE.HRM_EMP_RCI_ANSWERS ( SEQ_NO, REQUEST_ID, QUESTION_SEQ_NO, ANSWERS_RATING, ISACTIVE, L$USR_IN, L$IN_DATE ) ";
                    Query += " VALUES ( (SELECT NVL(MAX(A.SEQ_NO),0) + 1 FROM HRM_LIVE.HRM_EMP_RCI_ANSWERS A), (SELECT MAX(B.REQUEST_ID) FROM HRM_LIVE.HRM_EMP_RCI_REQUEST B WHERE B.EMP_CD = '" + txt_Employeecode.Text + "'), '" + QuestionId + "',  '" + Rating + "', 'Y', '" + EmployeeCode + "', SYSDATE)";
                    string Result = db.PostData(Query);
                }

                
            }
            catch (Exception ex)
            {
                
            }
        }



        public void EmailIntimationHOD(string EmployeeCode) 
        {
            try
            {
                string eBody = string.Empty;

                string query = "SELECT A.*, B.EMP_HOD_CD, B.HOD_NAME, (SELECT C.E_MAIL FROM HRM_EMPLOYEE C WHERE C.EMP_CD = B.EMP_HOD_CD) E_MAIL, (SELECT C.MOBILE_NO FROM HRM_EMPLOYEE C WHERE C.EMP_CD = B.EMP_HOD_CD) FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.EMP_CD = '" + EmployeeCode + "'";
                DataTable dt = new DataTable();
                dt = db.GetData(query);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    eBody += "Hi " + dr["HOD_NAME"].ToString() + ",<br /><br />";

                    eBody += "Please check the portal for a new resignation request with the employee details listed below." + "<br /><br />";

                    eBody += "Employee Code : " + dr["EMP_CD"].ToString() + "<br />";
                    eBody += "Employee Name : " + dr["EMP_NAME"].ToString() + "<br />";
                    eBody += "Employee Designation : " + dr["DESIGNATION_NAME"].ToString() + "<br />";
                    eBody += "Employee Department : " + dr["DEPARTMENT_NAME"].ToString() + "<br /><br />";

                    eBody += " **System Generated Email** ";

                    SendResponse res = new SendResponse();
                    res.SendEmail(dr["E_MAIL"].ToString(), eBody);
                }



            }
            catch (Exception ex)
            {
                
            }
        
        }

        protected void dd_ResignType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ResignType = dd_ResignType.Text;
                if (ResignType == "1")
                {
                    txt_LastDutyDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else if (ResignType == "2")
                {
                    txt_LastDutyDate.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void txt_Employeecode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Empcd = txt_Employeecode.Text.ToString();
           
                getImage(Empcd);
                loadEmployeeInfo(Empcd);

                int cdrCd = Convert.ToInt32(db.GetCrdCd("SELECT A.HRM_CADRE_CD FROM HRM_EMPLOYEE A WHERE A.EMP_CD='" + Empcd + "'"));
                if (cdrCd == 0)
                {
                    lbl_Msg.Text = "Employee Cardre not define in our system";
                    lbl_Msg.ForeColor = System.Drawing.Color.Red;
                }
                else if (cdrCd >= 12)
                {
                    img_d.Visible = false;
                }

                else
                {
                    img_d.Visible = true;
                }
            

            }
            catch (Exception ex)
            {

            }
        }


    }
}