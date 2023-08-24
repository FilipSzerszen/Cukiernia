using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System.Net.Mail;

namespace Cukiernia.MVC.Services
{
    public class EmailSender : IEmailSender
    {
        Task IEmailSender.SendEmailAsync(string toEmail, string subject, string body)
        {

            var mailConfig = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
            var fromAddress = mailConfig.GetValue<string>("MailSettings:Mail");

            //Construct the message
            var mailMessage = new System.Net.Mail.MailMessage(fromAddress, toEmail, subject, body);
            mailMessage.ReplyToList.Add(new MailAddress(fromAddress));

            //Specify whether the body is HTML
            mailMessage.IsBodyHtml = true;

            //Convert to MimeMessage
            MimeMessage message = MimeMessage.CreateFromMailMessage(mailMessage);
            var rawMessage = message.ToString();

            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = mailConfig.GetValue<string>("MailSettings:ClientId"),
                    ClientSecret = mailConfig.GetValue<string>("MailSettings:ClientSecret")
                },
                Scopes = new[] { GmailService.Scope.GmailCompose }
            });

            var token = new Google.Apis.Auth.OAuth2.Responses.TokenResponse()
            {
                AccessToken = mailConfig.GetValue<string>("MailSettings:AccessToken"),
                RefreshToken = mailConfig.GetValue<string>("MailSettings:RefreshToken")
            };

            //In my case the username is the same as the fromAddress
            var gmail = new GmailService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                ApplicationName = "App Name",
                HttpClientInitializer = new UserCredential(flow, fromAddress, token)
            });

            var result = gmail.Users.Messages.Send(new Message
            {
                Raw = Base64UrlEncode(rawMessage)
            }, "me").Execute();

            return Task.CompletedTask;
        }

        private static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes).Replace('+', '-').Replace('/', '_').Replace("=", "");
        }


    }
}

