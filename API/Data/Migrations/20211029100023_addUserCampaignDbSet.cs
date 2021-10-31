using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class addUserCampaignDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCampaign_Campaigns_UserId",
                table: "UserCampaign");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCampaign_Users_CampaignId",
                table: "UserCampaign");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCampaign",
                table: "UserCampaign");

            migrationBuilder.RenameTable(
                name: "UserCampaign",
                newName: "UserCampaigns");

            migrationBuilder.RenameIndex(
                name: "IX_UserCampaign_CampaignId",
                table: "UserCampaigns",
                newName: "IX_UserCampaigns_CampaignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCampaigns",
                table: "UserCampaigns",
                columns: new[] { "UserId", "CampaignId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserCampaigns_Campaigns_UserId",
                table: "UserCampaigns",
                column: "UserId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCampaigns_Users_CampaignId",
                table: "UserCampaigns",
                column: "CampaignId",
                principalTable: "Users",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCampaigns_Campaigns_UserId",
                table: "UserCampaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCampaigns_Users_CampaignId",
                table: "UserCampaigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCampaigns",
                table: "UserCampaigns");

            migrationBuilder.RenameTable(
                name: "UserCampaigns",
                newName: "UserCampaign");

            migrationBuilder.RenameIndex(
                name: "IX_UserCampaigns_CampaignId",
                table: "UserCampaign",
                newName: "IX_UserCampaign_CampaignId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCampaign",
                table: "UserCampaign",
                columns: new[] { "UserId", "CampaignId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserCampaign_Campaigns_UserId",
                table: "UserCampaign",
                column: "UserId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCampaign_Users_CampaignId",
                table: "UserCampaign",
                column: "CampaignId",
                principalTable: "Users",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
