using ShelterHelper.Core.Domain.UserBookings;
using MassTransit;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;

namespace ShelterHelper.EmailService.Events
{
    public class UserBookedShelterEventHandler : IConsumer<UserBookedShelterEvent>
    {
        private readonly ILogger<UserBookedShelterEventHandler> logger;
        private readonly IConfiguration configuration;
        private readonly IMailjetClient mailjetClient;

        public UserBookedShelterEventHandler(ILogger<UserBookedShelterEventHandler> logger, IConfiguration configuration, IMailjetClient mailjetClient)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.mailjetClient = mailjetClient;
        }

        public async Task Consume(ConsumeContext<UserBookedShelterEvent> notification)
        {
            var msg = notification.Message;

            logger.LogInformation($"Sending a check-in email to {msg.UserName} ({msg.UserEmail})");

            var email = new TransactionalEmailBuilder()
                   .WithFrom(new SendContact(configuration["Mailjet:Sender"]))
                   .WithSubject("Shelter: Check-in")
                   .WithHtmlPart($"<h1>Welcome</h1>Hello {msg.UserName}, enjoy your staying with us!")
                   .WithTo(new SendContact(msg.UserEmail))
                   .Build();

            await mailjetClient.SendTransactionalEmailAsync(email);
        }
    }
}
