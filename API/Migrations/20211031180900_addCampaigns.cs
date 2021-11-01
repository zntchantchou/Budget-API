using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addCampaigns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    CampaignId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 140, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.CampaignId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserCampaign",
                columns: table => new
                {
                    CampaignsCampaignId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersAppUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserCampaign", x => new { x.CampaignsCampaignId, x.UsersAppUserId });
                    table.ForeignKey(
                        name: "FK_AppUserCampaign_Campaigns_CampaignsCampaignId",
                        column: x => x.CampaignsCampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserCampaign_Users_UsersAppUserId",
                        column: x => x.UsersAppUserId,
                        principalTable: "Users",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCampaign_UsersAppUserId",
                table: "AppUserCampaign",
                column: "UsersAppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserCampaign");

            migrationBuilder.DropTable(
                name: "Campaigns");
        }
    }
}
