using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Companies.CompanyDelete
{
    public sealed record CompanyDeleteQueryCommand(int Id) : IRequest<Result<string>>;
    internal sealed class CompanyDeleteQueryHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork) : IRequestHandler<CompanyDeleteQueryCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CompanyDeleteQueryCommand request, CancellationToken cancellationToken)
        {
            var firma = await companyRepository.Where(p => p.Id == request.Id).FirstOrDefaultAsync();
            companyRepository.Delete(firma);
            await unitOfWork.SaveChangesAsync();
            return "Firma başarılı şekilde silindi";
        }
    }
}
