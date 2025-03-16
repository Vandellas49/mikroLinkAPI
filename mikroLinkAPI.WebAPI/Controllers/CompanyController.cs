using MediatR;
using Microsoft.AspNetCore.Mvc;
using mikroLinkAPI.Application.Features.Companies.CompanyAdd;
using mikroLinkAPI.Application.Features.Companies.CompanyDelete;
using mikroLinkAPI.Application.Features.Companies.CompanyExport;
using mikroLinkAPI.Application.Features.Companies.CompanyUpdate;
using mikroLinkAPI.Application.Features.Companies.GetCompany;
using mikroLinkAPI.Application.Features.Companies.GetCompanyByName;
using mikroLinkAPI.WebAPI.Abstractions;

namespace mikroLinkAPI.WebAPI.Controllers
{
    public sealed class CompanyController(IMediator mediator) : ApiController(mediator)
    {
        [HttpPost]
        public async Task<IActionResult> CompanyList(CompanyQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CompanyAdd(CompanyAddQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }  
        [HttpPost]
        public async Task<IActionResult> CompanyUpdate(CompanyUpdateQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CompanyExcel(CompanyExportCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CompanyByName(CompanyByNameQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CompanyDelete(CompanyDeleteQueryCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}