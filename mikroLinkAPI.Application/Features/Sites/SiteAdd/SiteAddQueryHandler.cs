using AutoMapper;
using GenericRepository;
using MediatR;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Sites.SiteAdd{
    internal sealed class SiteAddQueryHandler(ISiteRepository siteRepository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<SiteAddQueryCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(SiteAddQueryCommand request, CancellationToken cancellationToken)
        {
            var result = await siteRepository.FirstOrDefaultAsync(p => p.SiteName == request.SiteName, cancellationToken);
            if (result != null)
                return Result<string>.Failure("Saha zaten kayıtlı");
            await siteRepository.AddAsync(mapper.Map<Site>(request), cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Saha başarıyla kaydedildi";
        }
    }
}
