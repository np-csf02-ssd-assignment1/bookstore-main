using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebFrontend.Services.EmailSender.MSGraph
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddMSGraphEmailSender(this IServiceCollection services, IConfiguration configure)
        {
            services
                .AddOptions<AuthMessageSenderOptions>()
                .Bind(configure)
                .ValidateDataAnnotations();

            return services.AddTransient<IEmailSender, EmailSender>();
        }
    }
}
