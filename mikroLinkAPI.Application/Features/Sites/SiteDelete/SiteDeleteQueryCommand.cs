using MediatR;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Sites.SiteDelete;
public sealed record SiteDeleteQueryCommand(int Id) : IRequest<Result<string>>;
