using AutoMapper;
using GenericRepository;
using MediatR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Interfaces.Kafka;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Materials.MetarialAdd
{
    internal sealed class MetarialAddHandler(IMetarialRepository metarialRepository, IMapper mapper, IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<MetarialAddCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(MetarialAddCommand request, CancellationToken cancellationToken)
        {
            if (metarialRepository.Any(p => p.SeriNo != "SarfMalzeme" && p.SeriNo == request.SeriNo))
                return Result<string>.Failure("Bu malzeme kayıtlı");
            if (request.Sturdy + request.Defective + request.Scrap == 0)
                return Result<string>.Failure("sağlam,hurda yada arzalı değeri girmelisiniz");
            var seri = mapper.Map<ComponentSerial>(request);
            seri.CreatedBy = currentUser.UserId;
            if (request.SiteId == null)
                seri.CompanyId = currentUser.UserCompanyId;
            seri.CreatedDate = DateTime.Now;
            await metarialRepository.AddAsync(seri, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        
            return "Malzeme başarıyla kaydedildi";


        }
    }
}
