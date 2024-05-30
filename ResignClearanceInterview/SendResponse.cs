using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ResignClearanceInterview
{
    public class SendResponse
    {
        DBHelper hp = new DBHelper();
        public void SendEmail(string ToID, string mBody)
        {
            try
            {
                //string From = "mis@alkaram.com";
                string To = ToID;
                string Subject = "Online Resignation Approval";
                //string Body = "Dear " + Supp_Name.ToString() + ", \n\nHope this email finds you in good health! \n\n Alkaram Textiles Mills (Pvt.) Ltd. has a strong mechanism of Supplier on-boarding for which you must have received an Online Registration Form from the below mentioned link:\n\n" + mBody.ToString() + "\n\n Kindly fill out the complete Form and revert back to us to get you on-board for successful future business. \n\n*This is an Alkaram Textile Mills (Pvt.) Ltd. auto-generated email, please DO NOT REPLY. If you have any further queries, please feel free to reach us anytime.*";
                string Body = mBody;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("mis@alkaram.com");
                message.To.Add(new MailAddress(To));
                message.Subject = Subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = Body;

                string queryForCC = @"
                SELECT A.DETAIL_NAME CC FROM HRM_SETUP_DETL A
                WHERE 1=1
                AND A.SEQ_NO = 154
                AND TRIM(UPPER(A.VALUE1)) = TRIM(UPPER('CC'))
                AND TRIM(UPPER(A.VALUE2)) = TRIM(UPPER('Y'))
                AND TRIM(UPPER(A.STATUS)) = TRIM(UPPER('Y'))";
                DataTable CCdt = hp.GetDataFromDatabaseOraCus(queryForCC);
                if (CCdt.Rows.Count > 0)
                {
                    foreach (DataRow item in CCdt.Rows)
                    {
                        message.CC.Add(item["CC"].ToString().Trim());
                    }

                    // Dispose of the DataTable after it's no longer needed
                    CCdt.Dispose();

                    smtp.Port = 465;
                    smtp.Host = "172.16.0.20";
                    //smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential("mis@alkaram.com", "B3h1ndth3m1rror##");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                }
                //else
                //{

                //}
                //string cc = "muhammad.faizan@alkaram.com,rabnawaz.chohan@alkaram.com";
                //string[] CCId = cc.Split(',');

                //foreach (string CCEmail in CCId)
                //{
                //    message.CC.Add(new MailAddress(CCEmail));
                //}
              
            }
            catch (Exception ex) { }
        }


        public void SendEmailDomainEmail(string ToID, string mBody)
        {
            try
            {
                //string From = "mis@alkaram.com";
                string To = ToID;
                string Subject = "Online Resignation Approval";
                //string Body = "Dear " + Supp_Name.ToString() + ", \n\nHope this email finds you in good health! \n\n Alkaram Textiles Mills (Pvt.) Ltd. has a strong mechanism of Supplier on-boarding for which you must have received an Online Registration Form from the below mentioned link:\n\n" + mBody.ToString() + "\n\n Kindly fill out the complete Form and revert back to us to get you on-board for successful future business. \n\n*This is an Alkaram Textile Mills (Pvt.) Ltd. auto-generated email, please DO NOT REPLY. If you have any further queries, please feel free to reach us anytime.*";
                string Body = mBody;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("it.operation@alkaram.com");
                message.To.Add(new MailAddress(To));
                message.Subject = Subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = Body;

                //string cc = "rabnawaz.chohan@alkaram.com,muhammad.faizan@alkaram.com";
                //string[] CCId = cc.Split(',');

                //foreach (string CCEmail in CCId)
                //{
                //    message.CC.Add(new MailAddress(CCEmail));
                //}
                string queryForCC = @"
                SELECT A.DETAIL_NAME CC FROM HRM_SETUP_DETL A
                WHERE 1=1
                AND A.SEQ_NO = 154
                AND TRIM(UPPER(A.VALUE1)) = TRIM(UPPER('CC'))
                AND TRIM(UPPER(A.VALUE2)) = TRIM(UPPER('Y'))
                AND TRIM(UPPER(A.STATUS)) = TRIM(UPPER('Y'))";
                DataTable CCdt = hp.GetDataFromDatabaseOraCus(queryForCC);
                if (CCdt.Rows.Count > 0)
                {
                    foreach (DataRow item in CCdt.Rows)
                    {
                        message.CC.Add(item["CC"].ToString().Trim());
                    }
                    // Dispose of the DataTable after it's no longer needed
                    CCdt.Dispose();
                    smtp.Port = 465;
                    smtp.Host = "172.16.0.20";
                    //smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential("it.operation@alkaram.com", "BHo,q8!");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                }
                //smtp.Port = 465;
                //smtp.Host = "172.16.0.20";
                ////smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = true;
                //smtp.Credentials = new NetworkCredential("it.operation@alkaram.com", "BHo,q8!");
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Send(message);
            }
            catch (Exception ex) { }
        }

        public void SendEmailJazz(string ToID, string Sub, string mBody)
        {
            try
            {
                //string From = "mis@alkaram.com";
                string To = ToID;
                string Subject = Sub;  //"Online Resignation Approval";
                //string Body = "Dear " + Supp_Name.ToString() + ", \n\nHope this email finds you in good health! \n\n Alkaram Textiles Mills (Pvt.) Ltd. has a strong mechanism of Supplier on-boarding for which you must have received an Online Registration Form from the below mentioned link:\n\n" + mBody.ToString() + "\n\n Kindly fill out the complete Form and revert back to us to get you on-board for successful future business. \n\n*This is an Alkaram Textile Mills (Pvt.) Ltd. auto-generated email, please DO NOT REPLY. If you have any further queries, please feel free to reach us anytime.*";
                string Body = mBody;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("it.operation@alkaram.com");
                message.To.Add(new MailAddress(To));
                message.Subject = Subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = Body;

                //string cc = "rabnawaz.chohan@alkaram.com,jawwad.bakhtiar@alkaram.com,it.operation@alkaram.com,owais.qadri@alkaram.com,sajjad.ahmed@jazz.com.pk";
                //string[] CCId = cc.Split(',');

                //foreach (string CCEmail in CCId)
                //{
                //    message.CC.Add(new MailAddress(CCEmail));
                //}
                string queryForJazz = @"
                SELECT A.DETAIL_NAME CCJAZZ FROM HRM_SETUP_DETL A
                WHERE 1=1
                AND A.SEQ_NO = 154
                AND TRIM(UPPER(A.VALUE1)) = TRIM(UPPER('EMAILJAZZ'))
                AND TRIM(UPPER(A.VALUE2)) = TRIM(UPPER('Y'))
                AND TRIM(UPPER(A.STATUS)) = TRIM(UPPER('Y'))";

                DataTable Jazzdt = hp.GetDataFromDatabaseOraCus(queryForJazz);
                if (Jazzdt.Rows.Count > 0)
                {
                    foreach (DataRow item in Jazzdt.Rows)
                    {
                        message.CC.Add(item["CCJAZZ"].ToString().Trim());
                    }
                    // Dispose of the DataTable after it's no longer needed
                    Jazzdt.Dispose();
                    smtp.Port = 465;
                    smtp.Host = "172.16.0.20";
                    //smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential("it.operation@alkaram.com", "BHo,q8!");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                }
                //smtp.Port = 465;
                //smtp.Host = "172.16.0.20";
                ////smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = true;
                //smtp.Credentials = new NetworkCredential("it.operation@alkaram.com", "BHo,q8!");
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Send(message);
            }
            catch (Exception ex) { }
        }


        //######################################### SMS #######################

        string mUrl_Send = string.Empty;

        public void SendSmS(string ToMobileNo, string ToMessage)
        {
            try
            {
                DateTime dtNow = DateTime.Now;
                string _ToMobileNo = this.cellnoFormating(ToMobileNo);
                ToMessage += dtNow .ToString() + "\n";

                ToMessage = ToMessage.Replace(" ", "%20");
                ToMessage = ToMessage.Replace("<", "%3C");
                ToMessage = ToMessage.Replace(">", "%3E");
                ToMessage = ToMessage.Replace("#", "%23");
                //ToMessage = ToMessage.Replace("%", "%25");
                ToMessage = ToMessage.Replace("{", "%7B");
                ToMessage = ToMessage.Replace("}", "%7D");
                ToMessage = ToMessage.Replace("|", "%7C");
                ToMessage = ToMessage.Replace("\\", "%5C");
                ToMessage = ToMessage.Replace("^", "%5E");
                ToMessage = ToMessage.Replace("~", "%7E");
                ToMessage = ToMessage.Replace("[", "%5B");
                ToMessage = ToMessage.Replace("]", "%5D");
                ToMessage = ToMessage.Replace("`", "%60");
                ToMessage = ToMessage.Replace(";", "%3B");
                ToMessage = ToMessage.Replace("/", "%2F");
                ToMessage = ToMessage.Replace("?", "%3F");
                ToMessage = ToMessage.Replace(":", "%3A");
                ToMessage = ToMessage.Replace("@", "%40");
                ToMessage = ToMessage.Replace("&", "%26");
                ToMessage = ToMessage.Replace("$", "%24");



                //mUrl_Send = "http://sms.myvfirst.com.pk/smpp/sendsms?username=alkaramhttp&password= http12345&to=" + number + "&from=Alkaram&text=" + ToMessage + "";
                mUrl_Send = "http://sms.myvfirst.com.pk/smpp/sendsms?username=alkaramhttpsc&password=$tRo9g8091&to=" + _ToMobileNo + "&from=8091&text=" + ToMessage + "";


                WebRequest request_send = HttpWebRequest.Create(mUrl_Send);
                WebResponse response_send = request_send.GetResponse();
                Stream dataStream = response_send.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                response_send.Close();
                dataStream.Close();
                reader.Close();

                if (responseFromServer.Contains("Sent."))
                {
                    // Logs creation when system send result
                    //logManager.postResponse(MobileNo, ToMessage);
                }
            }
            catch (Exception ex)
            {

            }


        }


        public string cellnoFormating(string EmployeeContactNo)
        {


            string ResultEmployeeContactNo = string.Empty;

            string nine2, zero3, first2, last9, number, result = string.Empty;
            int inputLen;

            nine2 = "92";
            zero3 = "03";

            inputLen = EmployeeContactNo.Length;
            number = EmployeeContactNo;

            if (inputLen == 12 || inputLen == 11)
            {
                first2 = number.Substring(0, 2);
                last9 = number.Substring(2, 9);

                if (first2 == nine2)
                {
                    first2 = number.Substring(0, 2);
                    last9 = number.Substring(2, 10);
                }
                else if (first2 == zero3)
                {
                    first2 = nine2;
                    last9 = number.Substring(1, 10);
                }
                else
                { }
                ResultEmployeeContactNo = first2 + last9;
            }
            else
            {
            }

            return ResultEmployeeContactNo;
        }




    }
}