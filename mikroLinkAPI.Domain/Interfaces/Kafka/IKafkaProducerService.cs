
namespace mikroLinkAPI.Domain.Interfaces.Kafka
{
    public interface IKafkaProducerService
    {
        Task SendMessageAsync(FirmaEvent message,int CompanyId);
    }
}
