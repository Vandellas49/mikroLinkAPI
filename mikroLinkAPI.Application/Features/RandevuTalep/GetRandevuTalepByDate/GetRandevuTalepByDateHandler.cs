using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.RandevuTalep.GetRandevuTalepByDate
{
    internal sealed class GetRandevuTalepByDate(IRandevuPlanlamaRepository randevuPlanlamaRepository, ICurrentUserService currentUser) : IRequestHandler<GetRandevuTalepByDateCommand, Result<List<RandevuTalepByDateVM>>>
    {
        public async Task<Result<List<RandevuTalepByDateVM>>> Handle(GetRandevuTalepByDateCommand request, CancellationToken cancellationToken)
        {
            return await randevuPlanlamaRepository.Includes(p => p.Randevu)
                                           .Where(p => p.CompanyId == currentUser.UserCompanyId && p.RandevuTarihi == DateTime.Parse(request.Date))
                                           .Select(p => new RandevuTalepByDateVM
                                           {
                                               Id = p.Id,
                                               Time = p.RandevuBaslangic + "-" + p.RandevuBitis,
                                               IsTaken = p.Randevu != null
                                           }).ToListAsync(cancellationToken);
        }
    }
}
