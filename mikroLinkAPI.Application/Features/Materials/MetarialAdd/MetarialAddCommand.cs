using MediatR;
using mikroLinkAPI.Domain.Attributes;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.MetarialAdd
{
    [Transaction]
    public sealed record MetarialAddCommand(string SeriNo, string ComponentId, string GIrsaliyeNo, int Sturdy, int Defective, int Scrap, string Shelf, int MaterialType,int State,int? SiteId) : IRequest<Result<string>>;
}
