using MediatR;
using Microsoft.AspNetCore.Mvc;
using mikroLinkAPI.Application.Features.Users.GetActiveUsers;
using mikroLinkAPI.Application.Features.Users.GetUsers;
using mikroLinkAPI.Application.Features.Users.TeamLeader;
using mikroLinkAPI.WebAPI.Abstractions;

namespace mikroLinkAPI.WebAPI.Controllers
{
    public sealed class UserController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> TeamLeaderByCompanyId(TeamLeaderByCompanyIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> GetUsers(UsersQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> GetActiveUsers(ActiveUsersCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}