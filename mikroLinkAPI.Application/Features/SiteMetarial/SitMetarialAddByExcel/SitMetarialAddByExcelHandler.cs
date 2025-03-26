using Hangfire;
using MediatR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Interfaces;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.SiteMetarialAddByExcel
{
    internal sealed class SitMetarialAddByExcelHandler(IBackgroundJobClient _backgroundJobClient, IExcelHelper excelHelper, ICurrentUserService currentUserService) : IRequestHandler<SiteMetarialAddByExcelCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(SiteMetarialAddByExcelCommand request, CancellationToken cancellationToken)
        {
            if (Path.GetExtension(request.File.FileName) != ".xlsx")
                return Result<string>.Failure("Lütfen excel dosyası yükleyin");
            var fileid = await excelHelper.GenerateFilePathAsync(request.File);
            _backgroundJobClient.Enqueue<IProcessSiteMetarilInsertExcelJob>(x => x.RunAsync(fileid, currentUserService.UserId, null));
            return "İşlem sıraya alındı";

        }
    }
}
