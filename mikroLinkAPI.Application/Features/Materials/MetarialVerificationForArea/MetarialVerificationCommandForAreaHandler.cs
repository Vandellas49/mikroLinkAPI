using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
using mikroLinkAPI.Domain.ViewModel.ExcelModels;
namespace mikroLinkAPI.Application.Features.Materials.MetarialVerificationForArea
{
    internal sealed class MetarialVerificationCommandForAreaHandler(
        IMetarialRepository metarialRepository,
        ICurrentUserService currentUser,
        IMapper mapper,
         IExcelConvert excelConvert,
        IUnitOfWork unitOfWork) : IRequestHandler<MetarialVerificationCommandForArea, Result<string>>
    {
        public async Task<Result<string>> Handle(MetarialVerificationCommandForArea request, CancellationToken cancellationToken)
        {
            List<VerificationCompanyEX> excelmodel = [];
            foreach (var item in request.Model)
            {
                var cs = await metarialRepository.WhereWithTracking(p => p.ComponentId == item.ComponentId && p.SeriNo == item.SeriNo).FirstOrDefaultAsync(cancellationToken);
                cs.CreatedBy = currentUser.UserId;
                cs.CreatedDate = DateTime.Now;
                cs.State = (int)State.Verification;
                metarialRepository.Update(cs);
                var ex = mapper.Map<VerificationCompanyEX>(cs);
                ex.CreatedBy = currentUser.UserName;
                excelmodel.Add(ex);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result<string>($"Başarılı şekilde doğrulama yapıldı", excelConvert.ModelToExcel(excelmodel));
        }
    }
}
