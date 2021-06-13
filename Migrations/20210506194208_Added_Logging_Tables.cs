using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkTracker.Migrations
{
    public partial class Added_Logging_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ErrorCode = table.Column<int>(type: "int", nullable: true),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stacktrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    ErrorDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    CountAffected = table.Column<int>(type: "int", nullable: true),
                    FunctionName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "ServiceLogs");
        }
    }
}
