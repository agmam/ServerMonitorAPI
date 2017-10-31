using System.Collections.Generic;
using DAL.Repositories;
using Entities.Entities;

namespace DAL
{
    internal class EmailRecipientRepository : IRepository<EmailRecipient>
    {
        public EmailRecipient Create(EmailRecipient t)
        {
            throw new System.NotImplementedException();
        }

        public EmailRecipient Read(int i)
        {
            throw new System.NotImplementedException();
        }

        public List<EmailRecipient> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public List<EmailRecipient> ReadAllFromServer(int serverId)
        {
            throw new System.NotImplementedException();
        }

        public EmailRecipient Update(EmailRecipient t)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}