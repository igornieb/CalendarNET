using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarNET.Data.Migrations
{
    /// <inheritdoc />
    public partial class addtask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    shared = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskCollection_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskCollection_UserId1",
                table: "TaskCollection",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskCollection");
        }
    }
}
