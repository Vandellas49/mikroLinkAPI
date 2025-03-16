
namespace mikroLinkAPI.Domain.Interfaces.Kafka
{
    public interface IKafkaConfigService
    {
        Task EnsureTopicExistsAsync(string topicName, int numPartitions = 3, short replicationFactor = 1);
        Task<string> DeleteTopicAsync(string topicName);
    }

}
