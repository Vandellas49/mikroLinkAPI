using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mikroLinkAPI.Application.Features.Auth.Login;
using mikroLinkAPI.WebAPI.Abstractions;

namespace mikroLinkAPI.WebAPI.Controllers
{
    [AllowAnonymous]
    public sealed class AuthController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}