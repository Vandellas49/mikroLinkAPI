using MediatR;
using mikroLinkAPI.Domain.Interfaces.Kafka;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.RabbitMq
{
    public sealed record DeleteTopicCommand(string topicName) : IRequest<Result<string>>;
    internal sealed class DeleteTopicCommandHandler(IKafkaConfigService kafkaConfigService) : IRequestHandler<DeleteTopicCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            var message = await kafkaConfigService.DeleteTopicAsync("company-topic");
            return message;
        }
    }
}
