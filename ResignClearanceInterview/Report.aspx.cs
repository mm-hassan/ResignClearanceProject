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
    public partial class Report : System.Web.UI.Page
    {
        Database db = new Database();
        string EmployeeCode, EmployeeName, EmployeeDesignation;

        string DepartmentName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSession();
            if (!Page.IsPostBack)
            {
                hideControls(); 
               // loadEmployees(EmployeeCode);
                lbl_ConcernDep.InnerText = DepartmentName;
                checkRole(EmployeeCode);
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

        public void hideControls() 
        {
            dd_Employees.Visible = false;

            btn_Statusreport.Visible = false;
            btn_Clearancereport.Visible = false;
            btn_Exitinterviewreport.Visible = false;
            btn_Allpendingstatus.Visible = false;
            btn_Excelreport.Visible = false;
        }

        public void checkRole(string Empcode) 
        {
            try
            {
                DataTable dt = new DataTable();
                string Getquery = "SELECT T.ROLL FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW T WHERE T.EMP_CD = '" + Empcode + "'";
                dt = db.GetData(Getquery);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string Role = dr["ROLL"].ToString();

                        if (Role == "EMPLOYEE")
                        {
                            btn_Statusreport.Visible = true;
                        }
                        if (Role == "IR" || Role == "SUPER" || Role == "HR_TA" || Role == "HR_HEAD" )
                        {
                            btn_Statusreport.Visible = true;
                            btn_Clearancereport.Visible = true;
                            btn_Exitinterviewreport.Visible = true;
                            btn_Allpendingstatus.Visible = true;
                            btn_Excelreport.Visible = true;
                        }
                        if (Role == "IR_OPE")
                        {
                            btn_Clearancereport.Visible = true;
                            btn_Exitinterviewreport.Visible = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                
            }
        
        }



        public void loadEmployees(string EmployeeCode)
        {
            try
            {
                int count = 0;
                DataTable dt = new DataTable();
                string Getquery = "SELECT T.ROLL FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW T WHERE T.EMP_CD = '" + EmployeeCode + "'";
                dt = db.GetData(Getquery);
                if (dt.Rows.Count > 0)
                {
                    string query = string.Empty;
                    query = "SELECT (A.REQUEST_ID || ' - ' || A.EMP_CD) VALUE_ID, (A.REQUEST_ID || ' - ' || A.EMP_CD || ' - ' || B.EMP_NAME) VALUE_TEXT FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_EMPLOYEE B WHERE 1 =1 AND A.EMP_CD = B.EMP_CD";

                    foreach (DataRow dr in dt.Rows)
                    {
                        //string Role = dr["ROLL"].ToString();

                        //if (Role == "EMPLOYEE")
                        //{
                        //    query += " AND B.EMP_CD = '" + EmployeeCode + "'";
                        //}
                        count++;

                    }
                    if (count == 1)
                    {
                        query += " AND B.EMP_CD = '" + EmployeeCode + "'";
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

        //protected void btn_Print_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string[] Ids = dd_Employees.SelectedItem.Text.Split('-');
        //        string RequestId = Ids[0].ToString().Trim();

        //        //string url = "http://172.16.0.8/reports/rwservlet?server=rep_ALL_CUST_2012_all_cust&report=\\172.16.0.8\\erp_live_reg\\HRM0686.rdf&desformat=pdf&destype=CACHE&userid=apps/blacksheep007@aktmwage&P_EMP_CD=" + EmployeeId + "&paramform=no";
        //        string url = "http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\erp_live_reg\HRM0686.rd&P_REQUEST_ID="+RequestId+"&paramform=no";
        //        //url = url.Replace("all_cust&report=", "all_cust&report=\\'");
        //        url = url.Replace("'", "");
        //        //Response.Redirect(url);


        //        string script = "window.open('" + url + "', '_blank');";
        //        ScriptManager.RegisterStartupScript(this, GetType(), "OpenNewTab", script, true);
   

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected void btn_Clearancereport_Click(object sender, EventArgs e)
        {
        try
            {

  
                //string[] Ids = dd_Employees.SelectedItem.Text.Split('-');
                //string RequestId = Ids[0].ToString().Trim();
                string RequestId = lbl_RequestId.InnerText.ToString();

                //string url = "http://172.16.0.8/reports/rwservlet?server=rep_ALL_CUST_2012_all_cust&report=\\172.16.0.8\\erp_live_reg\\HRM0686.rdf&desformat=pdf&destype=CACHE&userid=apps/blacksheep007@aktmwage&P_EMP_CD=" + EmployeeId + "&paramform=no";
                string url = "http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0692.rdf&P_REQUEST_ID=" + RequestId + "&paramform=no";
                url = url.Replace("&report=", "&report=\\");
                url = url.Replace("erp_live_reg", "\\erp_live_reg\\");
                //url = url.Replace("'", "");
                Response.Redirect(url);



                //string script = "window.open('" + url + "', '_blank');";
                //ScriptManager.RegisterStartupScript(this, GetType(), "OpenNewTab", script, true);
   

            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_Statusreport_Click(object sender, EventArgs e)
        {
        try
            {

                //string[] Ids = dd_Employees.SelectedItem.Text.Split('-');
                //string RequestId = Ids[0].ToString().Trim();
                string RequestId = lbl_RequestId.InnerText.ToString();

                //string url = "http://172.16.0.8/reports/rwservlet?server=rep_ALL_CUST_2012_all_cust&report=\\172.16.0.8\\erp_live_reg\\HRM0686.rdf&desformat=pdf&destype=CACHE&userid=apps/blacksheep007@aktmwage&P_EMP_CD=" + EmployeeId + "&paramform=no";
                string url = "http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0686.rdf&P_REQUEST_ID=" + RequestId + "&paramform=no";
                url = url.Replace("&report=", "&report=\\");
                url = url.Replace("erp_live_reg", "\\erp_live_reg\\");
                //url = url.Replace("'", "");
                Response.Redirect(url);


                //string script = "window.open('" + url + "', '_blank');";
                //ScriptManager.RegisterStartupScript(this, GetType(), "OpenNewTab", script, true);
   

            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_Exitinterviewreport_Click(object sender, EventArgs e)
        {
            try
            {

                //string[] Ids = dd_Employees.SelectedItem.Text.Split('-');
                //string RequestId = Ids[0].ToString().Trim();
                string RequestId = lbl_RequestId.InnerText.ToString();


                //string url = "http://172.16.0.8/reports/rwservlet?server=rep_ALL_CUST_2012_all_cust&report=\\172.16.0.8\\erp_live_reg\\HRM0686.rdf&desformat=pdf&destype=CACHE&userid=apps/blacksheep007@aktmwage&P_EMP_CD=" + EmployeeId + "&paramform=no";
                string url = "http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0691.rdf&P_REQUEST_ID=" + RequestId + "&paramform=no";
                url = url.Replace("&report=", "&report=\\");
                url = url.Replace("erp_live_reg", "\\erp_live_reg\\");
                //url = url.Replace("'", "");
                Response.Redirect(url);


                //string script = "window.open('" + url + "', '_blank');";
                //ScriptManager.RegisterStartupScript(this, GetType(), "OpenNewTab", script, true);
   

            }
            catch (Exception ex)
            {

            }
        }



        protected void btn_Allpendingstatus_Click(object sender, EventArgs e)
        {
            try
            {
                 string url = "http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0699.rdf&paramform=no";
                        url = url.Replace("&report=", "&report=\\");
                        url = url.Replace("erp_live_reg", "\\erp_live_reg\\");
                        Response.Redirect(url);
            }
            catch (Exception ex)
            {
                
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

                    lbl_RequestId.InnerText = dr["REQUEST_ID"].ToString();

                    lbl_Msg.InnerText = string.Empty;
                }
                else 
                {
   
                    lbl_Msg.InnerText = "Resign not exist against this number";
                    lbl_RequestId.InnerText = string.Empty;
                    getImage("0101");
                }

            }
            catch (Exception ex)
            {
                
           
            }
        }

        protected void btn_Excelreport_Click(object sender, EventArgs e)
        {
            try
                {
                    DataTable dt = new DataTable();
                    string query = "SELECT * FROM HRM_LIVE.HRM_EMP_RCI_PEN_APPROVALS_V";
                    dt = db.GetData(query);
                    if (dt.Rows.Count > 0)
                    {
                        gv_Details.DataSource = dt;
                        gv_Details.DataBind();

                        dataExportexcel();
                    }
                    else
                    {
                        gv_Details.DataSource = dt;
                        gv_Details.DataBind();
                    }
                }
            catch (Exception ex)
            {

            }




            
        }



        public void dataExportexcel()
        {
            try
            {
                string filename = String.Format("Results_{0}_{1}.xls", DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString());
                if (!string.IsNullOrEmpty(gv_Details.Page.Title))
                    filename = gv_Details.Page.Title + ".xls";

                HttpContext.Current.Response.Clear();

                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);

                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.Charset = "";

                System.IO.StringWriter stringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);


                System.Web.UI.HtmlControls.HtmlForm form = new System.Web.UI.HtmlControls.HtmlForm();
                gv_Details.Parent.Controls.Add(form);
                form.Controls.Add(gv_Details);
                form.RenderControl(htmlWriter);

                HttpContext.Current.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                HttpContext.Current.Response.Write(stringWriter.ToString());
                HttpContext.Current.Response.End();

            }
            catch (Exception ex)
            {
                
            }
        
        }






        protected void btn_GenerateReport_Click(object sender, EventArgs e)
        {
            // Retrieve values from text boxes
            string fromDate = txtFromDate.Text;
            string toDate = txtToDate.Text;


            string url = "http://172.16.0.8/reports/rwservlet?link_idl&report=\\172.16.0.8\\erp_live_reg\\HRM0713.rdf&P_from_date=" + fromDate + "&P_TO_DATE=" + toDate + "&paramform=no";
            url = url.Replace("&report=", "&report=\\");
            url = url.Replace("erp_live_reg", "\\erp_live_reg\\");
            //url = url.Replace("'", "");
            Response.Redirect(url);

        }

        
 



    }
}