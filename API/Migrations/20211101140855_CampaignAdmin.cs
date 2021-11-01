using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class CampaignAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Campaigns",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_AdminId",
                table: "Campaigns",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Users_AdminId",
                table: "Campaigns",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Users_AdminId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_AdminId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Campaigns");
        }
    }
}
