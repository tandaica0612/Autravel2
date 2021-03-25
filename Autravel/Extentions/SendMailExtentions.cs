using Autravel.Controllers.Huy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Autravel.Extentions
{
    public class SendMailExtentions
    {
        public  static bool SendEmail(string[] _email, string Subject, string body,string[] _cc = null)
        {
            //  return false;
            var account = SqlModule.GetDataTable(" SELECT TOP 1 * FROM  dbo.SetupMailSMTP   order by NEWID()").Rows[0];
            MailMessage mail = new MailMessage();

            try
            {
                foreach (var item in _email)
                {
                    try
                    {
                        if (item.Contains("@"))
                        {
                            mail.To.Add(item);
                        }
                    }
                    catch
                    {
                    }

                }
                if (mail.To.Count==0)
                {
                    return false;
                }
 
                 if (_cc != null)
                {
                    foreach (var item in _cc)
                    {
                        mail.CC.Add(item);
                    }
                }
                
                mail.From = new MailAddress(account["SetupMailSMTP_Email"].ToString());
                mail.Subject = Subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = account["SetupMailSMTP_Host"].ToString(); //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential(account["SetupMailSMTP_Email"].ToString(), account["SetupMailSMTP_Password"].ToString());
                smtp.Port = int.Parse(account["SetupMailSMTP_Port"].ToString());
                smtp.EnableSsl = (account["SetupMailSMTP_SSL"].ToString()=="True" ? true : false);
              
                 smtp.Send(mail);
 

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
    }

}