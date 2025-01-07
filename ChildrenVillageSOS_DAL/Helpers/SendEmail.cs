using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_DAL.Helpers
{
        public static class SendEmail
        {
            public static async Task<bool> SendThankYouEmail(string toEmail)
            {
                if (!IsValidEmail(toEmail))
                {
                    return false;
                }

                var userName = "ChildrenVillageSOS";  
                var emailFrom = "toancx202@gmail.com";  
                var password = "taio jkdc phja ptkq";  

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(userName, emailFrom));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = "Thank you for supporting ChildrenVillageSOS.";

                message.Body = new TextPart("html")
                {
                    Text =
                    @"
                <html>
                    <head>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f8f9fa;
                                padding: 20px;
                            }
                            .container {
                                background-color: #ffffff;
                                padding: 30px;
                                border-radius: 8px;
                                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                            }
                            .header {
                                font-size: 24px;
                                color: #007bff;
                                margin-bottom: 20px;
                            }
                            .content {
                                font-size: 16px;
                                color: #333;
                            }
                            .footer {
                                margin-top: 30px;
                                font-size: 14px;
                                color: #777;
                            }
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>Thank you, " + toEmail + @"!</div>
                            <div class='content'>
                                <p>We would like to sincerely thank you for supporting the ChildrenVillageSOS system.</p>
                                <p>Thanks to your help, we can continue to provide support and care for children in need.</p>
                                <p>We deeply appreciate your contribution and hope that you will continue to join us in the future.</p>
                            </div>
                            <div class='footer'>
                                <p>Best regards,</p>
                                <p>The ChildrenVillageSOS System</p>
                            </div>
                        </div>
                    </body>
                </html>
                "
                };

                using var client = new SmtpClient();
                try
                {
                    // Kết nối tới máy chủ SMTP của Gmail
                    await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(emailFrom, password);

                    // Gửi email
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error sending email: " + ex.Message);
                    return false;
                }
            }

            private static bool IsValidEmail(string email)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }
        }
}

