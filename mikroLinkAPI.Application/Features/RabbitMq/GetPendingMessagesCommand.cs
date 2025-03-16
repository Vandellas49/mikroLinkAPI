using MediatR;
using mikroLinkAPI.Domain.Interfaces.Kafka;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.RabbitMq
{
    public sealed record GetPendingMessagesCommand : IRequest<Result<List<string>>>;
    internal sealed class GetPendingMessagesHandler(IManualKafkaConsumerService kafkaConsumerService) : IRequestHandler<GetPendingMessagesCommand, Result<List<string>>>
    {
        public async Task<Result<List<string>>> Handle(GetPendingMessagesCommand request, CancellationToken cancellationToken)
        {
            var messages = await Task.FromResult(kafkaConsumerService.GetRecentMessages("company-topic", 100));
            return messages;
        }
    }
}
