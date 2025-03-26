using MediatR;
using Microsoft.AspNetCore.Mvc;
using mikroLinkAPI.Application.Features.Materials.GetMetarialForArea;
using mikroLinkAPI.Application.Features.Materials.GetMetarialForRequest;
using mikroLinkAPI.Application.Features.Materials.GetMetarialForRequest.GetMatreialByRequestAndSeriNo;
using mikroLinkAPI.Application.Features.Materials.GetMetarialForSite;
using mikroLinkAPI.Application.Features.Materials.GetMetarialForTeamLeader;
using mikroLinkAPI.Application.Features.Materials.GetMetarialForVerification;
using mikroLinkAPI.Application.Features.Materials.GetMetarials;
using mikroLinkAPI.Application.Features.Materials.MetarialAdd;
using mikroLinkAPI.Application.Features.Materials.MetarialAddByExcel;
using mikroLinkAPI.Application.Features.Materials.MetarialExit;
using mikroLinkAPI.Application.Features.Materials.MetarialExitExport;
using mikroLinkAPI.Application.Features.Materials.MetarialExport;
using mikroLinkAPI.Application.Features.Materials.MetarialForTeamLeader;
using mikroLinkAPI.Application.Features.Materials.MetarialIn;
using mikroLinkAPI.Application.Features.Materials.MetarialInExport;
using mikroLinkAPI.Application.Features.Materials.MetarialVerification;
using mikroLinkAPI.Application.Features.Materials.MetarialVerificationForArea;
using mikroLinkAPI.Application.Features.Materials.SiteMetarialAddByExcel;
using mikroLinkAPI.Application.Features.SiteMetarial.GetMetarials;
using mikroLinkAPI.WebAPI.Abstractions;

namespace mikroLinkAPI.WebAPI.Controllers
{
    public sealed class MetarialController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> MetarialList(MetarialQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> MetarialExcel(MetarialExportCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> MetarialAdd(MetarialAddCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> MetarialAddByExcel([FromForm] MetarialAddByExcelCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> SiteMetarialAddByExcel([FromForm] SiteMetarialAddByExcelCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> SiteMetarialList(SiteMetarialQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        } 
        [HttpPost]
        public async Task<IActionResult> GetMetarialVerification(MetarialVerificationQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }     
        [HttpPost]
        public async Task<IActionResult> GetMetarialForArea(MetarialVerificationForAreaQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }     
        [HttpPost]
        public async Task<IActionResult> MetarialVerification(MetarialVerificationCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }  
        [HttpPost]
        public async Task<IActionResult> MetarialVerificationForArea(MetarialVerificationCommandForArea request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> MetarialGirisLog(MetarialInCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> MetarialGirisLogExcel(MetarialInExportCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> MetarialCikisLog(MetarialExitCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> MetarialCikisLogExcel(MetarialExitExportCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> GetMetarialRequest(MetarialRequestQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> GetMetarialSiteRequest(MetarialBySiteQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }  
        [HttpPost]
        public async Task<IActionResult> GetMetarialForTeamLeader(GetMetarialForTeamLeaderCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> MetarialForTeamLeader(MetarialForTeamLeaderCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> GetMetarialRequestBySeriNo(GetMatreialByRequestAndSeriNoCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}