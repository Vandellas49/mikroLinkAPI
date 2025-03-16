using MediatR;
using Microsoft.AspNetCore.Mvc;
using mikroLinkAPI.Application.Features.Dashboard;
using mikroLinkAPI.Application.Features.HangFire;
using mikroLinkAPI.Application.Features.HangFire.GetReportById;
using mikroLinkAPI.Application.Features.RabbitMq;
using mikroLinkAPI.WebAPI.Abstractions;

namespace mikroLinkAPI.WebAPI.Controllers
{
    public sealed class DashBoardController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> GetDashBoard(DashboardQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }  
        [HttpPost]
        public async Task<IActionResult> GetEvents(GetPendingMessagesCommand request, CancellationToken cancellationToken)
        {
         var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }      
        [HttpPost]
        public async Task<IActionResult> DeleteTopic(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
         var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }   
        [HttpPost]
        public async Task<IActionResult> GetJobInfo(GetJobInfoCommand request, CancellationToken cancellationToken)
        {
         var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }  
        [HttpPost]
        public async Task<IActionResult> GetReportById(GetReportByIdCommand request, CancellationToken cancellationToken)
        {
         var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}