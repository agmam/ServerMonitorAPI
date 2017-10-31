using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class EmailRecipient
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Email { get; set; }
    }
}
