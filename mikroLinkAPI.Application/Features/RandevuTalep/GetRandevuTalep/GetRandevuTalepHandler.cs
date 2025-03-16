using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.RandevuTalep.GetRandevuTalep
{
    internal sealed class GetRandevuTalepHandler(IRandevuPlanlamaRepository randevuPlanlamaRepository, ICurrentUserService currentUser) : IRequestHandler<GetRandevuTalepCommand, Result<List<RandevuTalepVM>>>
    {
        public async Task<Result<List<RandevuTalepVM>>> Handle(GetRandevuTalepCommand request, CancellationToken cancellationToken)
        {
            var result = await randevuPlanlamaRepository.Includes(p => p.Randevu.TeamLeader)
                                           .Where(p => p.CompanyId == currentUser.UserCompanyId && p.RandevuTarihi >= DateTime.Now.AddDays(-10) && p.Randevu != null)
                                           .Select(p => new RandevuTalepVM
                                           {
                                               Id = p.Randevu.Id,
                                               Date = p.RandevuTarihi,
                                               End = p.RandevuBitis,
                                               Start = p.RandevuBaslangic,
                                               Title = p.Randevu.TeamLeader.UserName + "-Malzeme talebi",
                                               Color = (p.RandevuTarihi < DateTime.Now || p.Randevu.Durum == 1) ? "#779ecb" : "#ff0000",
                                               TeamLeaderId=p.Randevu.TeamLeaderId,
                                               TeamLeaderName=p.Randevu.TeamLeader.UserName
                                           }).ToListAsync(cancellationToken);
            return result;
        }
    }
}
