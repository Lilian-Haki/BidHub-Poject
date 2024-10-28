using static BidHub_Poject.Models.EmailConfiguration;

namespace BidHub_Poject.Services
{
    public interface IEmailService
    {
      Task SendOtpEmailAsync(EmailData emailData);  // (Method definition)This will allow any class that implements IEmailService to define the SendOtpEmailAsync method.

    }
}

