using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Features.Materials.GetMetarials;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
using System.Globalization;

namespace mikroLinkAPI.Application.Features.Requests.GetRequests
{
    public sealed record RequestsQueryCommand(
        FilterByRequest Filters,
        PageSettings Page, DynamicParameter Dynamicfield) : IRequest<Result<Inventory<RequestsModelVM>>>;

    internal sealed class RequestsQueryHandler(
        IRequestRepository requestRepository,
        IMapper mapper,
        ICurrentUserService currentUserService) : IRequestHandler<RequestsQueryCommand, Result<Inventory<RequestsModelVM>>>
    {
        public async Task<Result<Inventory<RequestsModelVM>>> Handle(RequestsQueryCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Request> query = requestRepository.Where(p => true).
                Include(p => p.RequestedMaterial).
                ThenInclude(p => p.Component).
                Include(p => p.TeamLeader).
                Include(p => p.WhoDone).
                Include(p => p.ReceiverCompany).
                Include(p => p.RequestSiteCompanySerial).
                ThenInclude(c=>c.Cserial).
                Include(p => p.ReceiverSite).Where(p => p.CompanyId == currentUserService.UserCompanyId && p.RequestStatu==(int)RequestStatu.StockMan);
            if (request.Filters.Id != null)
                query = query.FilterBy(p => p.Id, request.Filters.Id.matchMode, request.Filters.Id.value);
            if (request.Filters.WorkOrderNo != null)
                query = query.FilterBy(p => p.WorkOrderNo, request.Filters.WorkOrderNo.matchMode, request.Filters.WorkOrderNo.value);
            if (request.Filters.RequestStatu != null)
                query = query.FilterBy(p => p.RequestStatu, request.Filters.RequestStatu.matchMode, request.Filters.RequestStatu.value);
            if (request.Filters.RequestType != null)
                query = query.FilterBy(p => p.RequestType, request.Filters.RequestType.matchMode, request.Filters.RequestType.value);
            if (request.Filters.CreatedBy != null)
                query = query.FilterBy(p => p.WhoDone.UserName, request.Filters.CreatedBy.matchMode, request.Filters.CreatedBy.value);
            if (request.Filters.Aciklama != null)
                query = query.FilterBy(p => p.RequestMessage, request.Filters.Aciklama.matchMode, request.Filters.Aciklama.value);
            if (request.Filters.TeamlLeader != null)
                query = query.FilterBy(p => p.TeamLeader, request.Filters.TeamlLeader.matchMode, request.Filters.TeamlLeader.value);
            if (request.Filters.RequestDestination != null)
            {
                query = query.FilterBy(p => p.ReceiverCompany.Name, request.Filters.RequestDestination.matchMode, request.Filters.RequestDestination.value);
                query = query.FilterBy(p => p.ReceiverSite.SiteName, request.Filters.RequestDestination.matchMode, request.Filters.RequestDestination.value);
            }
            if (request.Page.sorted != null && !string.IsNullOrEmpty(request.Page.sorted.attributeName))
            {
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                query = query.OrderByName(textInfo.ToTitleCase(request.Page.sorted.attributeName), request.Page.sorted.order);
            }
            else
                query = query.OrderBy(p => p.Id);
            var items = mapper.Map<List<RequestsModelVM>>(await query.Skip(request.Page.page * request.Page.pageSize).Take(request.Page.pageSize).ToListAsync(cancellationToken));
            var totalCount = await query.CountAsync(cancellationToken);
            return new Inventory<RequestsModelVM>(items, totalCount);
        }
    }
}
