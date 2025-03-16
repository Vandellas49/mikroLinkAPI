using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
using System.Globalization;

namespace mikroLinkAPI.Application.Features.Components.ComponentAddByExcel
{
    //IComponentRepository componentRepository,
    internal sealed class ComponentAddByExcelHandler( IExcelHelper excelHelper) : IRequestHandler<ComponentAddByExcelCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(ComponentAddByExcelCommand request, CancellationToken cancellationToken)
        {
            var filePath = await excelHelper.GenerateFilePathAsync(request.File);
            //var x = QueryGenerate.ComponentAddValidationQuery(filePath);
            //var result = await hataRepository.PureSqlCommandAsync(QueryGenerate.ComponentAddValidationQuery(filePath));
            //if (result.Count == 0)
            //{
            //    var savaComponent = await componentRepository.PureSqlCommandExecuteAsync(QueryGenerate.ComponentAddQuery(filePath));
            //    return "data başarılı şekilde kaydedildi";
            //}
            //File.Delete(filePath);
            //return Result<string>.Failure("Exceldeki datalar eklenemedi. Hataları indiriliren excel dosyasından inceleyebilirsiniz.", excelConvert.ModelToExcel(result));
            return "";
        }
    }
}
