using MediatR;
using Microsoft.AspNetCore.Mvc;
using mikroLinkAPI.Application.Features.CompanySiteAuthorization;
using mikroLinkAPI.WebAPI.Abstractions;

namespace mikroLinkAPI.WebAPI.Controllers
{
    public sealed class CompanySiteAuthorizationController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> GetCompanySite(GetCompanySiteQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }      
        [HttpPost]
        public async Task<IActionResult> CompanySiteAuthAdd(CompanySiteQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}