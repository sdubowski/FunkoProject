using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunkoProject.Migrations
{
    /// <inheritdoc />
    public partial class EditingUserAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFigures_Users_OwningUserIdId",
                table: "UserFigures");

            migrationBuilder.DropIndex(
                name: "IX_UserFigures_OwningUserIdId",
                table: "UserFigures");

            migrationBuilder.DropColumn(
                name: "OwningUserIdId",
                table: "UserFigures");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserFigures",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserFigures_UserId",
                table: "UserFigures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFigures_Users_UserId",
                table: "UserFigures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFigures_Users_UserId",
                table: "UserFigures");

            migrationBuilder.DropIndex(
                name: "IX_UserFigures_UserId",
                table: "UserFigures");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserFigures",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OwningUserIdId",
                table: "UserFigures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserFigures_OwningUserIdId",
                table: "UserFigures",
                column: "OwningUserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFigures_Users_OwningUserIdId",
                table: "UserFigures",
                column: "OwningUserIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
