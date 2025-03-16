using Confluent.Kafka.Admin;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using mikroLinkAPI.Domain.Interfaces.Kafka;

namespace mikroLinkAPI.Infrastructure.KafkaMessaging
{
    public class KafkaConfigService(ILogger<KafkaConfigService> logger) : IKafkaConfigService
    {
        private readonly string _bootstrapServers = "localhost:9092";
        private readonly ILogger<KafkaConfigService> _logger = logger;

        public async Task<string> DeleteTopicAsync(string topicName)
        {
            using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = _bootstrapServers }).Build();

            try
            {
                await adminClient.DeleteTopicsAsync(new[] { topicName });
                return $"Topic '{topicName}' deleted successfully.";
            }
            catch (DeleteTopicsException ex)
            {
                _logger.LogError($"Error while deleting topic '{topicName}': {ex.Results[0].Error.Reason}");
                throw;
            }
        }

        public async Task EnsureTopicExistsAsync(string topicName, int numPartitions = 3, short replicationFactor = 1)
        {
            using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = _bootstrapServers }).Build();

            try
            {
                var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(5));
                if (metadata.Topics.Any(t => t.Topic == topicName))
                {
                    _logger.LogInformation($"Topic '{topicName}' already exists.");
                }
                else
                {

                    var topicSpecification = new TopicSpecification
                    {
                        Name = topicName,
                        NumPartitions = numPartitions,
                        ReplicationFactor = replicationFactor,
                        Configs = new Dictionary<string, string>
                        {
                            { "retention.ms", "3600000" } // 🕐 1 saat (3600000 ms)
                        }
                    };

                    await adminClient.CreateTopicsAsync(new[] { topicSpecification });

                    _logger.LogInformation($"Topic '{topicName}' created successfully.");
                }
            }
            catch (CreateTopicsException ex) when (ex.Results.Any(r => r.Error.Code == ErrorCode.TopicAlreadyExists))
            {
                _logger.LogWarning($"Topic '{topicName}' already exists.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while creating topic '{topicName}': {ex.Message}");
                throw;
            }
        }
    }
}
