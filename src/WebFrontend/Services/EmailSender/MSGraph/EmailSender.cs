using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebFrontend.Services.EmailSender.MSGraph
{
    public class EmailSender : IEmailSender
    {
        public AuthMessageSenderOptions Options { get; }

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await Execute(Options, subject, message, email);
        }

        public async Task Execute(AuthMessageSenderOptions authOptions, string subject, string message, string email)
        {
            Console.WriteLine(JsonSerializer.Serialize(authOptions));

            var confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(authOptions.ClientID)
                .WithTenantId(authOptions.TenantID)
                .WithClientSecret(authOptions.ClientSecret)
                .Build();

            var authProvider = new ClientCredentialProvider(confidentialClientApplication);

            var graphClient = new GraphServiceClient(authProvider);

            var messageClass = new Message
            {
                Subject = subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = message
                },
                Sender = new Recipient
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = authOptions.Sender
                    }
                },
                From = new Recipient
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = authOptions.From
                    }
                },
                ToRecipients = new Recipient[]
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = email
                        }
                    }
                }
            };

            await graphClient
                .Users[authOptions.Sender]
                .SendMail(messageClass)
                .Request()
                .PostAsync();
        }
    }
}
