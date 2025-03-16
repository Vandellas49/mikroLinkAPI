using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mikroLinkAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_UQ_Company_TeamLeader_Randevu",
                table: "Randevu");

            migrationBuilder.DropIndex(
                name: "IDX_UQ_Company_TeamLeader_RandevuTarihi",
                table: "Randevu");

            migrationBuilder.DropColumn(
                name: "RandevuBitis",
                table: "Randevu");

            migrationBuilder.DropColumn(
                name: "RandevuTarihi",
                table: "Randevu");

            migrationBuilder.DropColumn(
                name: "RandevuZamani",
                table: "Randevu");

            migrationBuilder.RenameIndex(
                name: "IX_Randevu_RadevuPlanId",
                table: "Randevu",
                newName: "UX_Constraint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UX_Constraint",
                table: "Randevu",
                newName: "IX_Randevu_RadevuPlanId");

            migrationBuilder.AddColumn<string>(
                name: "RandevuBitis",
                table: "Randevu",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RandevuTarihi",
                table: "Randevu",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RandevuZamani",
                table: "Randevu",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");

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
        }
    }
}
