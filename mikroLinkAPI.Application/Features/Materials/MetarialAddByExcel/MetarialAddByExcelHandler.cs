using Hangfire;
using MediatR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Interfaces;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.MetarialAddByExcel
{
    internal sealed class MetarialAddByExcelHandler(IBackgroundJobClient _backgroundJobClient, IExcelHelper excelHelper,ICurrentUserService currentUserService) : IRequestHandler<MetarialAddByExcelCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(MetarialAddByExcelCommand request, CancellationToken cancellationToken)
        {
            var fileid = await excelHelper.GenerateFilePathAsync(request.File);
            _backgroundJobClient.Enqueue<IProcessMetarilInsertExcelJob>(x => x.RunAsync(fileid, currentUserService.UserId, currentUserService.UserCompanyId,null));
            return "İşlem sıraya alındı";
        }
    }
}
