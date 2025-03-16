using Confluent.Kafka;
using mikroLinkAPI.Domain.Interfaces.Kafka;

namespace mikroLinkAPI.Infrastructure.KafkaMessaging
{
    public class KafkaConfig : IKafkaConfig
    {
        private readonly string _bootstrapServers = "localhost:9092"; // Kafka adresi

        public ConsumerConfig GetConsumerConfig(string groupId)
        {
            return new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest, // Eski mesajları da al
                EnableAutoCommit = false
            };
        }

        public ProducerConfig GetProducerConfig()
        {
            return new ProducerConfig
            {
                BootstrapServers = _bootstrapServers
            };
        }
    }

}
