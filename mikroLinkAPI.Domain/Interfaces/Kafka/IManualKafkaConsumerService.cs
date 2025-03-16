
namespace mikroLinkAPI.Domain.Interfaces.Kafka
{
    public interface IManualKafkaConsumerService
    {
        List<string> GetRecentMessages(string topic, int count);
    }

}
