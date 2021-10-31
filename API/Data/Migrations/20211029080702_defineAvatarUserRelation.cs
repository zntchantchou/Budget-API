using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class defineAvatarUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserCampaign");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Expenses",
                newName: "ExpenseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Contributors",
                newName: "ContributorId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Campaigns",
                newName: "CampaignId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Avatars",
                newName: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ExpenseId",
                table: "Expenses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ContributorId",
                table: "Contributors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CampaignId",
                table: "Campaigns",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AvatarId",
                table: "Avatars",
                newName: "Id");

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
                name: "IX_AppUserCampaign_UsersId",
                table: "AppUserCampaign",
                column: "UsersId");
        }
    }
}
