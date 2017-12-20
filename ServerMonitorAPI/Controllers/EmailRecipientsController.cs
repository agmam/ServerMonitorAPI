using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL.DB;
using DAL.Repositories;
using Entities.Entities;

namespace ServerMonitorAPI.Controllers
{
    [Authorize]
    public class EmailRecipientsController : ApiController
    {
        private IRepository<EmailRecipient> emailRepository = 
            new DAL.DALFacade().GetEmailRecipientRepository();

        // GET: api/EmailRecipients
        public List<EmailRecipient> GetEmailRecipients()
        {
            return emailRepository.ReadAll();
        }

        // GET: api/EmailRecipients/5
        [ResponseType(typeof(EmailRecipient))]
        public IHttpActionResult GetEmailRecipient(int id)
        {
            EmailRecipient emailRecipient = emailRepository.Read(id);
            if (emailRecipient == null)
            {
                return NotFound();
            }

            return Ok(emailRecipient);
        }

        // PUT: api/EmailRecipients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmailRecipient(int id, EmailRecipient emailRecipient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emailRecipient.Id)
            {
                return BadRequest();
            }
            emailRepository.Update(emailRecipient);


            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmailRecipients
        [ResponseType(typeof(EmailRecipient))]
        public IHttpActionResult PostEmailRecipient(EmailRecipient emailRecipient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            emailRepository.Create(emailRecipient);
            return CreatedAtRoute("DefaultApi", new { id = emailRecipient.Id }, emailRecipient);
        }

        // DELETE: api/EmailRecipients/5
        [ResponseType(typeof(EmailRecipient))]
        public IHttpActionResult DeleteEmailRecipient(int id)
        {
            EmailRecipient emailRecipient = emailRepository.Read(id);
            if (emailRecipient == null)
            {
                return NotFound();
            }

            emailRepository.Delete(id);
            return Ok(emailRecipient);
        }

        public List<EmailRecipient> GetEmailRecipients(int id)
        {
            var a = emailRepository.ReadAllFromServer(id);

            return a;
        }
    }
}