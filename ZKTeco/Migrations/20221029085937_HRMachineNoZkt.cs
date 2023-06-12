using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZKTeco.Migrations
{
    public partial class HRMachineNoZkt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HR_Emp_DeviceInfoHIK",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    comId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cardNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    empImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    fingerData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_Emp_DeviceInfoHIK", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HR_MachineNoZkt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LuserId = table.Column<short>(type: "smallint", nullable: true),
                    Pcname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZKtUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZKtPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inout = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_MachineNoZkt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HR_Emp_DeviceInfoHIK");

            migrationBuilder.DropTable(
                name: "HR_MachineNoZkt");
        }
    }
}
