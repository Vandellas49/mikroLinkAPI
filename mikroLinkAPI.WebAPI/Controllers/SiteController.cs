using MediatR;
using Microsoft.AspNetCore.Mvc;
using mikroLinkAPI.Application.Features.Sites.GetSite;
using mikroLinkAPI.Application.Features.Sites.GetSiteBySiteOrSiteId;
using mikroLinkAPI.Application.Features.Sites.GetSiteFromCache;
using mikroLinkAPI.Application.Features.Sites.SiteAdd;
using mikroLinkAPI.Application.Features.Sites.SiteDelete;
using mikroLinkAPI.Application.Features.Sites.SiteExport;
using mikroLinkAPI.Application.Features.Sites.SiteUpdate;
using mikroLinkAPI.WebAPI.Abstractions;

namespace mikroLinkAPI.WebAPI.Controllers
{
    public sealed class SiteController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> SiteList(SiteQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> SiteBySiteName(SiteIdOrSiteNameQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> SiteExcel(SiteExportCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> SiteAdd(SiteAddQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> SiteDelete(SiteDeleteQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> SiteUpdate(SiteUpdateQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> GetSiteFromCache(SiteFromCacheQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}