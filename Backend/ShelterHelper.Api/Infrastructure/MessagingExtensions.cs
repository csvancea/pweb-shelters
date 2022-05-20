using MassTransit;
using System.Reflection;

namespace ShelterHelper.Api.Infrastructure
{
    public static partial class MessagingExtensions
    {
        public static void AddMassTransitService(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.GetExecutingAssembly());
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(config.GetValue<Uri>("RabbitMq:Uri"), h =>
                    {
                        h.Username(config["RabbitMq:Username"]);
                        h.Password(config["RabbitMq:Password"]);
                    });

                    cfg.ReceiveEndpoint("shelter-api-listener", e =>
                    {
                        e.ConfigureConsumers(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
