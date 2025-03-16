using MediatR;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.ViewModel;


namespace mikroLinkAPI.Application.Features.HangFire.GetReportById
{
    public sealed record GetReportByIdCommand(int Id) : IRequest<Result<byte[]>>;

}
