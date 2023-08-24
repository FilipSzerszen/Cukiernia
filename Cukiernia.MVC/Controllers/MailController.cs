using Cukiernia.MVC.Extensions;
using Cukiernia.MVC.Models;
using FluentValidation.Validators;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;
using System.Text;
using System.Text.Unicode;

namespace Cukiernia.MVC.Controllers
{
    public class MailController : Controller
    {

        [HttpGet]
        [Route("Mail/Send/{encodedName}")]
        public IActionResult Send(string encodedName)
        {
            emailDto emailDto = new emailDto()
            {
                fromAddress = "slodkagruszkamail@gmail.com",
                subject = encodedName.Replace("-", " "),
                toEmail = "",
                body = ""
            };
            return PartialView("Send", emailDto);
        }

        [HttpPost]
        [Route("Mail/Send/{encodedName}")]
        public IActionResult Send(string encodedName, string fromAddress, string toEmail, string subject, string body)
        {
            SendMail(fromAddress, toEmail, subject, body);
            this.SetNotification("success", $"Pomyślnie wysłano zapytanie");
            return RedirectToAction("Details", "Baking", new { encodedName = encodedName });
        }




        public string SendMail(string fromAddress, string toEmail, string subject, string body)
        {

            var mailConfig = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();

            //Construct the message    (fromAddress, toEmail, subject, body)
            var mailMessage = new System.Net.Mail.MailMessage(fromAddress, fromAddress, subject, "");
            mailMessage.ReplyToList.Add(new MailAddress(toEmail));

            //Specify whether the body is HTML
            mailMessage.IsBodyHtml = true;

            //Convert to MimeMessage
            MimeMessage message = MimeMessage.CreateFromMailMessage(mailMessage);
            var rawMessage = message.ToString();

            rawMessage += body;

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

            var mail = new Message
            {
                Raw = Base64UrlEncode(rawMessage)
            };

            var result = gmail.Users.Messages.Send(mail, "me").Execute();
            return result.Id;
        }

        private static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            // Special "url-safe" base64 encode.
            return Convert.ToBase64String(inputBytes).Replace('+', '-').Replace('/', '_').Replace("=", "");
        }


    }
}
