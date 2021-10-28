using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddHashsetAndVirtual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatars_Users_AppUserId",
                table: "Avatars");

            migrationBuilder.DropTable(
                name: "AppUserCampaign");

            migrationBuilder.DropIndex(
                name: "IX_Avatars_AppUserId",
                table: "Avatars");

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvatarId1",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarId1",
                table: "Users",
                column: "AvatarId1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CampaignId",
                table: "Users",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Avatars_AvatarId1",
                table: "Users",
                column: "AvatarId1",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Campaigns_CampaignId",
                table: "Users",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Avatars_AvatarId1",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Campaigns_CampaignId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AvatarId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CampaignId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AvatarId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "AppUserCampaign",
                columns: table => new
                {
                    CampaignsId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserCampaign", x => new { x.CampaignsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AppUserCampaign_Campaigns_CampaignsId",
                        column: x => x.CampaignsId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserCampaign_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_AppUserId",
                table: "Avatars",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCampaign_UsersId",
                table: "AppUserCampaign",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avatars_Users_AppUserId",
                table: "Avatars",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
