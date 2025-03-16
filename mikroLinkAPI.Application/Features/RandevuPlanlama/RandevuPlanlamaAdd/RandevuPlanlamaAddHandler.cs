using AutoMapper;
using GenericRepository;
using MediatR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.RandevuPlanlama.RandevuPlanlamaAdd
{
    internal sealed class RandevuPlanlamaAddHandler(IRandevuPlanlamaRepository randevuPlanlamaRepository, IMapper mapper, IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<RandevuPlanlamaAddCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(RandevuPlanlamaAddCommand request, CancellationToken cancellationToken)
        {
            if (request.planlama.Count == 0)
                return Result<string>.Failure("Lütfen randevu planlaması giriniz");
            var planlar = mapper.Map<List<RandevuPlanlanma>>(request.planlama);
            planlar.ForEach(c => c.CompanyId = currentUser.UserCompanyId);
            randevuPlanlamaRepository.AddRange(planlar);
            unitOfWork.SaveChanges();
            await Task.CompletedTask;
            return "Planlama başarılı şekilde kaydedildi";
        }
    }
}
