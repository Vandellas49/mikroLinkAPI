using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Requests.GetRequestById;
public class MetarialRequestResponse
{
    public int TalepTip { get; set; }
    public string Name { get; set; }
    public string Destination { get; set; }
    public string DestId { get; set; }
    public string TeamLeaderName { get; set; }
    public int TeamLeaderId { get; set; }
    public string WorkOrderNo { get; set; }
    public string Aciklama { get; set; }
    public List<MaterialsRequestVM> Material { get; set; }
    public List<ComponentSerialVM> Requests { get; set; }
}
