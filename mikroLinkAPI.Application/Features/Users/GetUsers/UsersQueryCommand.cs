using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Features.Materials.GetMetarials;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
using System.Globalization;

namespace mikroLinkAPI.Application.Features.Users.GetUsers
{
    public sealed record UsersQueryCommand(FilterUsers Filters, PageSettings Page) : IRequest<Result<Inventory<AccountSsomVM>>>;

    internal sealed class SiteIdOrSiteNameQueryHandler(
        IUserRepository userRepository,
        IMapper mapper,
        ICurrentUserService currentUserService
        ) : IRequestHandler<UsersQueryCommand, Result<Inventory<AccountSsomVM>>>
    {
        public async Task<Result<Inventory<AccountSsomVM>>> Handle(UsersQueryCommand request, CancellationToken cancellationToken)
        {
            int userCompanyId = currentUserService.UserCompanyId;
            var query= userRepository.Where(p => p.CompanyId == currentUserService.UserCompanyId &&
                                            p.Status).
                                 Include(p => p.AccountAuthority.Where(p=>p.Authority.UygulamaKodu== "MalzemeTakip")).
                                 ThenInclude(p=>p.Authority).
                                 Include(p=>p.Company).AsQueryable();
            if (request.Filters.Name != null)
                query = query.FilterBy(p => p.Name, request.Filters.Name.matchMode, request.Filters.Name.value);
            if (request.Filters.Authority != null)
                query = query.Where(p => p.AccountAuthority.Any(c => c.AuthorityId == request.Filters.Authority.value));
            if (request.Filters.Surname!=null)
                query = query.FilterBy(p => p.Surname, request.Filters.Surname.matchMode, request.Filters.Surname.value);
            if (request.Filters.Email != null)
                query = query.FilterBy(p => p.Email, request.Filters.Email.matchMode, request.Filters.Email.value);
            if (request.Filters.PhoneNumber != null)
                query = query.FilterBy(p => p.PhoneNumber, request.Filters.PhoneNumber.matchMode, request.Filters.PhoneNumber.value);
            if (request.Filters.OtherPhoneNumber != null)
                query = query.FilterBy(p => p.PhoneNumberTwo, request.Filters.OtherPhoneNumber.matchMode, request.Filters.OtherPhoneNumber.value);
            if (request.Page.sorted != null && !string.IsNullOrEmpty(request.Page.sorted.attributeName))
            {
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                query = query.OrderByName(textInfo.ToTitleCase(request.Page.sorted.attributeName), request.Page.sorted.order);
            }
            else
            {
                query = query.OrderBy(p => p.Id);
            }
            var items = mapper.Map<List<AccountSsomVM>>(await query.Skip(request.Page.page * request.Page.pageSize).Take(request.Page.pageSize).ToListAsync(cancellationToken));
            var totalCount = await query.CountAsync(cancellationToken);
            return new Inventory<AccountSsomVM>(items, totalCount);
        }
    }
}
