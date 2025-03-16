using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.ViewModel;
using mikroLinkAPI.Infrastructure.Interceptors;

namespace mikroLinkAPI.Infrastructure.Context
{
    internal sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
    {
        public DbSet<AccountAuthority> AccountAuthority { get; set; }
        public DbSet<AccountSsom> AccountSsom { get; set; }
        public DbSet<AuthoritySsom> AuthoritySsom { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanySiteAuthorization> CompanySiteAuthorization { get; set; }
        public DbSet<CompanyToCompanyAuthorization> CompanyToCompanyAuthorization { get; set; }
        public DbSet<Component> Component { get; set; }
        public DbSet<ComponentSerial> ComponentSerial { get; set; }
        public DbSet<FirmaMalzemeBarcode> FirmaMalzemeBarcode { get; set; }
        public DbSet<Ilceler> Ilceler { get; set; }
        public DbSet<Iller> Iller { get; set; }
        public DbSet<MaterialTracking> MaterialTracking { get; set; }
        public DbSet<MysLislem> MysLislem { get; set; }
        public DbSet<Randevu> Randevu { get; set; }
        public DbSet<RandevuPlanlanma> RandevuPlanlama { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<RequestSiteCompanySerial> RequestSiteCompanySerial { get; set; }
        public DbSet<RequestedMaterial> RequestedMaterial { get; set; }
        public DbSet<UserSession> UserSession { get; set; }
        public DbSet<Site> Site { get; set; }
        public DbSet<Smsverification> Smsverification { get; set; }
        public DbSet<StoreExit> StoreExit { get; set; }
        public DbSet<StoreIn> StoreIn { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<FileRecord> FileRecord { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUserRole<Guid>>();
            builder.Ignore<IdentityUserClaim<Guid>>();


        }
    }
}
