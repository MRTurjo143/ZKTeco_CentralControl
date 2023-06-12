using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZKTeco.Migrations
{
    public partial class HR_ZktFinger_unilever : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HR_ZktFinger_unilever",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    comId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    empCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    empName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    empImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fingerindex1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fingerindex2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fingerindex3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fingerindex4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fingerindex5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fingerindex6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fingerindex7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fingerindex8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fingerindex9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fingerindex10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userGroup = table.Column<long>(type: "bigint", nullable: true),
                    isDelete = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_ZktFinger_unilever", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HR_ZktFinger_unilever");
        }
    }
}
