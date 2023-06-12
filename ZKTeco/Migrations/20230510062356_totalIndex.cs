using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZKTeco.Migrations
{
    public partial class totalIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userGroup",
                table: "HR_ZktFinger_unilever",
                newName: "totalIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "totalIndex",
                table: "HR_ZktFinger_unilever",
                newName: "userGroup");
        }
    }
}
