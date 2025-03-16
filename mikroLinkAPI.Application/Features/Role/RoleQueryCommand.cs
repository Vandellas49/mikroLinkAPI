using AutoMapper;
using MediatR;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Role
{
    public sealed record RoleQueryCommand() : IRequest<Result<List<AuthorityVM>>>;

    internal sealed class SiteIdOrSiteNameQueryHandler(
        IRoleRepository roleRepository,
        IMapper mapper
        ) : IRequestHandler<RoleQueryCommand, Result<List<AuthorityVM>>>
    {
        public async Task<Result<List<AuthorityVM>>> Handle(RoleQueryCommand request, CancellationToken cancellationToken) =>
            mapper.Map<List<AuthorityVM>>(await roleRepository.GetAllFromCacheAsync(p => p.UygulamaKodu == "MalzemeTakip"));
    }
}
