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
    public partial class Store : System.Web.UI.Page
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
                //string query = "select REQUEST_NUMBER, ITEM_ID, ITEM_DESC, DESCRIPTION, PRIMARY_UOM_CODE, EMP_CD, EMP_NAME, DESIGNATION_NAME, DIVISION_NAME, DEPARTMENT_NAME, PRESENT_ADDRESS, TRANSACTION_ID, QUANTITY, TRANSACTION_DATE, STATUS, EXPIRY_DATE, SEQ, LOC from (select distinct mth.request_number, mtl.inventory_item_id item_id, msi.segment1 || '.' || msi.segment2 || '.' || msi.segment3 || '.' || msi.segment4 || '.' || msi.segment5 || '.' || msi.segment6 Item_desc, ak_func_seg_desc(msi.segment1, 'AK_ITEM_NATURE', 0) || '.' || ak_func_seg_desc(msi.segment2, 'AK_ITEM_QUALITY', msi.segment1) || '.' || ak_func_seg_desc(msi.segment3, 'AK_ITEM_WIDTH', msi.segment1) || '.' || replace(ak_func_seg_desc(msi.segment4, 'AK_ITEM_CCS', msi.segment1), 'Unsp', '') || replace(ak_func_seg_desc(msi.segment5, 'AK_ITEM_ORIGIN', msi.segment1), 'Unsp', '') || replace(ak_func_seg_desc(msi.segment6, 'AK_ITEM_FUTURE', msi.segment1), 'Unsp', '') description, msi.primary_uom_code, emp.emp_cd, emp.emp_name, emp.designation_name, emp.division_name, emp.department_name, emp.present_address, mmt.transaction_id, mmt.transaction_quantity * -1 quantity, mmt.transaction_date, 'Issued' Status, to_date(mtl.attribute2, 'YYYY/MM/DD HH24:MI:SS') expiry_date, 1 seq, mil.segment1 || '.' || mil.segment2 || '.' || mil.segment3 || '.' || mil.segment4 loc from mtl_txn_request_headers   mth, mtl_txn_request_lines     mtl, mtl_system_items_b        msi, LEGACY_HR_EMPLOYEE        emp, mtl_material_transactions mmt, mtl_item_locations        mil where mth.header_id = mtl.header_id and mtl.transaction_type_id = 2160 and mtl.inventory_item_id = msi.inventory_item_id and mtl.organization_id = msi.organization_id and mtl.attribute_category = '2160' and mtl.attribute1 = emp.emp_cd and mtl.line_id = mmt.move_order_line_id and mtl.organization_id = mmt.organization_id and mmt.locator_id = mil.inventory_location_id and mmt.organization_id = mil.organization_id and emp.emp_cd = nvl('" + EmployeeID + "', emp.emp_cd) union all select distinct null request_number, mtl.inventory_item_id item_id, msi.segment1 || '.' || msi.segment2 || '.' || msi.segment3 || '.' || msi.segment4 || '.' || msi.segment5 || '.' || msi.segment6 Item_desc, ak_func_seg_desc(msi.segment1, 'AK_ITEM_NATURE', 0) || '.' || ak_func_seg_desc(msi.segment2, 'AK_ITEM_QUALITY', msi.segment1) || '.' || ak_func_seg_desc(msi.segment3, 'AK_ITEM_WIDTH', msi.segment1) || '.' || replace(ak_func_seg_desc(msi.segment4, 'AK_ITEM_CCS', msi.segment1), 'Unsp', '') || replace(ak_func_seg_desc(msi.segment5, 'AK_ITEM_ORIGIN', msi.segment1), 'Unsp', '') || replace(ak_func_seg_desc(msi.segment6, 'AK_ITEM_FUTURE', msi.segment1), 'Unsp', '') description, rec.transaction_uom primary_uom_code, emp.emp_cd, emp.emp_name, emp.designation_name, emp.division_name, emp.department_name, emp.present_address, rec.transaction_id, rec.transaction_quantity quantity, rec.transaction_date, 'Return' Status, to_date(mtl.attribute2, 'YYYY/MM/DD HH24:MI:SS') expiry_date, 3 seq, mil.segment1 || '.' || mil.segment2 || '.' || mil.segment3 || '.' || mil.segment4 loc from mtl_txn_request_headers   mth, mtl_txn_request_lines     mtl, mtl_system_items_b        msi, LEGACY_HR_EMPLOYEE        emp, mtl_material_transactions mmt, mtl_material_transactions rec, mtl_item_locations        mil where mth.header_id = mtl.header_id and mtl.transaction_type_id = 2160 and mtl.attribute_category = '2160' and mtl.attribute1 = emp.emp_cd and mtl.line_id = mmt.move_order_line_id and mtl.organization_id = mmt.organization_id and rec.inventory_item_id = msi.inventory_item_id and rec.organization_id = msi.organization_id and rec.attribute1 = mmt.transaction_id and rec.attribute2 = emp.emp_cd and rec.transaction_type_id = 2161 and rec.locator_id = mil.inventory_location_id and rec.organization_id = mil.organization_id and emp.emp_cd = nvl('" + EmployeeID + "', emp.emp_cd) union all select distinct rec.attribute2 request_number, mSI.inventory_item_id item_id, msi.segment1 || '.' || msi.segment2 || '.' || msi.segment3 || '.' || msi.segment4 || '.' || msi.segment5 || '.' || msi.segment6 Item_desc, ak_func_seg_desc(msi.segment1, 'AK_ITEM_NATURE', 0) || '.' || ak_func_seg_desc(msi.segment2, 'AK_ITEM_QUALITY', msi.segment1) || '.' || ak_func_seg_desc(msi.segment3, 'AK_ITEM_WIDTH', msi.segment1) || '.' || replace(ak_func_seg_desc(msi.segment4, 'AK_ITEM_CCS', msi.segment1), 'Unsp', '') || replace(ak_func_seg_desc(msi.segment5, 'AK_ITEM_ORIGIN', msi.segment1), 'Unsp', '') || replace(ak_func_seg_desc(msi.segment6, 'AK_ITEM_FUTURE', msi.segment1), 'Unsp', '') description, rec.transaction_uom primary_uom_code, emp.emp_cd, emp.emp_name, emp.designation_name, emp.division_name, emp.department_name, emp.present_address, rec.transaction_id, rec.transaction_quantity quantity, rec.transaction_date, 'Return' Status, NULL expiry_date, 4 seq, mil.segment1 || '.' || mil.segment2 || '.' || mil.segment3 || '.' || mil.segment4 loc from mtl_material_transactions rec, mtl_item_locations        mil, mtl_system_items_b        msi, LEGACY_HR_EMPLOYEE        emp where 1 = 1 and rec.transaction_type_id = '1997' and rec.inventory_item_id = msi.inventory_item_id and rec.organization_id = msi.organization_id and rec.locator_id = mil.inventory_location_id and rec.organization_id = mil.organization_id and rec.attribute3 = TO_CHAR(emp.emp_cd(+)) and emp.emp_cd = nvl('" + EmployeeID + "', emp.emp_cd) union all select null request_number, mtl.inventory_item_id item_id, msi.segment1 || '.' || msi.segment2 || '.' || msi.segment3 || '.' || msi.segment4 || '.' || msi.segment5 || '.' || msi.segment6 Item_desc, ak_func_seg_desc(msi.segment1, 'AK_ITEM_NATURE', 0) || '.' || ak_func_seg_desc(msi.segment2, 'AK_ITEM_QUALITY', msi.segment1) || '.' || ak_func_seg_desc(msi.segment3, 'AK_ITEM_WIDTH', msi.segment1) || '.' || replace(ak_func_seg_desc(msi.segment4, 'AK_ITEM_CCS', msi.segment1), 'Unsp', '') || replace(ak_func_seg_desc(msi.segment5, 'AK_ITEM_ORIGIN', msi.segment1), 'Unsp', '') || replace(ak_func_seg_desc(msi.segment6, 'AK_ITEM_FUTURE', msi.segment1), 'Unsp', '') description, rec.transaction_uom primary_uom_code, emp.emp_cd, emp.emp_name, emp.designation_name, emp.division_name, emp.department_name, emp.present_address, rec.transaction_id, rec.transaction_quantity quantity, mmt.transaction_date, 'Incorrect Issuance' Status, to_date(mtl.attribute2, 'YYYY/MM/DD HH24:MI:SS') expiry_date, 2 seq, mil.segment1 || '.' || mil.segment2 || '.' || mil.segment3 || '.' || mil.segment4 loc from mtl_txn_request_headers   mth, mtl_txn_request_lines     mtl, mtl_system_items_b        msi, LEGACY_HR_EMPLOYEE        emp, mtl_material_transactions mmt, mtl_material_transactions rec, mtl_item_locations        mil where mth.header_id = mtl.header_id and mtl.transaction_type_id = 2160 and mtl.attribute_category = '2160' and mtl.attribute1 = emp.emp_cd and mtl.line_id = mmt.move_order_line_id and mtl.organization_id = mmt.organization_id and rec.inventory_item_id = msi.inventory_item_id and rec.organization_id = msi.organization_id and rec.attribute4 = to_char(mmt.transaction_id) and rec.locator_id = mil.inventory_location_id and rec.organization_id = mil.organization_id and rec.transaction_type_id = 681 and emp.emp_cd = nvl('" + EmployeeID + "', emp.emp_cd) union all select null request_number, null item_id, null Item_desc, null description, null                 primary_uom_code, emp.emp_cd, emp.emp_name, emp.designation_name, emp.division_name, emp.department_name, emp.present_address, null                 transaction_id, null                 quantity, null                 transaction_date, null                 Status, null                 expiry_date, 4                    seq, null from LEGACY_HR_EMPLOYEE emp where emp.emp_cd = nvl('" + EmployeeID + "', emp.emp_cd) order by item_id, seq ) where STATUS is not null";
                string query = "select * from apps.RC_STORE_PENDING_ASSETS where emp_cd= '" + EmployeeID + "'";
                dt = db.GetDataEbs(query);
                if (dt.Rows.Count > 0)
                {
                    div_Cardbody.Visible = true;
                    lbl_AssetMsg.Text = "Please collect the asset from the employee, then approve the request.";
                    gv_AssetDtl.DataSource = dt;
                    gv_AssetDtl.DataBind();
                }
                else
                {
                    div_Cardbody.Visible = false;
                    lbl_AssetMsg.Text = "No asset found on this employee.";
                    gv_AssetDtl.DataSource = dt;
                    gv_AssetDtl.DataBind();
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
                //string query = "SELECT A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, C.APPROVAL_MESSAGE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_LIVE.HRM_EMP_RCI_APPROVALS C WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = C.APPROVAL_SHORT_NAME AND A.STORE_APR_ON IS NOT NULL AND A.EMP_CD IN (SELECT DISTINCT TT.EMP_CD FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW TT WHERE TT.EMP_HOD_CD = '" + EmployeeCode + "' OR (SELECT DISTINCT X.EMP_CD FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW X WHERE X.EMP_CD = '" + EmployeeCode + "' AND X.ROLL = 'SUPER') = '" + EmployeeCode + "') ORDER BY REQUEST_ID DESC";
                string query = "SELECT A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, C.APPROVAL_MESSAGE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_LIVE.HRM_EMP_RCI_APPROVALS C WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = C.APPROVAL_SHORT_NAME AND A.STORE_APR_ON IS NOT NULL AND ORDER BY REQUEST_ID DESC";
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
                //string query = "SELECT (A.REQUEST_ID || '-' || A.EMP_CD) REQUEST_EMP_ID, A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, A.HR_HOD_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = 'HRTA' AND A.STORE_APR_ON IS NULL AND A.EMP_CD IN (SELECT DISTINCT TT.EMP_CD FROM HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW TT WHERE TT.EMP_HOD_CD = '" + EmployeeCode + "' OR (SELECT DISTINCT X.EMP_CD FROM HRM_LIVE.HRM_EMP_RCI_RIGHTS_VIEW X WHERE X.EMP_CD = '" + EmployeeCode + "' AND X.ROLL = 'SUPER') = '" + EmployeeCode + "') ORDER BY REQUEST_ID DESC";
                
                //*rz comment 24-07-2023*//
                //string query = "SELECT (A.REQUEST_ID || '-' || A.EMP_CD) REQUEST_EMP_ID, A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, A.HR_HOD_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, (CASE WHEN A.WAIVEOFF = 'Y' THEN 'YES' ELSE 'NO' END) WAIVE_OFF FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = 'HRTA' AND A.STORE_APR_ON IS NULL ORDER BY REQUEST_ID DESC";
                
                //*rz new 24-07-2023*//
                string query = "SELECT (A.REQUEST_ID || '-' || A.EMP_CD) REQUEST_EMP_ID, A.REQUEST_ID, A.EMP_CD, B.EMPLOYEE_NAME, B.DESIGNATION_NAME, A.REMARKS, A.HOD_REMARKS, A.HR_TA_REMARKS, A.HR_HOD_REMARKS, TO_CHAR(A.L$IN_DATE) L$IN_DATE, TO_CHAR(A.LAST_DUTY_DATE) LAST_DUTY_DATE, (CASE WHEN A.WAIVEOFF = 'Y' THEN 'YES' ELSE 'NO' END) WAIVE_OFF, C.REG_CD FROM HRM_LIVE.HRM_EMP_RCI_REQUEST A, HRM_LIVE.HRM_EMPLOYEE_INFO_VIEW B, HRM_EMPLOYEE C WHERE 1 = 1 AND A.EMP_CD = B.EMP_CD AND A.REQUEST_STATUS = 'HRTA' AND A.STORE_APR_ON IS NULL AND C.EMP_CD = A.EMP_CD AND C.REG_CD IN (SELECT T.REG_CD FROM HRM_EMP_RCI_REGION_ALLOW_V T WHERE T.EMP_CD = '"+EmployeeCode+"') ORDER BY REQUEST_ID DESC";
                
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
                    if (gv_AssetDtl.Rows.Count > 0)
                    {
                        lbl_AssetMsg.Text = "Please collect the asset from the employee, then approve the request.";
                        lbl_Msg.InnerText = "You can't process this request due to the on-hand assets available on this employee in EBS.";
                    }
                    else 
                    {
                        string Query = "UPDATE HRM_LIVE.HRM_EMP_RCI_REQUEST SET STORE_REMARKS = '" + Remarks + "', STORE_APR_ON = SYSDATE, STORE_APR_BY = '" + EmployeeCode + "', REQUEST_STATUS = 'HRTA' WHERE REQUEST_ID = '" + ReqId + "' AND EMP_CD = '" + Employeecd + "'";
                        string Result = db.PostData(Query);
                        if (Result == "Done") 
                        {

                            string QueryLogs = "INSERT INTO HRM_LIVE.HRM_EMP_RCI_APPROVALS_LOGS(LOG_ID, REQUEST_NO, ITEM_DESCRIPTIONS, ITEM_STATUS, REMARKS, APPROVAL_DEP, L$USR_IN, L$IN_DATE) ";
                            QueryLogs += " VALUES ((SELECT NVL(MAX(LOG_ID),0)+1 FROM HRM_LIVE.HRM_EMP_RCI_APPROVALS_LOGS), '" + ReqId + "','Assest','', 'No Assest Avalible', 'Store', '" + EmployeeCode + "', sysdate)";
                            db.PostData(QueryLogs);

                            lbl_Msg.InnerText = "Approved Sucefully.";
                            txt_Remarks.Text = string.Empty;
                            unload();
                        }
                    }


                    //foreach (GridViewRow row in gv_AssetDtl.Rows)
                    //{
                    //    if (gv_AssetDtl.Rows.Count > 0)
                    //    {

                    //    }

                    //    if (gv_AssetDtl.Rows[row.RowIndex].Cells[1].Text != null)
                    //    {
                        
                    //    }
                    //}



                    //string Query = "UPDATE HRM_LIVE.HRM_EMP_RCI_REQUEST SET STORE_REMARKS = '" + Remarks + "', STORE_APR_ON = SYSDATE, STORE_APR_BY = '" + EmployeeCode + "', REQUEST_STATUS = 'HRTA' WHERE REQUEST_ID = '" + ReqId + "' AND EMP_CD = '" + Employeecd + "'";
                   // string Result = db.PostData(Query);
                    //if (Result == "Done")
                    //{
                        // Gate Pass Detail Update Start
                        //string GatePassNo = string.Empty;
                        //foreach (GridViewRow row in gv_GatepassDtl.Rows)
                        //{
                        //    CheckBox cb_Clear = (CheckBox)row.FindControl("cb_Clear");
                        //    if (cb_Clear.Checked)
                        //    {
                        //        GatePassNo = gv_GatepassDtl.Rows[row.RowIndex].Cells[1].Text;

                        //        string QueryCommunication = "UPDATE CENTRALIZED_GATEPASS_MST V SET V.STATUS='C', V.L$USR_UP = (SELECT A.USR_CD FROM SECURITY_LIVE.USERS A WHERE A.EMP_CD = '" + EmployeeCode + "'), V.L$UP_DATE = SYSDATE ";
                        //        QueryCommunication += " WHERE V.RES_PERSON_CD='" + Employeecd + "' AND  (V.CATEGORY_TYPE || '-' || V.GATEPASS_TYPE || '-' || V.GATEPASS_NO ||'-'||V.REG_CD ||' '||V.FISCAL_YEAR_ID) = '" + GatePassNo + "'";
                        //        string _Result = db.PostData(QueryCommunication);
                        //        if (_Result == "Done")
                        //        {

                        //        }
                        //    }
                        //}
                        // Communication Detail Update End


                        //lbl_Msg.InnerText = "Approved Sucefully.";
                      //  txt_Remarks.Text = string.Empty;
                       // unload();
                    //}
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