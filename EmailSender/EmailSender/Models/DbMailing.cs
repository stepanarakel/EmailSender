using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSender.Models
{
    public class DbMailing
    {
        public string TABLE_NAME = "mailing";
        public string COLUMN_Recipient = "recipient";
        public string COLUMN_Subject = "subject";
        public string COLUMN_Message_text = "message_text";
        public string COLUMN_Carbon_copy_recipients = "carbon_copy_recipients";
        public string COLUMN_Departure_date = "departure_date";        
        public string COLUMN_Status = "status";
    }
}
