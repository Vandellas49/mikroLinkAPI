namespace mikroLinkAPI.Application.Features
{
    public record Inventory<T>(List<T> Items,int TotalCount);
  
}
