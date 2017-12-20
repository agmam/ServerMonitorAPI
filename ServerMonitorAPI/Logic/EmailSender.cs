using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Http.Results;
using DAL;
using DAL.Repositories;
using Entities.Entities;

namespace ServerMonitorAPI.Logic
{
    public class EmailSender
    {
        private readonly IRepository<EmailRecipient> emailRepo = new DALFacade().GetEmailRecipientRepository();
        public void SendEmail(List<Event> et)
        {
            return;
            try
            {
                //MailMessage mail = new MailMessage();
                //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                //mail.From = new MailAddress("vetechnorepley@gmail.com");
                //List<EmailRecipient> er = emailRepo.ReadAll();
                //foreach (var emailRecipient in er)
                //{
                //    mail.To.Add(emailRecipient.Email);
                //}
                ////test email
                //mail.To.Add("vetechnorepley@gmail.com");
                //mail.Subject = "Server warning!";
                //mail.Body = EmailMessage(et);

                //SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("vetechnorepley@gmail.com", "Vetech2017");
                //SmtpServer.EnableSsl = true;

                //SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                var e = ex;
                return;
            }
        }

        private string EmailMessage(List<Event> et)
        {
            string msg = "No Content";
            string rlmsg = "";
            foreach (var @event in et)
            {
                rlmsg += "Warning: " + @event.EventType.Name + 
                    Environment.NewLine + "On date: " + @event.EventType.Created + 
                    Environment.NewLine +
                    "On server with this name: " + @event.Server.ServerName + 
                    Environment.NewLine + "_________________________" + 
                    Environment.NewLine + 
                    Environment.NewLine;
            }
            if (string.IsNullOrEmpty(rlmsg))
            {
                msg = rlmsg;
            }
            return msg;
        }

    }
}