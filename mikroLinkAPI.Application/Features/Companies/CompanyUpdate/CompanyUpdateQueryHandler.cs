using AutoMapper;
using GenericRepository;
using MediatR;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Companies.CompanyUpdate
{
    internal sealed class CompanyUpdateQueryHandler(ICompanyRepository companyRepository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CompanyUpdateQueryCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CompanyUpdateQueryCommand request, CancellationToken cancellationToken)
        {
            var result = await companyRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken, false);
            if (result == null)
                return Result<string>.Failure("Firma bulunamadı");
            if (await companyRepository.AnyAsync(p => request.Name != result.Name && p.Name == request.Name, cancellationToken))
                return Result<string>.Failure("Firma zaten kayıtlı");
            var company = mapper.Map<Company>(request);
            companyRepository.Update(company);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Firma başarıyla güncellendi";
        }
    }
}
