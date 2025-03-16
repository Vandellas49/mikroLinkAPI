using Confluent.Kafka;
namespace mikroLinkAPI.Domain.Interfaces.Kafka
{
    public interface IKafkaConfig
    {
        ConsumerConfig GetConsumerConfig(string groupId);
        ProducerConfig GetProducerConfig();
    }

}
