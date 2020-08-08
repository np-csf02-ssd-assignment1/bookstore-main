using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebFrontend.Data;
using WebFrontend.Services.EmailSender.SendGrid;
using WebFrontend.Services.EmailSender.MSGraph;

namespace WebFrontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
                Create a "Basic Logger".
                Since the main logger is not initialised before
                ConfigureServices() is called, this is to enable
                use of a temporary logger.
            */
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var logger = loggerFactory.CreateLogger("WebFrontend.Startup (Basic Logger)");

            services.AddControllersWithViews();
            services.AddRazorPages();

            var emailSenderDisabled = Configuration.GetValue<bool>("EmailSender:Disabled");
            var msGraphEmailSenderOptions = Configuration.GetSection("EmailSender:MSGraph");
            var sendGridEmailSenderOptions = Configuration.GetSection("EmailSender:SendGrid");

            if (emailSenderDisabled)
            {
                logger.LogWarning("EmailSender explicitly disabled.");
            }
            else if (msGraphEmailSenderOptions.Get<WebFrontend.Services.EmailSender.MSGraph.AuthMessageSenderOptions>() != null)
            {
                services.AddMSGraphEmailSender(msGraphEmailSenderOptions);
                logger.LogInformation("MS Graph configured.");
            }
            else if (sendGridEmailSenderOptions.Get<WebFrontend.Services.EmailSender.SendGrid.AuthMessageSenderOptions>() != null)
            {
                services.AddSendGridEmailSender(sendGridEmailSenderOptions);
                logger.LogInformation("SendGrid configured.");
            }
            else
            {
                throw new Exception("EmailSender was not explicitly disabled, but no valid configuration was found.");
            }

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            });

            services.AddDbContext<WebFrontendContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("WebFrontendContext")));

            services.AddDbContext<WebFrontendAuditableContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("WebFrontendContext")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                logger.LogWarning("Development Mode is enabled.");
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSecurityHeaders(policies =>
            {
                policies
                    .AddFrameOptionsDeny()
                    .AddXssProtectionBlock()
                    .AddContentTypeOptionsNoSniff()
                    .AddReferrerPolicyNoReferrer()
                    .RemoveServerHeader()
                    .AddContentSecurityPolicy(configure =>
                    {
                        configure.AddDefaultSrc().None();
                        configure.AddScriptSrc()
                            .Self()
                            .From("https://cdnjs.cloudflare.com")
                            .From("https://unpkg.com")
                            .From("https://hcaptcha.com")
                            .From("https://*.hcaptcha.com");
                        configure.AddStyleSrc()
                            .Self()
                            .From("https://cdnjs.cloudflare.com")
                            .From("https://unpkg.com")
                            .From("https://hcaptcha.com")
                            .From("https://*.hcaptcha.com")
                            .From("https://fonts.googleapis.com");
                        configure.AddFrameSource()
                            .From("https://hcaptcha.com")
                            .From("https://*.hcaptcha.com");
                        configure.AddFontSrc().From("https://fonts.gstatic.com");
                        configure.AddImgSrc().Self();
                        configure.AddBaseUri().None();
                        configure.AddFormAction().Self();
                        configure.AddFrameAncestors().None();
                        configure.AddUpgradeInsecureRequests();
                    })
                    .AddFeaturePolicy(configure =>
                    {
                        configure.AddAccelerometer().None();
                        configure.AddAmbientLightSensor().None();
                        configure.AddAutoplay().None();
                        configure.AddCustomFeature("battery").None();
                        configure.AddCamera().None();
                        configure.AddCustomFeature("display-capture").None();
                        configure.AddCustomFeature("document-domain").None();
                        configure.AddEncryptedMedia().None();
                        configure.AddCustomFeature("execution-while-not-rendered").None();
                        configure.AddCustomFeature("execution-while-out-of-viewport").None();
                        configure.AddFullscreen().None();
                        configure.AddGeolocation().None();
                        configure.AddGyroscope().None();
                        configure.AddCustomFeature("layout-animations").None();
                        configure.AddCustomFeature("legacy-image-formats").None();
                        configure.AddMagnetometer().None();
                        configure.AddMicrophone().None();
                        configure.AddMidi().None();
                        configure.AddCustomFeature("oversize-images").None();
                        configure.AddPayment().None();
                        configure.AddPictureInPicture().None();
                        configure.AddCustomFeature("publickey-credentials-get").None();
                        configure.AddCustomFeature("require-sri-for script style");
                        configure.AddSyncXHR().None();
                        configure.AddCustomFeature("unoptimized-images").None();
                        configure.AddCustomFeature("unsized-media").None();
                        configure.AddUsb().None();
                        configure.AddCustomFeature("wake-lock").None();
                        configure.AddCustomFeature("xr-spatial-tracking").None();
                    });
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
