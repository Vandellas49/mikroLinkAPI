using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Sites.SiteDelete
{
    internal sealed class SiteDeleteQueryHandler(ISiteRepository siteRepository, ICompanySiteRepository companySiteRepository, IUnitOfWork unitOfWork) : IRequestHandler<SiteDeleteQueryCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(SiteDeleteQueryCommand request, CancellationToken cancellationToken)
        {
            var site = await siteRepository.Where(p => p.Id == request.Id).
                Include(c=>c.CompanySiteAuthorization).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (site.CompanySiteAuthorization.Count != 0)
                companySiteRepository.DeleteRange(site.CompanySiteAuthorization);
            siteRepository.Delete(site);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Saha başarılı şekilde silindi";
        }
    }
}
