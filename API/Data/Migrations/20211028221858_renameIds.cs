using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class renameIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Campaigns_CampaignId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_AuthorId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_PaidById",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "Contributors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses");

            migrationBuilder.RenameTable(
                name: "Expenses",
                newName: "Expense");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Expense",
                newName: "ExpenseId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_PaidById",
                table: "Expense",
                newName: "IX_Expense_PaidById");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_CampaignId",
                table: "Expense",
                newName: "IX_Expense_CampaignId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_AuthorId",
                table: "Expense",
                newName: "IX_Expense_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expense",
                table: "Expense",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Campaigns_CampaignId",
                table: "Expense",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Users_AuthorId",
                table: "Expense",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Users_PaidById",
                table: "Expense",
                column: "PaidById",
                principalTable: "Users",
                principalColumn: "AppUserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Campaigns_CampaignId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Users_AuthorId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Users_PaidById",
                table: "Expense");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expense",
                table: "Expense");

            migrationBuilder.RenameTable(
                name: "Expense",
                newName: "Expenses");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ExpenseId",
                table: "Expenses",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Expense_PaidById",
                table: "Expenses",
                newName: "IX_Expenses_PaidById");

            migrationBuilder.RenameIndex(
                name: "IX_Expense_CampaignId",
                table: "Expenses",
                newName: "IX_Expenses_CampaignId");

            migrationBuilder.RenameIndex(
                name: "IX_Expense_AuthorId",
                table: "Expenses",
                newName: "IX_Expenses_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Contributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExpenseId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contributors_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contributors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_ExpenseId",
                table: "Contributors",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_UserId",
                table: "Contributors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Campaigns_CampaignId",
                table: "Expenses",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_AuthorId",
                table: "Expenses",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_PaidById",
                table: "Expenses",
                column: "PaidById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
