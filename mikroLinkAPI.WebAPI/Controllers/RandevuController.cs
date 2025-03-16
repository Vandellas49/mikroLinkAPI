using MediatR;
using Microsoft.AspNetCore.Mvc;
using mikroLinkAPI.Application.Features.RandevuPlanlama.GetRandevuPlanlama;
using mikroLinkAPI.Application.Features.RandevuPlanlama.RandevuPlanlamaAdd;
using mikroLinkAPI.Application.Features.RandevuTalep.GetRandevuTalep;
using mikroLinkAPI.Application.Features.RandevuTalep.GetRandevuTalepByDate;
using mikroLinkAPI.Application.Features.RandevuTalep.RandevuTalep;
using mikroLinkAPI.WebAPI.Abstractions;

namespace mikroLinkAPI.WebAPI.Controllers
{
    public sealed class RandevuController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> RandevuPlanAdd(RandevuPlanlamaAddCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> GetRandevuPlanlama(GetRandevuPlanlamaCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        } 
        [HttpPost]
        public async Task<IActionResult> GetRandevuTalepByDate(GetRandevuTalepByDateCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }      
        [HttpPost]
        public async Task<IActionResult> GetRandevuTalep(GetRandevuTalepCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> RandevuTalep(RandevuTalepCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}