using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerAdministration.Server.DataAccess.Migrations.Slave
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IISLogEvents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientIp = table.Column<string>(nullable: true),
                    ServerPort = table.Column<int>(nullable: true),
                    ServerIp = table.Column<string>(nullable: true),
                    Computername = table.Column<string>(nullable: true),
                    Sitename = table.Column<string>(nullable: true),
                    DateTimeEvent = table.Column<DateTime>(nullable: false),
                    SentBytes = table.Column<int>(nullable: true),
                    RecievedBytes = table.Column<int>(nullable: true),
                    TimeTaken = table.Column<int>(nullable: true),
                    Win32Status = table.Column<long>(nullable: true),
                    ProtocolStatus = table.Column<int>(nullable: true),
                    ProtocolSubstatus = table.Column<int>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    Referer = table.Column<string>(nullable: true),
                    Cookie = table.Column<string>(nullable: true),
                    UserAgent = table.Column<string>(nullable: true),
                    ProtocolVersion = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    UriQuery = table.Column<string>(nullable: true),
                    UriStem = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IISLogEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteIISLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SlaveServerId = table.Column<int>(nullable: false),
                    SiteAppPath = table.Column<string>(nullable: true),
                    IISLogEventId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteIISLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteIISLogs_IISLogEvents_IISLogEventId",
                        column: x => x.IISLogEventId,
                        principalTable: "IISLogEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteIISLogs_IISLogEventId",
                table: "SiteIISLogs",
                column: "IISLogEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteIISLogs");

            migrationBuilder.DropTable(
                name: "IISLogEvents");
        }
    }
}
