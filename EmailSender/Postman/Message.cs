using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace Postman
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public Message(IEnumerable<string> to, string subject, string text)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Text = text;
        }
    }
}
