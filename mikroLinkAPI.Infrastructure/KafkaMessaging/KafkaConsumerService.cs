using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Interfaces.Kafka;

namespace mikroLinkAPI.Infrastructure.KafkaMessaging
{
    public class KafkaConsumerService(IKafkaConfig kafkaConfig, IServiceScopeFactory serviceScopeFactory) : IKafkaConsumerService
    {
        private readonly IKafkaConfig _kafkaConfig = kafkaConfig;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        public void StartListening(string topic, Action<string> onMessageReceived, CancellationToken cancellationToken)
        {
            var config = _kafkaConfig.GetConsumerConfig("company-consumer-group");
            using var consumer = new ConsumerBuilder<int, string>(config).Build();
            consumer.Subscribe(topic);
            using var scope = _serviceScopeFactory.CreateScope();
            var _dashboardNotificationService = scope.ServiceProvider.GetRequiredService<IDashboardNotificationService>();
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumeResult = consumer.Consume(TimeSpan.FromSeconds(5));
                    if (consumeResult?.Message != null)
                    {
                        _dashboardNotificationService.NotificationAllChange(consumeResult.Message.Value, consumeResult.Message.Key);
                        onMessageReceived(consumeResult.Message.Value);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }
    }
}
