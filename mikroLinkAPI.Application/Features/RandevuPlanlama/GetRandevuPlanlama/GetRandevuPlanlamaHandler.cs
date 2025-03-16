using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.RandevuPlanlama.GetRandevuPlanlama
{
    internal sealed class GetRandevuPlanlamaHandler(IRandevuPlanlamaRepository randevuPlanlamaRepository, IMapper mapper, ICurrentUserService currentUser) : IRequestHandler<GetRandevuPlanlamaCommand, Result<List<RandevuPlanlamaVM>>>
    {
        public async Task<Result<List<RandevuPlanlamaVM>>> Handle(GetRandevuPlanlamaCommand request, CancellationToken cancellationToken)
        {
            var result = mapper.Map<List<RandevuPlanlamaVM>>(await randevuPlanlamaRepository.Where(c => c.CompanyId == currentUser.UserCompanyId && c.RandevuTarihi >= DateTime.Now).ToListAsync(cancellationToken));
            return result;
        }
    }
}
