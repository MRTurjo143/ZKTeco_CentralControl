using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZKTeco.Migrations
{
    public partial class BlockList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blocklist",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    empCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    empName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isBlock = table.Column<bool>(type: "bit", nullable: false),
                    blockdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    unblockdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocklist", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blocklist");
        }
    }
}
