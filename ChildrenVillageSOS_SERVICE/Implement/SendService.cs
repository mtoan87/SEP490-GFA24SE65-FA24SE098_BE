using ChildrenVillageSOS_DAL.DTO.SendEmail;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_SERVICE.Interface;
using MailKit.Net.Smtp;
using Microsoft.Identity.Client;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class SendService : ISendService
    {
        private readonly AppConfiguration _emailConfig;
        public SendService(AppConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.EmailConfiguration.From, _emailConfig.EmailConfiguration.From));

            foreach (var recipient in message.Recipients)
            {
                emailMessage.To.Add(new MailboxAddress(recipient, recipient));
            }

            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart("html")
            {
                Text = message.Content 
            };

            return emailMessage;
        }
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    await smtpClient.ConnectAsync(_emailConfig.EmailConfiguration.SmtpServer,
                                                  _emailConfig.EmailConfiguration.Port,
                                                  true); 

                    await smtpClient.AuthenticateAsync(_emailConfig.EmailConfiguration.UserName,
                                                        _emailConfig.EmailConfiguration.Password);

                    await smtpClient.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi gửi email: {ex.Message}");
                    throw; 
                }
                finally
                {
                    await smtpClient.DisconnectAsync(true);
                }
            }
        }
    }
}
