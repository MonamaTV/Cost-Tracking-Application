using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
namespace School_Website
{
    public class SendEmail
    {
        public string ClientEmail { get; set; }
        public string ClientSubject { get; set; }
        public string ClientQuery { get; set; }

        public SendEmail(string clientEmail, string clientSubject, string clientQuery)
        {

            this.ClientEmail = clientEmail;
            this.ClientSubject = clientSubject;
            this.ClientQuery = clientQuery;
        }
        public void sendToClient()
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                };
                client.Credentials = new NetworkCredential("The organization email", "password");
                MailMessage message = new MailMessage();
                message.To.Add(ClientEmail);
                message.From = new MailAddress("The organization email");
                message.Subject = ClientSubject;
                message.Body += $"{ClientQuery}";
                client.Send(message);
                client.Dispose();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
    }
}