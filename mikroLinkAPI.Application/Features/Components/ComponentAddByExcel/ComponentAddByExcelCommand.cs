using MediatR;
using Microsoft.AspNetCore.Http;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Components.ComponentAddByExcel
{
    public sealed record ComponentAddByExcelCommand(IFormFile File) : IRequest<Result<string>>;
}
