using FluentValidation;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Features.Materials.GetMetarialForRequest;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using System.Text;

namespace mikroLinkAPI.Application.Features.Materials.MetarialVerification
{
    public sealed class MetarialRequestQueryCommandValidator : AbstractValidator<MetarialRequestQueryCommand>
    {

        public MetarialRequestQueryCommandValidator()
        {
            RuleFor(p => p.searchValue).NotEmpty().WithMessage("Parça KODU alanını giriniz");
            RuleFor(p => p.MetarialType).NotNull().
            WithMessage("Malzeme türünü giriniz");
        }
    }
}
