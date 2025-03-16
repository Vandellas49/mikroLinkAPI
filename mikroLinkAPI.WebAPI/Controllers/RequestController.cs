using MediatR;
using Microsoft.AspNetCore.Mvc;
using mikroLinkAPI.Application.Features.Requests.GetReceivedRequests;
using mikroLinkAPI.Application.Features.Requests.GetRequestById;
using mikroLinkAPI.Application.Features.Requests.GetRequests;
using mikroLinkAPI.Application.Features.Requests.MetarialRequest;
using mikroLinkAPI.Application.Features.Requests.MetarialRequestComplate;
using mikroLinkAPI.Application.Features.Requests.MetarialRequestTeamLeader;
using mikroLinkAPI.Application.Features.Requests.MetarialRequestUpdate;
using mikroLinkAPI.Application.Features.Requests.MetarialSiteRequest;
using mikroLinkAPI.Application.Features.Requests.RequestDelete;
using mikroLinkAPI.WebAPI.Abstractions;

namespace mikroLinkAPI.WebAPI.Controllers
{
    public sealed class RequestController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> GetTalepler(RequestsQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> GetGelenTalepler(RequestsReceivedQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> GetGidenTalepler(FetchDispatchedRequestsQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> MetarialRequest(MetarialRequestCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }  
   
        [HttpPost]
        public async Task<IActionResult> MetarialSiteRequest(MetarialSiteRequestCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }  
        [HttpPost]
        public async Task<IActionResult> MetarialSiteRequestUpdate(MetarialRequestUpdateCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> GetRequestById(RequestByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }      
        [HttpPost]
        public async Task<IActionResult> MetarialRequestTeamLeader(MetarialRequestTeamLeaderCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }   
        [HttpPost]
        public async Task<IActionResult> MetarialRequestComplate(MetarialRequestComplateCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        } 
        [HttpPost]
        public async Task<IActionResult> RequestDelete(RequestDeleteQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}