namespace mikroLinkAPI.Domain.Interfaces.Kafka
{
    public interface IKafkaConsumerService
    {
        void StartListening(string topic, Action<string> onMessageReceived, CancellationToken cancellationToken);
    }
}
