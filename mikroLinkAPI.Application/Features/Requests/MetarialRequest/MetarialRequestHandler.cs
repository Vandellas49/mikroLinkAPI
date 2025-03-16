using AutoMapper;
using GenericRepository;
using MediatR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Requests.MetarialRequest
{
    internal sealed class MetarialRequestHandler(
        IRequestRepository requestRepository,
        ICurrentUserService currentUser,
        IMapper mapper,
        IUnitOfWork unitOfWork) : IRequestHandler<MetarialRequestCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(MetarialRequestCommand request, CancellationToken cancellationToken)
        {
            var TradingCompany= request.TradingCompany ?? currentUser.UserCompanyId;
            if (request.Model.Count == 0 || request.Model.Sum(p => p.Sturdy + p.Defective + p.Scrap) == 0)
                return Result<string>.Failure("0 malzeme çıkış talebi yapılamaz");
            var result = mapper.Map<Request>(request);
            result.CompanyId = TradingCompany;
            result.WhoDoneId = currentUser.UserId;
            await requestRepository.AddAsync(result, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var res = new Result<string>($"Başarılı şekilde talep yapıldı");
            res.IslemId = result.Id;
            return res;
        }
    }
}
