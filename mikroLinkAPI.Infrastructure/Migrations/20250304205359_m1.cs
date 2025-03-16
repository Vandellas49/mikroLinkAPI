using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mikroLinkAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthoritySSOM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UygulamaKodu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    YetkiKodu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthoritySSOM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Component",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EquipmentDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MalzemeTuru = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Component", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FirmaMalzemeBarcode",
                columns: table => new
                {
                    Barcode = table.Column<byte[]>(type: "binary(5000)", fixedLength: true, maxLength: 5000, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Ilceler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ilce = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Sehir = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilceler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Iller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sehir = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MYS_LISLEM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MYS_LIS_GUID = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    MYS_LIS_TSTMP = table.Column<DateTime>(type: "datetime", nullable: false),
                    MYS_LIS_DNAME = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    MYS_LDB_UYG_KODU = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    MYS_LDB_IP = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MYS_LISLEM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SMSVerification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CurrentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    VerificationCode = table.Column<int>(type: "int", nullable: false),
                    ApplicationCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSVerification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IlId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_iller",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SiteId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SiteTip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KordinatN = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    KordinatE = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Site_iller",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountSSOM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<byte[]>(type: "binary(150)", fixedLength: true, maxLength: 150, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumberTwo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSSOM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountSSOM_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyToCompanyAuthorization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentCompanyId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyToCompanyAuthorization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyToCompanyAuthorization_Company",
                        column: x => x.ParentCompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyToCompanyAuthorization_Company1",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RandevuPlanlama",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    RandevuTarihi = table.Column<DateTime>(type: "date", nullable: false),
                    RandevuBaslangic = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    RandevuBitis = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandevuPlanlama", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RandevuPlanlama_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    NumberOfCar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanySiteAuthorization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySiteAuthorization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanySiteAuthorization_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanySiteAuthorization_Site",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComponentSerial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeriNo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ComponentId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    G_IrsaliyeNo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Sturdy = table.Column<int>(type: "int", nullable: false),
                    Defective = table.Column<int>(type: "int", nullable: false),
                    Scrap = table.Column<int>(type: "int", nullable: false),
                    Shelf = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialType = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    SiteId = table.Column<int>(type: "int", nullable: true),
                    TeamLeaderId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentSerial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentSerial_AccountSSOM",
                        column: x => x.TeamLeaderId,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComponentSerial_AccountSSOM1",
                        column: x => x.CreatedBy,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentSerial_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComponentSerial_Component",
                        column: x => x.ComponentId,
                        principalTable: "Component",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComponentSerial_Site",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestType = table.Column<int>(type: "int", nullable: false),
                    WhoDoneId = table.Column<int>(type: "int", nullable: false),
                    TeamLeaderId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    SiteId = table.Column<int>(type: "int", nullable: true),
                    RequestStatu = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    WorkOrderNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ReceiverSiteId = table.Column<int>(type: "int", nullable: true),
                    RequestMessage = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReceiverCompanyId = table.Column<int>(type: "int", nullable: true),
                    WhoCanceledId = table.Column<int>(type: "int", nullable: true),
                    CanUpdate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_AccountSSOM",
                        column: x => x.TeamLeaderId,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Request_AccountSSOM1",
                        column: x => x.WhoCanceledId,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Request_AccountSSOM2",
                        column: x => x.WhoDoneId,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Request_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Request_Company1",
                        column: x => x.ReceiverCompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Request_Site",
                        column: x => x.ReceiverSiteId,
                        principalTable: "Site",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Request_Site1",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Randevu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    TeamLeaderId = table.Column<int>(type: "int", nullable: false),
                    RandevuTarihi = table.Column<DateTime>(type: "date", nullable: false),
                    RandevuZamani = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    RandevuBitis = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    RadevuPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Randevu_AccountSSOM",
                        column: x => x.TeamLeaderId,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Randevu_RandevuPlanlama",
                        column: x => x.RadevuPlanId,
                        principalTable: "RandevuPlanlama",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountAuthority",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorityId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAuthority", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountAuthority_AccountSSOM",
                        column: x => x.AccountId,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountAuthority_AuthoritySSOM",
                        column: x => x.AuthorityId,
                        principalTable: "AuthoritySSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountAuthority_Teams",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaterialTracking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CSerialId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    SiteId = table.Column<int>(type: "int", nullable: true),
                    TeamLeaderId = table.Column<int>(type: "int", nullable: true),
                    Sturdy = table.Column<int>(type: "int", nullable: false),
                    Defective = table.Column<int>(type: "int", nullable: false),
                    Scrap = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTracking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialTracking_AccountSSOM",
                        column: x => x.TeamLeaderId,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialTracking_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialTracking_ComponentSerial",
                        column: x => x.CSerialId,
                        principalTable: "ComponentSerial",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialTracking_Site",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestedMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    Sturdy = table.Column<int>(type: "int", nullable: false),
                    Defective = table.Column<int>(type: "int", nullable: false),
                    Scrap = table.Column<int>(type: "int", nullable: false),
                    MaterialType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestedMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestedMaterial_Component",
                        column: x => x.ComponentId,
                        principalTable: "Component",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestedMaterial_Request",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestSiteCompanySerial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    CserialId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestSiteCompanySerial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestCompanySerial_ComponentSerial",
                        column: x => x.CserialId,
                        principalTable: "ComponentSerial",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestSiteCompanySerial_Request",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StoreExit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CSerialId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    SiteId = table.Column<int>(type: "int", nullable: true),
                    TeamLeaderId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sturdy = table.Column<int>(type: "int", nullable: false),
                    Defective = table.Column<int>(type: "int", nullable: false),
                    Scrap = table.Column<int>(type: "int", nullable: false),
                    CompanyIdExit = table.Column<int>(type: "int", nullable: true),
                    SiteIdExit = table.Column<int>(type: "int", nullable: true),
                    TeamLeaderIdExit = table.Column<int>(type: "int", nullable: true),
                    RequestId = table.Column<int>(type: "int", nullable: true),
                    ExitType = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreExit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreExit_AccountSSOM",
                        column: x => x.TeamLeaderId,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreExit_AccountSSOM1",
                        column: x => x.TeamLeaderIdExit,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreExit_AccountSSOM2",
                        column: x => x.CreatedBy,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreExit_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreExit_Company1",
                        column: x => x.CompanyIdExit,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreExit_ComponentSerial",
                        column: x => x.CSerialId,
                        principalTable: "ComponentSerial",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreExit_Request",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreExit_Site",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreExit_Site1",
                        column: x => x.SiteIdExit,
                        principalTable: "Site",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StoreIn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CSerialId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    SiteId = table.Column<int>(type: "int", nullable: true),
                    TeamLeaderId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sturdy = table.Column<int>(type: "int", nullable: false),
                    Defective = table.Column<int>(type: "int", nullable: false),
                    Scrap = table.Column<int>(type: "int", nullable: false),
                    FromCompanyId = table.Column<int>(type: "int", nullable: true),
                    FromSiteId = table.Column<int>(type: "int", nullable: true),
                    FromTeamLeaderId = table.Column<int>(type: "int", nullable: true),
                    RequestId = table.Column<int>(type: "int", nullable: true),
                    EnterType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreIn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreIn_AccountSSOM",
                        column: x => x.FromTeamLeaderId,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreIn_AccountSSOM1",
                        column: x => x.TeamLeaderId,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreIn_AccountSSOM2",
                        column: x => x.CreatedBy,
                        principalTable: "AccountSSOM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreIn_Company",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreIn_Company1",
                        column: x => x.FromCompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreIn_ComponentSerial",
                        column: x => x.CSerialId,
                        principalTable: "ComponentSerial",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreIn_Request",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreIn_Site",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoreIn_Site1",
                        column: x => x.FromSiteId,
                        principalTable: "Site",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountAuthority_AccountId",
                table: "AccountAuthority",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountAuthority_AuthorityId",
                table: "AccountAuthority",
                column: "AuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountAuthority_TeamId",
                table: "AccountAuthority",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSSOM_CompanyId",
                table: "AccountSSOM",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_IlId",
                table: "Company",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySiteAuthorization_CompanyId",
                table: "CompanySiteAuthorization",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySiteAuthorization_SiteId",
                table: "CompanySiteAuthorization",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyToCompanyAuthorization_CompanyId",
                table: "CompanyToCompanyAuthorization",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyToCompanyAuthorization_ParentCompanyId",
                table: "CompanyToCompanyAuthorization",
                column: "ParentCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentSerial_CompanyId",
                table: "ComponentSerial",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentSerial_ComponentId",
                table: "ComponentSerial",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentSerial_CreatedBy",
                table: "ComponentSerial",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentSerial_SiteId",
                table: "ComponentSerial",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentSerial_TeamLeaderId",
                table: "ComponentSerial",
                column: "TeamLeaderId");

            migrationBuilder.CreateIndex(
                name: "UX_Constraint",
                table: "ComponentSerial",
                column: "SeriNo",
                unique: true,
                filter: "[SeriNo] <> 'Sarfmalzeme'");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTracking_CompanyId",
                table: "MaterialTracking",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTracking_CSerialId",
                table: "MaterialTracking",
                column: "CSerialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTracking_SiteId",
                table: "MaterialTracking",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTracking_TeamLeaderId",
                table: "MaterialTracking",
                column: "TeamLeaderId");

            migrationBuilder.CreateIndex(
                name: "IDX_UQ_Company_TeamLeader_Randevu",
                table: "Randevu",
                columns: new[] { "CompanyId", "TeamLeaderId", "RandevuTarihi" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IDX_UQ_Company_TeamLeader_RandevuTarihi",
                table: "Randevu",
                columns: new[] { "CompanyId", "TeamLeaderId", "RandevuTarihi", "RandevuZamani" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_RadevuPlanId",
                table: "Randevu",
                column: "RadevuPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_TeamLeaderId",
                table: "Randevu",
                column: "TeamLeaderId");

            migrationBuilder.CreateIndex(
                name: "IDX_UQ_Company_Baslangic_Bitis_Tarih",
                table: "RandevuPlanlama",
                columns: new[] { "CompanyId", "RandevuBaslangic", "RandevuBitis", "RandevuTarihi" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Request_CompanyId",
                table: "Request",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ReceiverCompanyId",
                table: "Request",
                column: "ReceiverCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ReceiverSiteId",
                table: "Request",
                column: "ReceiverSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_SiteId",
                table: "Request",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_TeamLeaderId",
                table: "Request",
                column: "TeamLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_WhoCanceledId",
                table: "Request",
                column: "WhoCanceledId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_WhoDoneId",
                table: "Request",
                column: "WhoDoneId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedMaterial_ComponentId",
                table: "RequestedMaterial",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedMaterial_RequestId",
                table: "RequestedMaterial",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestSiteCompanySerial_CserialId",
                table: "RequestSiteCompanySerial",
                column: "CserialId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestSiteCompanySerial_RequestId",
                table: "RequestSiteCompanySerial",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_IlId",
                table: "Site",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreExit_CompanyId",
                table: "StoreExit",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreExit_CompanyIdExit",
                table: "StoreExit",
                column: "CompanyIdExit");

            migrationBuilder.CreateIndex(
                name: "IX_StoreExit_CreatedBy",
                table: "StoreExit",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StoreExit_CSerialId",
                table: "StoreExit",
                column: "CSerialId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreExit_RequestId",
                table: "StoreExit",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreExit_SiteId",
                table: "StoreExit",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreExit_SiteIdExit",
                table: "StoreExit",
                column: "SiteIdExit");

            migrationBuilder.CreateIndex(
                name: "IX_StoreExit_TeamLeaderId",
                table: "StoreExit",
                column: "TeamLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreExit_TeamLeaderIdExit",
                table: "StoreExit",
                column: "TeamLeaderIdExit");

            migrationBuilder.CreateIndex(
                name: "IX_StoreIn_CompanyId",
                table: "StoreIn",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreIn_CreatedBy",
                table: "StoreIn",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StoreIn_CSerialId",
                table: "StoreIn",
                column: "CSerialId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreIn_FromCompanyId",
                table: "StoreIn",
                column: "FromCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreIn_FromSiteId",
                table: "StoreIn",
                column: "FromSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreIn_FromTeamLeaderId",
                table: "StoreIn",
                column: "FromTeamLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreIn_RequestId",
                table: "StoreIn",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreIn_SiteId",
                table: "StoreIn",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreIn_TeamLeaderId",
                table: "StoreIn",
                column: "TeamLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CompanyId",
                table: "Teams",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountAuthority");

            migrationBuilder.DropTable(
                name: "CompanySiteAuthorization");

            migrationBuilder.DropTable(
                name: "CompanyToCompanyAuthorization");

            migrationBuilder.DropTable(
                name: "FirmaMalzemeBarcode");

            migrationBuilder.DropTable(
                name: "Ilceler");

            migrationBuilder.DropTable(
                name: "MaterialTracking");

            migrationBuilder.DropTable(
                name: "MYS_LISLEM");

            migrationBuilder.DropTable(
                name: "Randevu");

            migrationBuilder.DropTable(
                name: "RequestedMaterial");

            migrationBuilder.DropTable(
                name: "RequestSiteCompanySerial");

            migrationBuilder.DropTable(
                name: "SMSVerification");

            migrationBuilder.DropTable(
                name: "StoreExit");

            migrationBuilder.DropTable(
                name: "StoreIn");

            migrationBuilder.DropTable(
                name: "AuthoritySSOM");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "RandevuPlanlama");

            migrationBuilder.DropTable(
                name: "ComponentSerial");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Component");

            migrationBuilder.DropTable(
                name: "AccountSSOM");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Iller");
        }
    }
}
