using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Companies.CompanyExport
{
    internal sealed class CompanyExportHandler(ICompanyRepository companyRepository, IMapper mapper, IExcelConvert convert) : IRequestHandler<CompanyExportCommand, Result<byte[]>>
    {
        public async Task<Result<byte[]>> Handle(CompanyExportCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Company> query = companyRepository.Where(p => true).Include(p => p.Il);
            if (request.filters.Name != null)
            {
                query = query.FilterBy(p=>p.Name, request.filters.Name.matchMode, request.filters.Name.value);
            }
            if (request.filters.IlId != null)
            {
                query = query.FilterBy(p=>p.Id, request.filters.IlId.matchMode, request.filters.IlId.value);
            }
            if (request.filters.Email != null)
            {
                query = query.FilterBy(p=>p.Email, request.filters.Email.matchMode, request.filters.Email.value);
            }
            var items = mapper.Map<List<CompanyVM>>(await query.ToListAsync(cancellationToken));
            return convert.ModelToExcel(items);
        }
    }
}
