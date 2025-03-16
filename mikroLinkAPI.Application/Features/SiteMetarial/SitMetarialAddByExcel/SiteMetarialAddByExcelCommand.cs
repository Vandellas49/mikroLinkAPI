using MediatR;
using Microsoft.AspNetCore.Http;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.SiteMetarialAddByExcel
{
    public sealed record SiteMetarialAddByExcelCommand(IFormFile File) : IRequest<Result<string>>;
}
