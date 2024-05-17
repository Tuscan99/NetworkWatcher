using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetworkWatcher.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RemoteId = table.Column<string>(type: "TEXT", nullable: true),
                    ConnectionId = table.Column<string>(type: "TEXT", nullable: true),
                    DeviceId = table.Column<string>(type: "TEXT", nullable: true),
                    IPAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    WifiSsid = table.Column<string>(type: "TEXT", nullable: true),
                    WifiSignalStrength = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    LastConnectionDateTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<string>(type: "TEXT", nullable: true),
                    ConnectionId = table.Column<string>(type: "TEXT", nullable: true),
                    WifiSsid = table.Column<string>(type: "TEXT", nullable: true),
                    WifiSignalStrength = table.Column<int>(type: "INTEGER", nullable: true),
                    EventType = table.Column<int>(type: "INTEGER", nullable: true),
                    DateTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OuterLogItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RemoteId = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<string>(type: "TEXT", nullable: false),
                    WifiSsid = table.Column<string>(type: "TEXT", nullable: false),
                    WifiSignalStrength = table.Column<int>(type: "INTEGER", nullable: false),
                    ConnectDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    StatusRaw = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OuterLogItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "LogItems");

            migrationBuilder.DropTable(
                name: "OuterLogItems");
        }
    }
}
