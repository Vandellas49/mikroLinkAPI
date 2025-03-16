using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
using System.Globalization;

namespace mikroLinkAPI.Application.Features.Companies.GetCompany
{
    internal sealed class CompanyQueryHandler(ICompanyRepository companyRepository, IMapper mapper) : IRequestHandler<CompanyQueryCommand, Result<Inventory<CompanyVM>>>
    {
        public async Task<Result<Inventory<CompanyVM>>> Handle(CompanyQueryCommand request, CancellationToken cancellationToken)
        {
            List<CompanyVM> items;
            IQueryable<Company> query = companyRepository.Where(p => true).Include(p => p.Il).Include(p=>p.AccountSsom).Include(p=>p.ComponentSerial);
            if (request.Filters.Name!=null)
            {
                    query = query.FilterBy(p=>p.Name, request.Filters.Name.matchMode, request.Filters.Name.value);
            }
            if (request.Filters.IlId != null)
            {
                query = query.FilterBy(p=>p.IlId, request.Filters.IlId.matchMode, request.Filters.IlId.value);
            }
            if (request.Filters.Email != null)
            {
                query = query.FilterBy(p=>p.Email, request.Filters.Email.matchMode, request.Filters.Email.value);
            }
            if (request.Page.sorted != null && !string.IsNullOrEmpty(request.Page.sorted.attributeName))
            {
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                query = query.OrderByName(textInfo.ToTitleCase(request.Page.sorted.attributeName), request.Page.sorted.order);
            }
            else
            {
                query = query.OrderBy(p => p.Name);
            }
            if (!request.Page.All)
                 items = mapper.Map<List<CompanyVM>>(await query.Skip(request.Page.page * request.Page.pageSize).Take(request.Page.pageSize).ToListAsync(cancellationToken));
            else
                items = mapper.Map<List<CompanyVM>>(await query.ToListAsync(cancellationToken));
            var totalCount = await query.CountAsync(cancellationToken: cancellationToken);
            return new Inventory<CompanyVM>(items, totalCount);

        }
    }


}
