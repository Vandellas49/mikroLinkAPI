using AutoMapper;
using mikroLinkAPI.Application.Features.Companies.CompanyAdd;
using mikroLinkAPI.Application.Features.Companies.CompanyUpdate;
using mikroLinkAPI.Application.Features.Components.ComponentAdd;
using mikroLinkAPI.Application.Features.Components.ComponentUpdate;
using mikroLinkAPI.Application.Features.Materials.MetarialAdd;
using mikroLinkAPI.Application.Features.Requests.GetRequestById;
using mikroLinkAPI.Application.Features.Requests.MetarialRequest;
using mikroLinkAPI.Application.Features.Requests.MetarialRequestUpdate;
using mikroLinkAPI.Application.Features.Requests.MetarialSiteRequest;
using mikroLinkAPI.Application.Features.Sites.SiteAdd;
using mikroLinkAPI.Application.Features.Sites.SiteUpdate;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.ViewModel;
using mikroLinkAPI.Domain.ViewModel.ExcelModels;

namespace mikroLinkAPI.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Iller, IlVM>().
                ForMember(p => p.Id, s => s.MapFrom(s => s.Id)).
                ForMember(p => p.Sehir, s => s.MapFrom(s => s.Sehir)).ReverseMap();

            CreateMap<Component, ComponentVM>()
                .ForMember(p => p.IsEditable, s => s.MapFrom(k => k.RequestedMaterial.Count <= 0 && k.ComponentSerial.Count <= 0))
                .ReverseMap();

            CreateMap<AccountSsom, AccountSsomVM>().
                ForMember(c => c.AuthorityId, s => s.MapFrom(k => k.AccountAuthority.FirstOrDefault().AuthorityId)).
                ForMember(c => c.Company, s => s.MapFrom(k => k.Company)).
                ReverseMap();

            CreateMap<AccountSsom, TeamLeaderVM>().ReverseMap();

            CreateMap<Component, ComponentAddCommand>().ReverseMap();

            CreateMap<Component, ComponentUpdateCommand>().ReverseMap();

            CreateMap<ComponentSerial, MetarialAddCommand>().ReverseMap();

            CreateMap<MetarialRequestCommand, Request>()
           .ForMember(p => p.ReceiverCompanyId, s => s.MapFrom(c => c.CompanyId))
           .ForMember(p => p.TeamLeaderId, s => s.MapFrom(c => c.TeamLeaderId))
           .ForMember(p => p.RequestDate, s => s.MapFrom(c => DateTime.Now))
           .ForMember(p => p.RequestMessage, s => s.MapFrom(c => c.Aciklama))
           .ForMember(p => p.ReceiverSiteId, s => s.MapFrom(c => c.SiteId))
           .ForMember(p => p.WorkOrderNo, s => s.MapFrom(c => c.WorkOrderNo))
           .ForMember(p => p.RequestType, s => s.MapFrom(c => c.TalepTip))
           .ForMember(p => p.RequestedMaterial, s => s.MapFrom(c => c.Model)).ReverseMap();

            CreateMap<Request, MetarialRequestResponse>()
                .ForMember(p => p.TalepTip, s => s.MapFrom(c => c.RequestType))
                .ForMember(p => p.WorkOrderNo, s => s.MapFrom(c => c.WorkOrderNo))
                .ForMember(p => p.TeamLeaderName, s => s.MapFrom(c => c.TeamLeader.UserName))
                .ForMember(p => p.TeamLeaderId, s => s.MapFrom(c => c.TeamLeaderId))
                .ForMember(p => p.Name, s => s.MapFrom(c => c.ReceiverSiteId != null ? c.ReceiverSite.SiteName : c.ReceiverCompany.Name))
                .ForMember(p => p.Destination, s => s.MapFrom(c => c.ReceiverSiteId != null ? "Saha" : "Firma"))
                .ForMember(p => p.DestId, s => s.MapFrom(c => c.ReceiverSiteId != null ? c.ReceiverSiteId : c.ReceiverCompanyId))
                .ForMember(p => p.Aciklama, s => s.MapFrom(c => c.RequestMessage))
                .ForMember(p => p.Material, s => s.MapFrom(c => c.RequestedMaterial))
                .ForMember(p => p.Requests, s => s.MapFrom(c => c.RequestSiteCompanySerial)).
                ReverseMap();

            CreateMap<MetarialSiteRequestCommand, Request>()
            .ForMember(p => p.RequestDate, s => s.MapFrom(c => DateTime.Now))
            .ForMember(p => p.RequestMessage, s => s.MapFrom(c => c.Aciklama))
            .ForMember(p => p.SiteId, s => s.MapFrom(c => c.SiteId))
            .ForMember(p => p.WorkOrderNo, s => s.MapFrom(c => c.WorkOrderNo))
            .ForMember(p => p.RequestType, s => s.MapFrom(c => (int)RequestType.SitetoCompany))
            .ForMember(p => p.RequestStatu, s => s.MapFrom(c => (int)RequestStatu.TeamLeader))
            .ForPath(p => p.RequestSiteCompanySerial, s => s.MapFrom(c => c.RequestMaterial)).ReverseMap();

            CreateMap<RequestMaterialVM, RequestSiteCompanySerial>()
                .ForMember(p => p.CserialId, s => s.MapFrom(c => c.Id))
                .ForMember(p => p.Id, s => s.Ignore())
                .ReverseMap();

            CreateMap<MetarialRequestCommand, MetarialRequestUpdateCommand>()
            .ForMember(p => p.Id, s => s.Ignore())
            .ReverseMap();


            CreateMap<AuthorityVM, AuthoritySsom>()
               .ForMember(p => p.YetkiKodu, s => s.MapFrom(c => c.YetkiKodu))
               .ReverseMap();

            CreateMap<RandevuPlanlamaVM, RandevuPlanlanma>()
           .ForMember(p => p.CompanyId, s => s.Ignore())
           .ForMember(p => p.RandevuTarihi, s => s.MapFrom(c => c.RandevuTarihi))
           .ForMember(p => p.RandevuBaslangic, s => s.MapFrom(c => c.StartTime))
           .ForMember(p => p.RandevuBitis, s => s.MapFrom(c => c.EndTime))
           .ReverseMap();

            CreateMap<RequestedMaterial, MaterialsRequestVM>()
                    .ForMember(p => p.Defective, s => s.MapFrom(c => c.Defective))
                    .ForMember(p => p.Sturdy, s => s.MapFrom(c => c.Sturdy))
                    .ForMember(p => p.Scrap, s => s.MapFrom(c => c.Scrap))
                    .ForMember(p => p.EquipmentDescription, s => s.MapFrom(c => c.Component.EquipmentDescription))
                    .ForMember(p => p.ComponentId, s => s.MapFrom(c => c.ComponentId))
                    .ForMember(p => p.MaterialType, s => s.MapFrom(c => c.MaterialType));

            CreateMap<MaterialsRequestVM, RequestedMaterial>()
                    .ForMember(p => p.Defective, s => s.MapFrom(c => c.Defective))
                    .ForMember(p => p.Sturdy, s => s.MapFrom(c => c.Sturdy))
                    .ForMember(p => p.Scrap, s => s.MapFrom(c => c.Scrap))
                    .ForMember(p => p.ComponentId, s => s.MapFrom(c => c.ComponentId))
                    .ForMember(p => p.MaterialType, s => s.MapFrom(c => c.MaterialType));


            CreateMap<ComponentSerial, ComponentSerialVM>()
                .ForMember(p => p.EquipmentDescription, s => s.MapFrom(c => c.Component.EquipmentDescription))
                .ForMember(p => p.Location, s => s.MapFrom(c => c.CompanyId != null ? "Firma" : c.SiteId != null ? "Saha" : "Takım Lideri"))
                .ForMember(p => p.Site, s => s.MapFrom(c => c.Site))
                .ForMember(p => p.Company, s => s.MapFrom(c => c.Company))
                .ForMember(p => p.TeamLeader, s => s.MapFrom(c => c.TeamLeader))
                .ReverseMap();

            CreateMap<RequestSiteCompanySerial, ComponentSerialVM>()
                .IncludeMembers(c => c.Cserial)
                .ForMember(c => c.Id, s => s.MapFrom(c => c.CserialId))
                .ForMember(c => c.Defective, s => s.MapFrom(c => c.Cserial.Defective))
                .ForMember(c => c.Shelf, s => s.MapFrom(c => c.Cserial.Shelf))
                .ForMember(c => c.SiteId, s => s.MapFrom(c => c.Cserial.SiteId))
                .ForMember(c => c.CompanyId, s => s.MapFrom(c => c.Cserial.CompanyId))
                .ForMember(c => c.GIrsaliyeNo, s => s.MapFrom(c => c.Cserial.GIrsaliyeNo))
                .ForMember(c => c.MaterialType, s => s.MapFrom(c => c.Cserial.MaterialType))
                .ForMember(c => c.Scrap, s => s.MapFrom(c => c.Cserial.Scrap))
                .ForMember(c => c.SeriNo, s => s.MapFrom(c => c.Cserial.SeriNo))
                .ForMember(c => c.TeamLeaderId, s => s.MapFrom(c => c.Cserial.TeamLeaderId))
                .ForMember(c => c.EquipmentDescription, s => s.MapFrom(c => c.Cserial.Component.EquipmentDescription))
                .ReverseMap();
            CreateMap<Company, CompanyAddQueryCommand>()
                .ReverseMap();
            CreateMap<Company, CompanyUpdateQueryCommand>()
                .ReverseMap();
            CreateMap<Site, SiteAddQueryCommand>()
              .ReverseMap();
            CreateMap<Site, SiteUpdateQueryCommand>()
                .ReverseMap();

            CreateMap<StoreExit, MaterialExitVM>()
                .ForMember(p => p.CompanyName, s => s.MapFrom(c => c.Company.Name))
                .ForMember(p => p.ComponentId, s => s.MapFrom(c => c.Cserial.ComponentId))
                .ForMember(p => p.CreatedDate, s => s.MapFrom(c => c.CreatedDate))
                .ForMember(p => p.CserialId, s => s.MapFrom(c => c.CserialId))
                .ForMember(p => p.Defective, s => s.MapFrom(c => c.Defective))
                .ForMember(p => p.ExitType, s => s.MapFrom(c => c.ExitType))
                .ForMember(p => p.EquipmentDescription, s => s.MapFrom(c => c.Cserial.Component.EquipmentDescription))
                .ForMember(p => p.ExitCompanyName, s => s.MapFrom(c => c.CompanyIdExitNavigation.Name))
                .ForMember(p => p.ExitSiteName, s => s.MapFrom(c => c.SiteIdExitNavigation.SiteName))
                .ForMember(p => p.ExitTeamLeaderName, s => s.MapFrom(c => c.TeamLeaderIdExitNavigation.UserName))
                .ForMember(p => p.WhoDoneName, s => s.MapFrom(c => c.WhoDone.UserName))
                .ForMember(p => p.GIrsaliyeNo, s => s.MapFrom(c => c.Cserial.GIrsaliyeNo))
                .ForMember(p => p.MaterialType, s => s.MapFrom(c => c.Cserial.MaterialType))
                .ForMember(p => p.Scrap, s => s.MapFrom(c => c.Scrap))
                .ForMember(p => p.SeriNo, s => s.MapFrom(c => c.Cserial.SeriNo))
                .ForMember(p => p.Sturdy, s => s.MapFrom(c => c.Sturdy))
                .ReverseMap();
            CreateMap<StoreIn, MaterialInVM>()
                .ForMember(p => p.CompanyName, s => s.MapFrom(c => c.Company.Name))
                .ForMember(p => p.ComponentId, s => s.MapFrom(c => c.Cserial.ComponentId))
                .ForMember(p => p.CreatedDate, s => s.MapFrom(c => c.CreatedDate))
                .ForMember(p => p.CserialId, s => s.MapFrom(c => c.CserialId))
                .ForMember(p => p.Defective, s => s.MapFrom(c => c.Defective))
                .ForMember(p => p.EnterType, s => s.MapFrom(c => c.EnterType))
                .ForMember(p => p.EquipmentDescription, s => s.MapFrom(c => c.Cserial.Component.EquipmentDescription))
                .ForMember(p => p.FromCompanyName, s => s.MapFrom(c => c.FromCompany.Name))
                .ForMember(p => p.FromSiteName, s => s.MapFrom(c => c.FromSite.SiteName))
                .ForMember(p => p.FromTeamLeaderName, s => s.MapFrom(c => c.FromTeamLeader.UserName))
                .ForMember(p => p.WhoDoneName, s => s.MapFrom(c => c.WhoDone.UserName))
                .ForMember(p => p.GIrsaliyeNo, s => s.MapFrom(c => c.Cserial.GIrsaliyeNo))
                .ForMember(p => p.MaterialType, s => s.MapFrom(c => c.Cserial.MaterialType))
                .ForMember(p => p.Scrap, s => s.MapFrom(c => c.Scrap))
                .ForMember(p => p.SeriNo, s => s.MapFrom(c => c.Cserial.SeriNo))
                .ForMember(p => p.Sturdy, s => s.MapFrom(c => c.Sturdy))
                .ReverseMap();

            CreateMap<Request, RequestsModelVM>()
                .ForMember(p => p.RequestStatu, s => s.MapFrom(c => c.RequestStatu))
                .ForMember(p => p.RequestType, s => s.MapFrom(c => c.RequestType))
                .ForMember(p => p.TeamLeader, s => s.MapFrom(c => c.TeamLeader))
                .ForMember(p => p.CreatedBy, s => s.MapFrom(c => c.WhoDone))
                .ForMember(p => p.RequestDate, s => s.MapFrom(c => c.RequestDate))
                .ForMember(p => p.WorkOrderNo, s => s.MapFrom(c => c.WorkOrderNo))
                .ForMember(p => p.Aciklama, s => s.MapFrom(c => c.RequestMessage))
                .ForMember(p => p.MaterialCount, s => s.MapFrom(m => m.RequestSiteCompanySerial.Count() > 0 ? m.RequestSiteCompanySerial.Select(c => c.Cserial.Scrap + c.Cserial.Sturdy + c.Cserial.Defective).Sum() : m.RequestedMaterial.Select(p => p.Sturdy + p.Defective + p.Scrap).Sum()))
                .ForMember(p => p.RequestDestination, s => s.MapFrom(c => c.ReceiverCompanyId != null ? c.ReceiverCompany.Name : c.ReceiverSite.SiteName))
                .ForMember(p => p.Sender, s => s.MapFrom(c => c.CompanyId != null ? c.Company.Name : c.Site.SiteName))
                .ReverseMap();



            CreateMap<ComponentSerial, VerificationCompanyEX>()
            .ForMember(p => p.EquipmentDescription, s => s.MapFrom(c => c.Component.EquipmentDescription))
            .ForMember(p => p.ComponentId, s => s.MapFrom(c => c.ComponentId))
            .ForMember(p => p.SeriNo, s => s.MapFrom(c => c.SeriNo))
            .ForMember(p => p.CreatedBy, s => s.Ignore())
            .ForMember(p => p.Scrap, s => s.MapFrom(c => c.Scrap))
            .ForMember(p => p.Defective, s => s.MapFrom(c => c.Defective))
            .ForMember(p => p.Sturdy, s => s.MapFrom(c => c.Sturdy))
            .ForMember(p => p.Shelf, s => s.MapFrom(c => c.Shelf))
            .ForMember(p => p.Tarih, s => s.MapFrom(_ => DateTime.Now))
            .ReverseMap();


            CreateMap<Company, CompanyVM>().
                ForMember(p => p.Email, s => s.MapFrom(s => s.Email)).
                ForMember(p => p.IlId, s => s.MapFrom(s => s.IlId)).
                ForMember(p => p.Id, s => s.MapFrom(s => s.Id)).
                ForMember(p => p.Name, s => s.MapFrom(s => s.Name)).
                ForMember(p => p.Il, s => s.MapFrom(s => s.Il)).
                ForMember(p => p.IsEditable, s => s.MapFrom(s => s.AccountSsom.Count <= 0 && s.ComponentSerial.Count <= 0)).
                ReverseMap();

            CreateMap<Site, SiteVM>().
                ForMember(p => p.SiteTip, s => s.MapFrom(s => s.SiteTip)).
                ForMember(p => p.SiteId, s => s.MapFrom(s => s.SiteId)).
                ForMember(p => p.IlId, s => s.MapFrom(s => s.IlId)).
                ForMember(p => p.SiteName, s => s.MapFrom(s => s.SiteName)).
                ForMember(p => p.PlanId, s => s.MapFrom(s => s.PlanId)).
                ForMember(p => p.Region, s => s.MapFrom(s => s.Region)).
                ForMember(p => p.KordinatE, s => s.MapFrom(s => s.KordinatE)).
                ForMember(p => p.KordinatN, s => s.MapFrom(s => s.KordinatN)).
                ForMember(p => p.IsEditable, s => s.MapFrom(s => s.ComponentSerial.Count <= 0 && s.RequestSite.Count <= 0 && s.StoreExitSite.Count <= 0)).
                ForMember(p => p.Il, s => s.MapFrom(s => s.Il)).ReverseMap();

        }
    }
}
