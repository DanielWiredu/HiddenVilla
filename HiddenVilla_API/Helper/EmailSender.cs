using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Api.Helper
{
    public class EmailSender : IEmailSender
    {
        private readonly MailJetSettings _mailJetSettings;

        public EmailSender(IOptions<MailJetSettings> mailjetSettings)
        {
            _mailJetSettings = mailjetSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient(_mailJetSettings.PublicKey,
                _mailJetSettings.PrivateKey)
            {
                Version = ApiVersion.V3,
            }; 

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, _mailJetSettings.Email)
             .Property(Send.FromName, "Your Mailjet Pilot")
             .Property(Send.Recipients, new JArray {
                 new JObject {
                     {"Email", email},
                     {"Name", "Heya"}
                 }
             })
             .Property(Send.Subject, subject)
             .Property(Send.TextPart, htmlMessage)
             .Property(Send.HtmlPart, "<h3>Dear passenger, welcome to Mailjet!</h3><br />May the delivery force be with you!");
            //.Property(Send.Messages, new JArray {
            // new JObject {
            //  {"From", new JObject {
            //   {"Email", _mailJetSettings.Email},
            //   {"Name", "Mailjet Pilot"}
            //   }},
            //  {"To", new JArray {
            //   new JObject {
            //    {"Email", email},
            //    {"Name", "Hello"}
            //    }
            //   }},
            //  {"Subject", subject},
            //  {"HTMLPart", htmlMessage}
            //  }
            //    });
            MailjetResponse response = await client.PostAsync(request);

        }


    }
}
