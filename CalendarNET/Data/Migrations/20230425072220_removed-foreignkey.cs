using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarNET.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskCollection_AspNetUsers_UserId1",
                table: "TaskCollection");

            migrationBuilder.DropIndex(
                name: "IX_TaskCollection_UserId1",
                table: "TaskCollection");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TaskCollection");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TaskCollection");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TaskCollection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TaskCollection",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskCollection_UserId1",
                table: "TaskCollection",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCollection_AspNetUsers_UserId1",
                table: "TaskCollection",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
