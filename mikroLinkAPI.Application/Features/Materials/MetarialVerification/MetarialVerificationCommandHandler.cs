using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
using mikroLinkAPI.Domain.ViewModel.ExcelModels;
namespace mikroLinkAPI.Application.Features.Materials.MetarialVerification
{
    internal sealed class MetarialVerificationCommandHandler(
        IMetarialRepository metarialRepository,
        IRequestSiteCompanySerialRepository requestSiteCompanySerialRepository,
        ICurrentUserService currentUser,
        IMapper mapper,
         IExcelConvert excelConvert,
        IUnitOfWork unitOfWork) : IRequestHandler<MetarialVerificationCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(MetarialVerificationCommand request, CancellationToken cancellationToken)
        {
            List<VerificationCompanyEX> excelmodel = [];
            foreach (var item in request.Model)
            {
                var cs = await metarialRepository.WhereWithTracking(p => p.ComponentId == item.ComponentId && p.SeriNo == item.SeriNo).FirstOrDefaultAsync(cancellationToken);
                cs.CreatedBy = currentUser.UserId;
                cs.CreatedDate = DateTime.Now;
                cs.CompanyId = request.OperationType==OperationType.Company?request.Id:null;
                cs.SiteId = request.OperationType==OperationType.Site?request.Id:null;
                cs.TeamLeaderId = request.OperationType==OperationType.TeamLeader?request.Id:null;
                cs.State = (int)State.Verification;
                var ConflictItems=request.OperationType==OperationType.Site?(item.SiteId != request.Id):
                                  request.OperationType==OperationType.TeamLeader?(item.TeamLeaderId==request.Id):
                                                                                   item.CompanyId!=request.Id;
                if (ConflictItems)
                {
                    cs.MaterialType = item.MaterialType;
                    cs.Shelf = item.Shelf;
                    cs.Defective = item.Defective;
                    cs.Scrap = item.Scrap;
                    cs.Sturdy = item.Sturdy;
                }
                await CheckRequestAndDelete(item.Id);
                metarialRepository.Update(cs);
                var ex = mapper.Map<VerificationCompanyEX>(cs);
                ex.CreatedBy = currentUser.UserName;
                excelmodel.Add(ex);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result<string>($"Başarılı şekilde doğrulama yapıldı", excelConvert.ModelToExcel(excelmodel));
        }
        private async Task CheckRequestAndDelete(int CSerialId)
        {
            var result = await requestSiteCompanySerialRepository.
                  Where(p => p.CserialId == CSerialId).
                  FirstOrDefaultAsync();
            if (result != null)
                requestSiteCompanySerialRepository.Delete(result);
        }
    }
}
