using MassTransit;
using Mailjet.Client;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var config = context.Configuration;

        services.AddMassTransit(x =>
        {
            x.AddConsumers(Assembly.GetExecutingAssembly());
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(config.GetValue<Uri>("RabbitMq:Uri"), h =>
                {
                    h.Username(config["RabbitMq:Username"]);
                    h.Password(config["RabbitMq:Password"]);
                });

                cfg.ReceiveEndpoint("shelter-emailservice-listener", e =>
                {
                    e.ConfigureConsumers(context);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddHttpClient<IMailjetClient, MailjetClient>(client =>
        {
            client.SetDefaultSettings();
            client.UseBasicAuthentication(config["Mailjet:ApiKey"], config["Mailjet:ApiSecret"]);
        });
    })
    .Build();

await host.RunAsync();
