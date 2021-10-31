using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class campaignusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Campaigns_CampaignId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CampaignId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Campaigns",
                newName: "CampaignId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Avatars",
                newName: "AvatarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CampaignId",
                table: "Campaigns",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AvatarId",
                table: "Avatars",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CampaignId",
                table: "Users",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Campaigns_CampaignId",
                table: "Users",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
