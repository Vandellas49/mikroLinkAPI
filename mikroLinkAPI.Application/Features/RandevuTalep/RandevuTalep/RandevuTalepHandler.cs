using GenericRepository;
using MediatR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.RandevuTalep.RandevuTalep
{
    internal sealed class RandevuTalepHandler(IRandevuRepository randevuRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork) : IRequestHandler<RandevuTalepCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(RandevuTalepCommand request, CancellationToken cancellationToken)
        {
            if (await randevuRepository.AnyAsync(c => c.RadevuPlanId == request.Id, cancellationToken))
                return Result<string>.Failure("Talep ettiğiniz randevu alınmıştır.Lütfen başka bir saat seçiniz");
            await randevuRepository.AddAsync(new Domain.Entities.Randevu { RadevuPlanId = request.Id, CompanyId = currentUserService.UserCompanyId, TeamLeaderId = currentUserService.UserId }, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Başarılı şekilde randevunuz oluşturuldu";
        }
    }
}
