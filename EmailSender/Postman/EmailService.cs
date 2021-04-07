using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Postman.Interfaces;

namespace Postman
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        private string sentStatus = "The letter has not been sent yet.";
        public string SentStatus
        {
            get {
                return sentStatus;
            }
        }
        public EmailService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public string SendEmail(Message message)
        {
            var emailMessage = CreateMessage(message);
            Send(emailMessage);
            return SentStatus;
        }       
        private MimeMessage CreateMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Text
            };
            return emailMessage;           
        }
        private void Send(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                client.MessageSent += Client_MessageSent;
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                try
                {
                    client.Connect(_emailConfig.SmptServer, _emailConfig.Port, true);                    
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(emailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            };
        }
        private void Client_MessageSent(object sender, MailKit.MessageSentEventArgs e)
        {
            sentStatus = e.Response.ToString();            
        }

        public async Task<string> SendEmailAsync(Message message)
        {
            var emailMessage = CreateMessage(message);
            await SendAsync(emailMessage);
            return SentStatus;
        }

        private async Task SendAsync(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                client.MessageSent += Client_MessageSent;
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                try
                {
                    await client.ConnectAsync(_emailConfig.SmptServer, _emailConfig.Port, true);
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(emailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            };
        }
    }
}
