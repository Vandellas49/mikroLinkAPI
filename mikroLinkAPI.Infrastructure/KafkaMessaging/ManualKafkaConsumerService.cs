using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Interfaces.Kafka;
using mikroLinkAPI.Infrastructure.Services;
namespace mikroLinkAPI.Infrastructure.KafkaMessaging
{
    public class ManualKafkaConsumerService(IKafkaConfig kafkaConfig, IServiceScopeFactory serviceScopeFactory) : IManualKafkaConsumerService
    {
        private readonly IKafkaConfig _kafkaConfig = kafkaConfig;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        public List<string> GetRecentMessages(string topic, int count)
        {
            using var scope = _serviceScopeFactory.CreateScope(); // 🔹 Scoped servis oluştur
            var _currentUserService = scope.ServiceProvider.GetRequiredService<ICurrentUserService>();
            var messages = new List<string>();
            var config = _kafkaConfig.GetConsumerConfig("manual-consumer-group");

            using var consumer = new ConsumerBuilder<int, string>(config).Build();
            consumer.Subscribe(topic);

            int maxWaitTimeMs = 25000; // Maksimum bekleme süresi
            int elapsedTime = 0;
            int pollInterval = 1800;  // Her turda 500ms bekleyerek mesajları al

            while (messages.Count < count && elapsedTime < maxWaitTimeMs)
            {
                var consumeResult = consumer.Consume(TimeSpan.FromMilliseconds(pollInterval));
                if (consumeResult != null && consumeResult.Message != null && consumeResult.Message.Key == _currentUserService.UserCompanyId)
                {
                    messages.Add(consumeResult.Message.Value);
                }
                elapsedTime += pollInterval; // Geçen süreyi takip et
            }

            consumer.Close();
            messages.Reverse();
            return messages;
        }

    }
}
