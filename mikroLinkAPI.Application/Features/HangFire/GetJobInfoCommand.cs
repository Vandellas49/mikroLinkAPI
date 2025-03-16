using MediatR;
using mikroLinkAPI.Domain.ViewModel;


namespace mikroLinkAPI.Application.Features.HangFire
{
    public sealed record GetJobInfoCommand(PageSettings Page,int? JobType,DateTime CreatedDate) : IRequest<Result<Inventory<GetJobInfoResponse>>>;

}
