namespace mikroLinkAPI.Application.Services
{
    public interface IExcelConvert
    {
        byte[] ModelToExcel<TModel>(List<TModel> collection);
    }
    
}