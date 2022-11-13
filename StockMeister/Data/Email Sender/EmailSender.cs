using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMeister.Data
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient("749d0eacdbd4b1e6612e9a6405d83f0a", "3ed5e49056d8dc38f2d196ea695a814c")
            {
                
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "stockmeister11@gmail.com")
            .Property(Send.FromName, "Stock Meister")
            .Property(Send.Subject, subject)
            .Property(Send.HtmlPart, htmlMessage)
            .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", email}
                 }
                });
            MailjetResponse response = await client.PostAsync(request);
        }
    }
}
