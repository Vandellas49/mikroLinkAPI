using MediatR;
using Microsoft.AspNetCore.Http;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.MetarialAddByExcel
{
    public sealed record MetarialAddByExcelCommand(IFormFile File) : IRequest<Result<string>>;
}
