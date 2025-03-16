using MediatR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;


namespace mikroLinkAPI.Application.Features.HangFire.GetReportById
{
    internal sealed class GetReportByIdCommandHandler(IFileRecordRepository fileRecordRepository, ICurrentUserService currentUserService) : IRequestHandler<GetReportByIdCommand, Result<byte[]>>
    {
        public async Task<Result<byte[]>> Handle(GetReportByIdCommand request, CancellationToken cancellationToken)
        {
            var file = await fileRecordRepository.FirstOrDefaultAsync(c => c.Id == request.Id && currentUserService.UserId == c.CreatedBy);
            if (file == null)
                Result<byte[]>.Failure("dosya bulunamadı");
            return File.ReadAllBytes(file.FilePath);
        }
    }

}
