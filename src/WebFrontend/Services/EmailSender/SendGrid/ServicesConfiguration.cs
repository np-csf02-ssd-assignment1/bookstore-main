using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebFrontend.Services.EmailSender.SendGrid
{
    public static class ServicesConfiguration
    {
        public static void AddSendGridEmailSender(this IServiceCollection services, IConfiguration configure)
        {
            services
                .AddOptions<AuthMessageSenderOptions>()
                .Bind(configure)
                .ValidateDataAnnotations();

            services.AddTransient<IEmailSender, EmailSender>();
        }
    }
}
