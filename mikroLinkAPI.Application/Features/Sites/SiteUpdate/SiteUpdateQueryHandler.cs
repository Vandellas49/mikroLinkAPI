using AutoMapper;
using GenericRepository;
using MediatR;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Sites.SiteUpdate
{
    internal sealed class SiteUpdateQueryHandler(ISiteRepository siteRepository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<SiteUpdateQueryCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(SiteUpdateQueryCommand request, CancellationToken cancellationToken)
        {
            var result = await siteRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken, false);
            if (result == null)
                return Result<string>.Failure("Saha bulunamadı");
            if (await siteRepository.AnyAsync(p => request.SiteName != result.SiteName && p.SiteName == request.SiteName, cancellationToken))
                return Result<string>.Failure("Saha zaten kayıtlı");
            var site = mapper.Map<Site>(request);
            siteRepository.Update(site);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Firma başarıyla güncellendi";
        }
    }
}
