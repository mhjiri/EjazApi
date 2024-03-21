using Application.Emails.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Emails
{
    public class EmailNotifier : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<EmailSettings> _options;
        public EmailNotifier(IConfiguration configuration, IOptions<EmailSettings> options)
        {
            _configuration = configuration;
            _options = options;
        }
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            string fromEmail = _options.Value.SenderEmail;
            string fromName = _options.Value.SenderName;
            string apiKey = _options.Value.ApiKey;
            var sendGridClient = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(toEmail);

            var plainTextContent = Regex.Replace(htmlMessage, "<[^>]*>", "");
            var msg = MailHelper.CreateSingleEmail(from, to, subject,
            plainTextContent, htmlMessage);
            var response = await sendGridClient.SendEmailAsync(msg);
            var status = response.StatusCode;
            if (status == System.Net.HttpStatusCode.Accepted) return true;
            return false;
        }
    }
}
