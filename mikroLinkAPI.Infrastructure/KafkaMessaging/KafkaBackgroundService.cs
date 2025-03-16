using Microsoft.Extensions.Hosting;
using mikroLinkAPI.Domain.Interfaces.Kafka;

namespace mikroLinkAPI.Infrastructure.KafkaMessaging
{
    public class KafkaBackgroundService(IKafkaConsumerService consumerService) : BackgroundService
    {
        private readonly IKafkaConsumerService _consumerService = consumerService;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                _consumerService.StartListening("company-topic", message =>
                {
                    Console.WriteLine($"Kafka Mesajı Alındı: {message}");
                }, stoppingToken);
            }, stoppingToken);
        }
    }
}
