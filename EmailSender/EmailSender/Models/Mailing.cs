using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailSender.Models
{
    public class Mailing : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Recipient { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message_text { get; set; }       
        public List<string> CarbonCopyRecipients { get; set; }
        public string Departure_date { get; set; }
        public string Status { get; set; }
    }
}
