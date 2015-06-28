using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace teamwork.Utility
{
    public static class Mailer
    {
        static string SmtpServer { get; set; }
        static int SmtpPort { get; set; }
        static string username { get; set; }
        static string password { get; set; }
        static Mailer()
        {
            SmtpServer = "smtp.gmail.com";
            SmtpPort = 587;
            username = "stylistics.test@gmail.com";
            password = "stylistics.test123";
        }

        public static bool SendMail(string subject,string body, string toMail)
        {
            try
            {
                WebMail.SmtpServer = SmtpServer;
                WebMail.SmtpPort = SmtpPort;
                WebMail.EnableSsl = true;
                WebMail.UserName = username;
                WebMail.Password = password;
                WebMail.From = username;

                WebMail.Send(to: toMail, subject: subject, body: body, isBodyHtml: true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public static bool SendMail(string subject, string body, string toMail,IEnumerable<string> attachment)
        {
            try
            {
                WebMail.SmtpServer = SmtpServer;
                WebMail.SmtpPort = SmtpPort;
                WebMail.EnableSsl = true;
                WebMail.UserName = username;
                WebMail.Password = password;
                WebMail.From = username;

                WebMail.Send(to: toMail, subject: subject, body: body, isBodyHtml: true,filesToAttach: attachment);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}