using AutoMapper;
using MediatR;
using mikroLinkAPI.Application.Features.Requests.MetarialRequest;
using mikroLinkAPI.Application.Features.Requests.RequestDelete;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Requests.MetarialRequestUpdate
{
    internal sealed class MetarialRequestUpdateHandler(
        IMapper mapper,
        IRequestHandler<MetarialRequestCommand, Result<string>> requestHandler,
        IRequestHandler<RequestDeleteQueryCommand, Result<string>> deleteHandler) : IRequestHandler<MetarialRequestUpdateCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(MetarialRequestUpdateCommand request, CancellationToken cancellationToken)
        {
            await deleteHandler.Handle(new RequestDeleteQueryCommand(request.Id), cancellationToken);
            return await requestHandler.Handle(mapper.Map<MetarialRequestCommand>(request), cancellationToken);
        }
    }
}
