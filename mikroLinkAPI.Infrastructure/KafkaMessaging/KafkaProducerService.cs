using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain;
using mikroLinkAPI.Domain.Interfaces.Kafka;
using System.Text.Json;


namespace mikroLinkAPI.Infrastructure.KafkaMessaging
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IProducer<int, string> _producer;

        public KafkaProducerService(IKafkaConfig kafkaConfig, IServiceScopeFactory serviceScopeFactory)
        {
            var config = kafkaConfig.GetProducerConfig();
            _serviceScopeFactory = serviceScopeFactory;
            _producer = new ProducerBuilder<int, string>(config).Build();
        }

        public async Task SendMessageAsync(FirmaEvent message,int CompanyId)
        {
            using var scope = _serviceScopeFactory.CreateScope(); // 🔹 Scoped servis oluştur
            var _currentUserService = scope.ServiceProvider.GetRequiredService<ICurrentUserService>();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            await _producer.ProduceAsync("company-topic", new Message<int, string> { Key = _currentUserService.UserCompanyId, Value = JsonSerializer.Serialize(message,options) });
        }
    }
}
