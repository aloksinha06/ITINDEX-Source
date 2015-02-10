using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ITINDEXAU.Email
{
    public class MyMail
    {
        public static string ServerHostName()
        {
            string port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (port == null)
            {
                port = "";
            }
            else
            {
                port = ":" + port;
            }

            string protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (protocol == null || protocol == "0")
            {
                protocol = "http://";
            }
            else
            {
                protocol = "https://";
            }

            return protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port;
        }

        public static string SendMail(string To,string Subject,string MessageBody)
        {
            try
            {
               MailMessage mail = new MailMessage();
                  mail.To.Add(To);
                 
                  mail.From = new MailAddress("webdirectory34@gmail.com");
                  mail.Subject = Subject;//"Web Directory Email Verification!!!";

                  string Body = MessageBody;
                  mail.Body = Body;
                  
                  mail.IsBodyHtml = true;
                  SmtpClient smtp = new SmtpClient();
                  smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                  smtp.Credentials = new System.Net.NetworkCredential
                       ("webdirectory34@gmail.com", "web@2013");
                //Or your Smtp Email ID and Password
                  smtp.EnableSsl = true;
                  
                  
                  smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                  smtp.Send(mail);
            
                return "sent";

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

    }
}