using AutoMapper;
using GenericRepository;
using MediatR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Requests.MetarialSiteRequest
{
    internal sealed class MetarialSiteRequestHandler(
        IRequestRepository requestRepository,
        IMetarialRepository metarialRepository,
        ICurrentUserService currentUser,
        IMapper mapper,
        IUnitOfWork unitOfWork) : IRequestHandler<MetarialSiteRequestCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(MetarialSiteRequestCommand request, CancellationToken cancellationToken)
        {
            var result = mapper.Map<Request>(request);
            foreach (var item in request.RequestMaterial)
            {
                var malzeme = await metarialRepository.FirstOrDefaultAsync(p => p.Id == item.Id, cancellationToken);
                malzeme.TeamLeaderId = currentUser.UserId;
                malzeme.SiteId = null;
                malzeme.CompanyId = null;
                malzeme.CreatedDate = DateTime.Now;
                malzeme.CreatedBy = currentUser.UserId;
                metarialRepository.Update(malzeme);
            }
            result.ReceiverCompanyId = currentUser.UserCompanyId;
            result.TeamLeaderId = currentUser.UserId;
            result.WhoDoneId = currentUser.UserId;
            await requestRepository.AddAsync(result,cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var res = new Result<string>($"Başarılı şekilde talep yapıldı");
            res.IslemId = result.Id;
            return res;
        }
    }
}
