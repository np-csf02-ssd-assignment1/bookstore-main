using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebFrontend.Services.EmailSender.SendGrid
{
    public static class ServicesConfiguration
    {
        public static void AddSendGridEmailSender(this IServiceCollection services, AuthMessageSenderOptions options)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddOptions<AuthMessageSenderOptions>();
        }
    }
}
