using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EmailServicen.Controllers
{
    public class HomeController : ApiController
    {
       

        public async void SendMailAsync(List<string> receivers)
        {
            string m = "hej";
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("vetechnorepley@gmail.com");
                foreach (var emailRecipient in receivers)
                {
                    mail.To.Add(emailRecipient);
                }
                //test email

                mail.Subject = "Server warning!";
                mail.Body = "Test";

                SmtpServer.Port = 587;
                
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
SmtpServer.Credentials = new System.Net.NetworkCredential("vetechnorepley@gmail.com", "Vetech2017");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);


            }
            catch (Exception ex)
            {
                var e = ex;
            }
        }

        //private string EmailMessage(List<Event> et)
        //{
        //string msg = "No Content";
        //string rlmsg = "";
        //foreach (var @event in et)
        //{
        //rlmsg += "Warning: " + @event.EventType.Name +
        //Environment.NewLine + "On date: " + @event.EventType.Created +
        //Environment.NewLine +
        //"On server with this name: " + @event.Server.ServerName +
        //Environment.NewLine + "_________________________" +
        //Environment.NewLine +
        //Environment.NewLine;
        //}
        //if (string.IsNullOrEmpty(rlmsg))
        //{
        //msg = rlmsg;
        //}
        //return msg;
        //}
    }
}
