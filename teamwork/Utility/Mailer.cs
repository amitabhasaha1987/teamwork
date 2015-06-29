using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Configuration;
using System.Xml.Linq;
using System.IO;

namespace teamwork.Utility
{
    public static class Mailer
    {
        public static bool SendMail(string subject,string body, string toMail,string appDataPath)
        {
            try
            {
                
                string file = Path.Combine(appDataPath + "\\EmailCredential.xml");
                XElement ele = XElement.Load(file);
                
                WebMail.SmtpServer = ele.Element("smtp").Value;
                WebMail.SmtpPort = Convert.ToInt32(ele.Element("port").Value);
                WebMail.EnableSsl = true;
                WebMail.UserName = ele.Element("email").Value;
                WebMail.Password = ele.Element("passowrd").Value;
                WebMail.From = ele.Element("email").Value;
                WebMail.Send(to: toMail, subject: subject, body: body, isBodyHtml: true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        /*
        public static bool SendMail(string subject, string body, string toMail,IEnumerable<string> attachment)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "stylistics.test@gmail.com";
                WebMail.Password = "stylistics.test123";
                WebMail.From = "stylistics.test@gmail.com";
                WebMail.Send(to: toMail, subject: subject, body: body, isBodyHtml: true,filesToAttach: attachment);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }*/
    }
}