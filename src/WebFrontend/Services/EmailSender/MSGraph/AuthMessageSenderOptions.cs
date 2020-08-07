using System.ComponentModel.DataAnnotations;

namespace WebFrontend.Services.EmailSender.MSGraph
{
    public class AuthMessageSenderOptions
    {
        public string ClientID { get; set; }
        public string TenantID { get; set; }
        public string ClientSecret { get; set; }
        [EmailAddress]
        public string Sender { get; set; }
        public string From { get; set; }
    }
}
