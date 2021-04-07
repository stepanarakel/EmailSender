using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Postman.Interfaces;
using EmailSender.Models;
using EmailSender.Repository;
using Postman;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EmailSender.Controllers
{
    [ApiController]
    [Route("v1/api/emails")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public EmailController(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }
        
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            MailingRepository db = new MailingRepository(_configuration);
            var mailing = await db.FindAllAsync();
            return Ok(mailing);
        }

        [HttpPost]
        public async Task Post([FromBody]object RB)
        {
            var requestBody = JsonConvert.DeserializeObject<RequestBody>(RB.ToString());

            var recipients = new List<string>();
            recipients.Add(requestBody.recipient);
            recipients.AddRange(requestBody.carbon_copy_recipients);

            var message = new Message(recipients, requestBody.subject, requestBody.text);
            string status = await _emailService.SendEmailAsync(message);

            var item = new Mailing();
            item.Recipient = requestBody.recipient;
            item.Subject = requestBody.subject;
            item.Message_text = requestBody.text;
            item.CarbonCopyRecipients = new List<string>();
            item.CarbonCopyRecipients.AddRange(requestBody.carbon_copy_recipients);
            item.Status = status;

            MailingRepository db = new MailingRepository(_configuration);
            await db.AddAsync(item);
        }
    }
}
