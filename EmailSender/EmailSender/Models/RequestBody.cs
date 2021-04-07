using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSender.Models
{
    public class RequestBody
    {
        public string recipient;
        public string subject;
        public string text;
        public IEnumerable<string> carbon_copy_recipients;
    }
}
